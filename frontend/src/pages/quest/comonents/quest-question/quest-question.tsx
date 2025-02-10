import { FC } from 'react';

import { WoodenContainer } from '@/components';
import styles from '@/pages/quest/quest.page.module.scss';

type QuestQuestionProps = {}
const QuestQuestion: FC<QuestQuestionProps> = () => {
    return (
        <WoodenContainer className={styles.questionContainer}>
            <div className={styles.questionImage}>
                <img src="/src/resources/profile.png" alt="question image"/>
            </div>

            <>Question</>
        </WoodenContainer>
    );
};

export { QuestQuestion };
