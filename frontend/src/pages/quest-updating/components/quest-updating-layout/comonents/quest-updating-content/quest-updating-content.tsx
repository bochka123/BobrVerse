import { faEdit, faSave } from '@fortawesome/free-solid-svg-icons';
import { FC, useEffect, useMemo, useState } from 'react';
import { useParams } from 'react-router-dom';

import { ToastModeEnum } from '@/common';
import { IconButton, Loader, WoodenContainer } from '@/components';
import { useToast } from '@/hooks';
import { PhotoPicker } from '@/modules';
import { UpsertQuestTaskModal } from '@/modules/modals/upsert-quest-task-modal';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';
import defaultImg from '@/resources/background.jpg';
import {
    useDeleteQuestTaskPhotoMutation,
    useLazyGetQuestTaskByIdQuery,
    useUploadQuestTaskPhotoMutation
} from '@/services';

import styles from './quest-updating.module.scss';

const QuestUpdatingContent: FC = () => {
    const { taskId, questId } = useParams<{ questId: string, taskId: string }>();
    const { updateTask, getTaskById } = useQuestUpdating();

    const [uploadPhoto] = useUploadQuestTaskPhotoMutation();
    const [deletePhoto] = useDeleteQuestTaskPhotoMutation();

    const [taskTrigger, { isLoading: isTaskLoading }] = useLazyGetQuestTaskByIdQuery();

    useEffect(() => {
        if (!getTaskById(taskId as string)) {
            taskTrigger(taskId as string)
                .unwrap()
                .then((res) => updateTask(taskId as string, res.data));
        }
    }, [taskId]);

    const [editModalVisible, setEditModalVisible ] = useState(false);

    const task = useMemo(() => getTaskById(taskId as string), [taskId, getTaskById]);

    const [imageUrl, setImageUrl] = useState<string | undefined>(task?.url);
    const [file, setFile] = useState<File | null>(null);

    useEffect(() => {
        setImageUrl(task?.url);
    }, [task?.url]);

    const { addToast } = useToast();

    const onSave = (): void => {
        if (task && imageUrl && file && imageUrl !== task?.url) {
            const formData = new FormData();
            formData.append(file.name, file, `/${file.name}`);

            uploadPhoto({ taskId: taskId as string, photoDto: formData })
                .unwrap()
                .then(() => {
                    updateTask(taskId as string, { ...task, url: imageUrl });
                    addToast(ToastModeEnum.SUCCESS, 'Successfully updated task image');
                })
                .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to update task image'));
        }

        if (task?.url && !imageUrl) {
            deletePhoto(taskId as string)
                .unwrap()
                .then(() => {
                    updateTask(taskId as string, { ...task, url: '' });
                    addToast(ToastModeEnum.SUCCESS, 'Successfully deleted task image');
                })
                .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to delete task image'));
        }
    };

    const onDeletePhoto = (): void => {
        setImageUrl(undefined);
        setFile(null);
    };

    if (isTaskLoading) return <Loader size={30}/>;

    if (!task) {
        return <WoodenContainer className={styles.container}><h2>Slide not found</h2></WoodenContainer>;
    }

    return (
        <div className={styles.wrapper}>
            <WoodenContainer className={styles.container}>
                <h2>Edit task</h2>
                <div className={styles.photoPickerWrapper}>
                    <PhotoPicker
                        setImageUrl={setImageUrl}
                        setFile={setFile}
                        imageUrl={imageUrl}
                        onDeletePhoto={onDeletePhoto}
                        defaultImage={defaultImg}
                    />
                    <IconButton icon={faSave} onClick={onSave}/>
                </div>
                <div className={styles.infoWrapper}>
                    <p>Task type: {task.taskType}</p>
                    <p>Short description: {task.shortDescription}</p>
                    <p>Description: {task.description}</p>
                    <p>Max attempts: {task.maxAttempts}</p>
                    <p>Time limit in seconds: {task.timeLimitInSeconds}</p>
                    <p>Is required for next stage: {task.isRequiredForNextStage ? 'Yes' : 'No'}</p>
                    <p>Is temple: {task.isTemplate ? 'Yes' : 'No'}</p>
                    <p>Resources: {task.requiredResources.map(resource => resource.name).join(', ')}</p>
                </div>
                <div className={styles.buttonWrapper}>
                    <IconButton icon={faEdit} onClick={() => setEditModalVisible(true)}/>
                </div>
            </WoodenContainer>

            {
                editModalVisible &&
                <UpsertQuestTaskModal
                    visible={editModalVisible}
                    setVisible={setEditModalVisible}
                    questId={questId as string}
                    taskForEditing={task}
                />
            }
        </div>
    );
};

export { QuestUpdatingContent };
