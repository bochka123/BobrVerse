import { FC } from 'react';

import { IQuestDto } from '@/models/responses';

import styles from './my-quests-item.module.scss';

type MyQuestsItemProps = {
    quest: IQuestDto;
}

const MyQuestsItem: FC<MyQuestsItemProps> = ({ quest }) => {
    return (
        <div className={styles.questItemWrapper}>
            <div className={styles.backgroundOverlay} />
            <div className={styles.questInfo}>
                <p className={styles.title}>{quest.title}</p>
            </div>
        </div>
    );
};

export { MyQuestsItem };
