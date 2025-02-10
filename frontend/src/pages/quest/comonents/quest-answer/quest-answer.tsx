import { FC } from 'react';

import { WoodenContainer } from '@/components';
import styles from '@/pages/quest/quest.page.module.scss';

type QuestAnswerProps = {}
const QuestAnswer: FC<QuestAnswerProps> = () => {
    return (
        <WoodenContainer className={styles.answerContainer}>
            <>Answer</>
        </WoodenContainer>
    );
};

export { QuestAnswer };
