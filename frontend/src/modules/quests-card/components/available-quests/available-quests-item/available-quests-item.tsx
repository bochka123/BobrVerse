import { faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { FC } from 'react';
import { useNavigate } from 'react-router-dom';

import { IconButton } from '@/components';
import { IQuestDto } from '@/models/responses';

import styles from './available-quests-item.module.scss';

type AvailableQuestsItemProps = {
    quest: IQuestDto;
}

const AvailableQuestsItem: FC<AvailableQuestsItemProps> = ({ quest }) => {

    const navigate = useNavigate();

    return (
        <div className={styles.itemWrapper}>
            <div className={styles.backgroundOverlay} />
            <div className={styles.itemInfo}>
                <p className={styles.title}>{quest.title}</p>

                <div>
                    <IconButton icon={faArrowRight} onClick={() => navigate(`/quests/${quest.id}`)} />
                </div>
            </div>
        </div>
    );
};

export { AvailableQuestsItem };
