import React, { FC } from 'react';

import { BaseButton, WoodenContainer } from '@/components';
import { IQuestTaskDto } from '@/models/responses';

import styles from './quest-hints.module.scss';

type QuestHintsProps = {
    task: IQuestTaskDto;
    fetchNextTask: () => void;
    isNextTaskExists: boolean;
}

const areEqual = (prevProps: QuestHintsProps, nextProps: QuestHintsProps): boolean => {
    return (
        prevProps.isNextTaskExists === nextProps.isNextTaskExists &&
        prevProps.fetchNextTask === nextProps.fetchNextTask &&
        JSON.stringify(prevProps.task.requiredResources) === JSON.stringify(nextProps.task.requiredResources)
    );
};

const QuestHints: FC<QuestHintsProps> = React.memo(({ task, fetchNextTask, isNextTaskExists }) => {
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
}, areEqual);

export { QuestHints };
