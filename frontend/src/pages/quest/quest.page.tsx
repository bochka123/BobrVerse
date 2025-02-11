import { FC } from 'react';

import { BackButton } from '@/components';

import { QuestAnswer, QuestHints, QuestQuestion } from './comonents';
import styles from './quest.page.module.scss';

type QuestPageProps = {}
const QuestPage: FC<QuestPageProps> = () => {
    return (
        <>
            <BackButton />
            <div className={styles.container}>
                <div className={styles.leftPanelWrapper}>
                    <h1>Quest name</h1>
                    <QuestQuestion />
                    <QuestAnswer />
                </div>
                <div className={styles.rightPanelWrapper}>
                    <div className={styles.timeWrapper}><h1>Time left: 00:00</h1></div>
                    <QuestHints />
                </div>
            </div>
        </>
    );
};

export { QuestPage };
