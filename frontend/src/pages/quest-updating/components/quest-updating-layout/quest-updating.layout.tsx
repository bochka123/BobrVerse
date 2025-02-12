import { FC } from 'react';
import { Outlet } from 'react-router-dom';

import { QuestHints, QuestSlides } from './comonents';
import styles from './quest-updating.layout.module.scss';

type QuestUpdatingLayoutProps = {}
const QuestUpdatingLayout: FC<QuestUpdatingLayoutProps> = () => {


    return (
        <div className={styles.main}>
            <QuestSlides />

            <div className={styles.mainContentWrapper}>
                <Outlet />
            </div>
            <div className={styles.rightPanelWrapper}>
                <QuestHints />
            </div>
        </div>
    );
};

export { QuestUpdatingLayout };
