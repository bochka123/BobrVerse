import { FC } from 'react';

import { IProfileDto } from '@/models/responses';

import styles from './rating-item.module.scss';

type RatingItemProps = {
    profile: IProfileDto;
}

const RatingItem: FC<RatingItemProps> = ({ profile }) => {
    return (
        <div className={styles.itemWrapper}>
            <div className={styles.backgroundOverlay} />
            <div className={styles.itemInfo}>
                <p className={styles.title}>{profile.name}</p>
            </div>
        </div>
    );
};

export { RatingItem };
