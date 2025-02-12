import { FC } from 'react';
import { useParams } from 'react-router-dom';

import { WoodenContainer } from '@/components';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';

import { QuestUpdatingHints } from './components';
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
                <h1>Editing Slide {task.id}</h1>
            </WoodenContainer>
            <QuestUpdatingHints />
        </div>
    );
};

export { QuestUpdatingContent };
