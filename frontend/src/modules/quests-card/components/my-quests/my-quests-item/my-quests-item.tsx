import { faArrowRight,faEdit } from '@fortawesome/free-solid-svg-icons';
import { FC } from 'react';
import { useNavigate } from 'react-router-dom';

import { IconButton } from '@/components';
import { IQuestDto } from '@/models/responses';

import styles from './my-quests-item.module.scss';

type MyQuestsItemProps = {
    quest: IQuestDto;
}

const MyQuestsItem: FC<MyQuestsItemProps> = ({ quest }) => {

    const navigate = useNavigate();

    return (
        <div className={styles.itemWrapper}>
            <div className={styles.backgroundOverlay} />
            <div className={styles.itemInfo}>
                <p className={styles.title}>{quest.title}</p>

                <div className={styles.buttonsWrapper}>
                    <IconButton icon={faEdit} onClick={() => {}} />
                    <IconButton icon={faArrowRight} onClick={() => navigate(`/quests/edit/${quest.id}`)} />
                </div>
            </div>
        </div>
    );
};

export { MyQuestsItem };
