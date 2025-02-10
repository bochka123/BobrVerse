import { FC } from 'react';

import { IQuestDto } from '@/models/responses';

import styles from './passed-quests-item.module.scss';

type PassedQuestsItemProps = {
    quest: IQuestDto;
}

const PassedQuestsItem: FC<PassedQuestsItemProps> = ({ quest }) => {
    return (
        <div className={styles.itemWrapper}>
            <div className={styles.backgroundOverlay} />
            <div className={styles.itemInfo}>
                <p className={styles.title}>{quest.title}</p>

                <div>
                    
                </div>
            </div>
        </div>
    );
};

export { PassedQuestsItem };
