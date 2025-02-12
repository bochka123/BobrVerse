import { FC } from 'react';

import { WoodenContainer } from '@/components';
import { IQuestTaskDto } from '@/models/responses';

import styles from './quest-question.module.scss';

type QuestQuestionProps = {
    task: IQuestTaskDto;
}

const QuestQuestion: FC<QuestQuestionProps> = ({ task }) => {
    return (
        <WoodenContainer className={styles.questionContainer}>
            <div className={styles.questionImage}>
                <img src="/src/resources/profile.png" alt="question image"/>
            </div>
            <div className={styles.questionImage}>
                <h1>Task #{task.order + 1}</h1>
                <p>{task.shortDescription}</p>
                <p>{task.description}</p>
            </div>
        </WoodenContainer>
    );
};

export { QuestQuestion };
