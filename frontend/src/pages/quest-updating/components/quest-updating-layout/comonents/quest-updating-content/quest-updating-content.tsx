import { faSave } from '@fortawesome/free-solid-svg-icons';
import { FC } from 'react';
import { useParams } from 'react-router-dom';

import { IconButton, WoodenContainer } from '@/components';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';

import styles from './quest-updating.module.scss';

const QuestUpdatingContent: FC = () => {
    const { taskId } = useParams<{ taskId: string }>();
    const { questTasks } = useQuestUpdating();

    const task = questTasks.find(task => task.id === taskId);

    if (!task) {
        return <WoodenContainer className={styles.container}><h2>Slide not found</h2></WoodenContainer>;
    }

    return (
        <div className={styles.wrapper}>
            <WoodenContainer className={styles.container}>
                <div className={styles.wrapper}>
                    <h1>Editing Slide {task.id}</h1>
                </div>
                <div className={styles.buttonWrapper}>
                    <IconButton icon={faSave}/>
                </div>
            </WoodenContainer>
        </div>
    );
};

export { QuestUpdatingContent };
