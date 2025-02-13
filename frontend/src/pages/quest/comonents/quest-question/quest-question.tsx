import React, { FC } from 'react';

import { WoodenContainer } from '@/components';
import { IQuestTaskDto, IQuestTaskTypeInfoDto } from '@/models/responses';
import img from '@/resources/profile.png';

import styles from './quest-question.module.scss';

type QuestQuestionProps = {
    task: IQuestTaskDto;
    taskType?: IQuestTaskTypeInfoDto;
}

const areEqual = (prevProps: QuestQuestionProps, nextProps: QuestQuestionProps): boolean => {
    return (
        JSON.stringify(prevProps.task) === JSON.stringify(nextProps.task) &&
        JSON.stringify(prevProps.taskType) === JSON.stringify(nextProps.taskType)
    );
};

const QuestQuestion: FC<QuestQuestionProps> = React.memo(({ task, taskType }) => {
    return (
        <WoodenContainer className={styles.questionContainer}>
            <div className={styles.questionImage}>
                <img src={img} alt="question image"/>
            </div>
            <div className={styles.questionInfo}>
                <h3>{taskType?.description}</h3>
                <h1>Task #{task.order + 1}</h1>
                <p>{task.shortDescription}</p>
                <p>{task.description}</p>
            </div>
        </WoodenContainer>
    );
}, areEqual);

export { QuestQuestion };
