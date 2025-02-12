import { faEdit, faSave } from '@fortawesome/free-solid-svg-icons';
import { FC, useEffect, useMemo, useState } from 'react';
import { useParams } from 'react-router-dom';

import { IconButton, Loader, WoodenContainer } from '@/components';
import { PhotoPicker } from '@/modules';
import { UpsertQuestTaskModal } from '@/modules/modals/upsert-quest-task-modal';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';
import defaultImg from '@/resources/background.jpg';
import {
    useLazyGetQuestTaskByIdQuery,
} from '@/services';

import { useQuestUpdatingPhoto } from './hooks';
import styles from './quest-updating.module.scss';

const QuestUpdatingContent: FC = () => {
    const { taskId, questId } = useParams<{ questId: string, taskId: string }>();
    const { updateTask, getTaskById } = useQuestUpdating();

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

    useEffect(() => setImageUrl(task?.url), [task?.url]);

    const { uploadPhoto, deletePhoto } = useQuestUpdatingPhoto(task);

    const [imageUrl, setImageUrl] = useState<string | undefined>(task?.url);
    const [file, setFile] = useState<File | null>(null);

    const onSave = (): void => {
        if (task && imageUrl && file && imageUrl !== task?.url) uploadPhoto(file, imageUrl);
        if (task?.url && !imageUrl) deletePhoto();
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
                    <div className={styles.infoBlock}>
                        <p>Task type: {task.taskType}</p>
                        <p>Short description: {task.shortDescription}</p>
                        <p>Description: {task.description}</p>
                        <p>Max attempts: {task.maxAttempts}</p>
                    </div>
                    <div className={styles.infoBlock}>
                        <p>Time limit in seconds: {task.timeLimitInSeconds}</p>
                        <p>Is required for next stage: {task.isRequiredForNextStage ? 'Yes' : 'No'}</p>
                        <p>Is temple: {task.isTemplate ? 'Yes' : 'No'}</p>
                        <p>Resources: {task.requiredResources.map(resource => resource.name).join(', ')}</p>
                    </div>
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
