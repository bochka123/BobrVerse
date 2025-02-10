import { FC } from 'react';
import { useNavigate } from 'react-router-dom';

import { IQuestDto } from '@/models/responses';

import styles from './my-quests-item.module.scss';

type MyQuestsItemProps = {
    quest: IQuestDto;
}

const MyQuestsItem: FC<MyQuestsItemProps> = ({ quest }) => {

    const navigate = useNavigate();

    return (
        <div className={styles.questItemWrapper}>
            <div className={styles.backgroundOverlay} />
            <div className={styles.questInfo}>
                <p className={styles.title}>{quest.title}</p>
                <button onClick={() => navigate(`/quests/${quest.id}`)}>{'->'}</button>
            </div>
        </div>
    );
};

export { MyQuestsItem };
