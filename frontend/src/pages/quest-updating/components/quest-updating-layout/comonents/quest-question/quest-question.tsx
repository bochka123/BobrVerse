import { FC } from 'react';

import { WoodenContainer } from '@/components';

import styles from './quest-question.module.scss';

type QuestQuestionProps = {
    title: string;
}

const QuestQuestion: FC<QuestQuestionProps> = ({ title }) => {
    return (
        <WoodenContainer className={styles.questionContainer}>
            <div className={styles.questionImage}>
                <img src="/src/resources/profile.png" alt="question image"/>
            </div>
            {title}
            <p>Question</p>
        </WoodenContainer>
    );
};

export { QuestQuestion };
