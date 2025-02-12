import { faEdit } from '@fortawesome/free-solid-svg-icons';
import { FC, useEffect, useMemo, useState } from 'react';
import { useParams } from 'react-router-dom';

import { IconButton, Loader, WoodenContainer } from '@/components';
import { UpsertQuestTaskModal } from '@/modules/modals/upsert-quest-task-modal';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';
import { useLazyGetQuestTaskByIdQuery } from '@/services';

import styles from './quest-updating.module.scss';

const QuestUpdatingContent: FC = () => {
    const { taskId, questId } = useParams<{ questId: string, taskId: string }>();
    const { updateTask, getTaskById } = useQuestUpdating();

    const [taskTrigger, { isLoading: isTaskLoading }] = useLazyGetQuestTaskByIdQuery();

    useEffect(() => {
        taskTrigger(taskId as string)
            .unwrap()
            .then((res) => updateTask(taskId as string, res.data));
    }, [taskId]);

    const [editModalVisible, setEditModalVisible ] = useState(false);

    const task = useMemo(() => getTaskById(taskId as string), [taskId]);

    if (isTaskLoading) return <Loader size={30}/>;

    if (!task) {
        return <WoodenContainer className={styles.container}><h2>Slide not found</h2></WoodenContainer>;
    }

    return (
        <div className={styles.wrapper}>
            <WoodenContainer className={styles.container}>
                <div className={styles.wrapper}>
                    <h1>Editing task</h1>
                </div>
                <div className={styles.buttonWrapper}>
                    <IconButton icon={faEdit} onClick={() => setEditModalVisible(true)}/>
                </div>
            </WoodenContainer>

            <UpsertQuestTaskModal
                visible={editModalVisible}
                setVisible={setEditModalVisible}
                questId={questId as string}
                taskForEditing={task}
            />
        </div>
    );
};

export { QuestUpdatingContent };
