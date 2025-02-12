import { FC, useEffect, useState } from 'react';
import { clearInterval,setInterval } from 'worker-timers';

import { BackButton } from '@/components';

import { QuestAnswer, QuestHints, QuestQuestion } from './comonents';
import styles from './quest.page.module.scss';

type QuestPageProps = {}
const QuestPage: FC<QuestPageProps> = () => {
    const [timeLeft, setTimeLeft] = useState<number>(300);

    useEffect(() => {
        const timer = setInterval(() => {
            setTimeLeft((prevTime) => (prevTime > 0 ? prevTime - 1 : 0));
        }, 1000);

        return () => clearInterval(timer);
    }, []);

    const formatTime = (seconds: number): string => {
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = seconds % 60;
        return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
    };

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
                    <div className={styles.timeWrapper}><h1>Time left: {formatTime(timeLeft)}</h1></div>
                    <QuestHints />
                </div>
            </div>
        </>
    );
};

export { QuestPage };
