import { FC } from 'react';

import { BaseButton, WoodenContainer } from '@/components';
import { IQuestTaskDto } from '@/models/responses';

import styles from './quest-hints.module.scss';

type QuestHintsProps = {
    task: IQuestTaskDto;
    fetchNextTask: () => void;
    isNextTaskExists: boolean;
}

const QuestHints: FC<QuestHintsProps> = ({ task, fetchNextTask, isNextTaskExists }) => {
    return (
        <WoodenContainer className={styles.hintsContainer}>
            <div className={styles.hintsWrapper}>
                <h2>Resources</h2>
                <ul>
                    {
                        task.requiredResources.map((res, key) => <li key={`resource-${key}`}>{res.name} - {res.quantity}</li>)
                    }
                </ul>
                <BaseButton buttonClasses={styles.button} onClick={fetchNextTask}>
                    {isNextTaskExists ? 'Next' : 'Finish'}
                </BaseButton>
            </div>
        </WoodenContainer>
    );
};

export { QuestHints };
