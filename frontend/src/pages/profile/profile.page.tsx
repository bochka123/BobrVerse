import { FC } from 'react';

import img from '@/resources/profile.png';

import styles from './profile.page.module.scss';

const ProfilePage: FC = () => {
    return (
        <div className={styles.profileContainer}>
            <div className={styles.profileFrame}>
                <img src={img} alt="Profile Picture" className={styles.profileImg} />
            </div>

            <div>
                Name
            </div>
        </div>
    );
};

export { ProfilePage };
