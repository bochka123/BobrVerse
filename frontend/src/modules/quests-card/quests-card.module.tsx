import { FC, ReactNode, useState } from 'react';

import { QuestCardTypes } from '@/common';

import { AvaiableQuests, MyQuests, PassedQuests, Ratings } from './components';
import styles from './quests-card.module.module.scss';

const QuestsCardModule: FC = () => {
    const [activeTab, setActiveTab] = useState(QuestCardTypes.MY_QUESTS);

    const getContent = (): ReactNode => {
        switch (activeTab) {
            case QuestCardTypes.MY_QUESTS:
                return <MyQuests />;
            case QuestCardTypes.PASSED_QUESTS:
                return <PassedQuests />;
            case QuestCardTypes.AVAILABLE_QUESTS:
                return <AvaiableQuests />;
            case QuestCardTypes.RATINGS:
                return <Ratings />;
        }
    };

    return (
        <div className={styles.card}>
            <div className={styles.cardHeader}>
                <ul className={styles.navTabs}>
                    <li className={styles.navItem}>
                        <button
                            className={`${styles.navLink} ${activeTab === QuestCardTypes.MY_QUESTS ? styles.active : ''}`}
                            onClick={() => setActiveTab(QuestCardTypes.MY_QUESTS)}
                        >
                            My quests
                        </button>
                    </li>
                    <li className={styles.navItem}>
                        <button
                            className={`${styles.navLink} ${activeTab === QuestCardTypes.PASSED_QUESTS ? styles.active : ''}`}
                            onClick={() => setActiveTab(QuestCardTypes.PASSED_QUESTS)}
                        >
                            Passed quests
                        </button>
                    </li>
                    <li className={styles.navItem}>
                        <button
                            className={`${styles.navLink} ${activeTab === QuestCardTypes.AVAILABLE_QUESTS ? styles.active : ''}`}
                            onClick={() => setActiveTab(QuestCardTypes.AVAILABLE_QUESTS)}
                        >
                            Available quests
                        </button>
                    </li>
                    <li className={styles.navItem}>
                        <button
                            className={`${styles.navLink} ${activeTab === QuestCardTypes.RATINGS ? styles.active : ''}`}
                            onClick={() => setActiveTab(QuestCardTypes.RATINGS)}
                        >
                            Ratings
                        </button>
                    </li>
                </ul>
            </div>
            <div className={styles.cardBody}>
                {getContent()}
            </div>
        </div>
    );
};

export { QuestsCardModule };
