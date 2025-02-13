import { FC } from 'react';

import { IQuestResponseDto } from '@/models/responses';

import styles from './passed-quests-item.module.scss';

type PassedQuestsItemProps = {
    quest: IQuestResponseDto;
}

const PassedQuestsItem: FC<PassedQuestsItemProps> = ({ quest }) => {
    return (
        <div className={styles.itemWrapper}>
            <div className={styles.backgroundOverlay} />
            <div className={styles.itemInfo}>
                <p className={styles.title}>{quest.questTitle}</p>
                <p className={styles.title}>{quest.status}</p>
                <p className={styles.xpEarned}>{quest.xpEarned}xp</p>
            </div>
        </div>
    );
};

export { PassedQuestsItem };
