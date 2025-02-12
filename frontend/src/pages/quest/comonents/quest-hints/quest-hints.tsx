import { FC } from 'react';

import { BaseButton, WoodenContainer } from '@/components';
import { IQuestTaskDto } from '@/models/responses';

import styles from './quest-hints.module.scss';

type QuestHintsProps = {
    task: IQuestTaskDto;
    fetchNextTask: () => void;
    isNextTaskExists: boolean;
    finishQuest: () => void;
}

const QuestHints: FC<QuestHintsProps> = ({ task, fetchNextTask, isNextTaskExists, finishQuest }) => {
    return (
        <WoodenContainer className={styles.hintsContainer}>
            <div className={styles.hintsWrapper}>
                <h2>Resources</h2>
                <ul>
                    {
                        task.requiredResources.map((res, key) => <li key={`resource-${key}`}>{res.name} - {res.quantity}</li>)
                    }
                </ul>
                <BaseButton buttonClasses={styles.button} onClick={() => isNextTaskExists ? fetchNextTask() : finishQuest()}>
                    {isNextTaskExists ? 'Next' : 'Finish'}
                </BaseButton>
            </div>
        </WoodenContainer>
    );
};

export { QuestHints };
