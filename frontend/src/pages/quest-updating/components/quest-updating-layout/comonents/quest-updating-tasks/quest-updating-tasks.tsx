import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { FC, useState } from 'react';
import { useParams } from 'react-router-dom';

import { IconButton, WoodenContainer } from '@/components';
import { UpsertQuestTaskModal } from '@/modules/modals/upsert-quest-task-modal';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';

import { QuestUpdatingTasksItem } from './components';
import styles from './quest-updating-tasks.module.scss';

const QuestUpdatingTasks: FC = () => {

    const { questId } = useParams();

    const [addTaskModalVisible, setAddTaskModalVisible  ] = useState(false);

    const { questTasks } = useQuestUpdating();
    
    return (
        <>
            <WoodenContainer className={styles.slidesContainer}>
                <div className={styles.slidesWrapper}>
                    <div className={styles.slidesColumn}>
                        {
                            questTasks.map((task, index) => (
                                <QuestUpdatingTasksItem
                                    questId={questId as string}
                                    taskId={task.id}
                                    slideNumber={index + 1}
                                    key={`task-${task.id}`}
                                />
                            ))
                        }
                    </div>
                    <IconButton icon={faPlus} onClick={() => setAddTaskModalVisible(true)}/>
                </div>
            </WoodenContainer>

            <UpsertQuestTaskModal
                visible={addTaskModalVisible}
                setVisible={setAddTaskModalVisible}
                questId={questId as string}
            />
        </>
    );
};

export { QuestUpdatingTasks };
