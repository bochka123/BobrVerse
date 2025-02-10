import { FC } from 'react';

import { IProfileDto } from '@/models/responses';

import { RatingItem } from './rating-item';
import styles from './ratings.module.scss';

const Ratings: FC = () => {

    const profiles: IProfileDto[] = [
        {
            id: '',
            name: 'Profile 1',
            level: {
                level: 1,
                requiredXP: 100,
                title: 'HZ',
                description: ''
            },
            xp: 0,
            logs: 150
        }
    ];

    return (
        <div className={styles.ratingsWrapper}>
            <div className={styles.ratingsContainer}>
                {
                    profiles.map((x, key) => <RatingItem profile={x} key={key} />)
                }
            </div>
        </div>
    );
};

export { Ratings };
