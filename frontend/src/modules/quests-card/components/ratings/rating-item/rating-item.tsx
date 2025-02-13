import { FC } from 'react';

import styles from './rating-item.module.scss';
import { IQuestRatingDTO } from '@/models/requests';

type RatingItemProps = {
    profile: IQuestRatingDTO;
}

const RatingItem: FC<RatingItemProps> = ({ profile }) => {
    return (
        <div className={styles.itemWrapper}>
            <div className={styles.backgroundOverlay} />
            <div className={styles.itemInfo}>
                <p className={styles.title}>{profile.questTitle}</p>
                <p className={styles.title}>{profile.authorName}</p>
                <p className={styles.title}>{profile.averageRating}</p>
            </div>
        </div>
    );
};

export { RatingItem };
