import { FC } from 'react';

import { WoodenContainer } from '@/components';

import { QuestAnswer, QuestQuestion } from './comonents';
import styles from './quest.page.module.scss';

type QuestPageProps = {}
const QuestPage: FC<QuestPageProps> = () => {
    return (
        <div className={styles.container}>
            <div className={styles.leftPanelWrapper}>
                <h1>Quest name</h1>
                <QuestQuestion />
                <QuestAnswer />
            </div>
            <div className={styles.rightPanelWrapper}>
                <div className={styles.timeWrapper}><h1>Time left: 00:00</h1></div>
                <WoodenContainer className={styles.hintsContainer}>
                    <div className={styles.hintsWrapper}>
                        <h2>Objects</h2>
                        <ul>
                            <li>tree</li>
                            <li>binary tree</li>
                        </ul>
                    </div>
                    <div className={styles.hintsWrapper}>
                        <h2>Commands</h2>
                        <ul>
                            <li>go</li>
                            <li>play</li>
                            <li>set</li>
                        </ul>
                    </div>
                </WoodenContainer>
            </div>
        </div>
    );
};

export { QuestPage };
