import { FC } from 'react';

import { useProfileHook } from '@/hooks';
import img from '@/resources/profile.png';

import { useProfilePageHook } from './hooks';
import styles from './profile.page.module.scss';

const ProfilePage: FC = () => {
    
    const { isProfileLoading } = useProfilePageHook();
    const { name } = useProfileHook();

    if (isProfileLoading) {
        return <div>Loading...</div>;
    }
    
    return (
        <div className={styles.profileContainer}>
            <div className={styles.profileFrame}>
                <img src={img} alt="Profile Picture" className={styles.profileImg} />
            </div>

            <div>
                <div>
                    <p className={styles.name}>{name || 'Unknown'}</p>
                </div>
                <div>

                </div>
            </div>
        </div>
    );
};

export { ProfilePage };
