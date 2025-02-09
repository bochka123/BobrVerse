import { FC, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

import { BaseButton } from '@/components';
import { ButtonSizeEnum } from '@/components/primitives/buttons/common';
import { useProfileHook } from '@/hooks';
import { QuestsCardModule } from '@/modules';
import logImage from '@/resources/log.png';
import img from '@/resources/profile.png';

import { useProfilePageHook } from './hooks';
import styles from './profile.page.module.scss';

const ProfilePage: FC = () => {

    const { isProfileLoading, hasLoaded } = useProfilePageHook();
    const { id, name, logs } = useProfileHook();
    const navigate = useNavigate();

    useEffect(() => {
        // TODO: fix broken logic
        if(id !== '')
            return;
        
        if ((hasLoaded && !isProfileLoading) || (!hasLoaded && !isProfileLoading)) {            
            navigate('auth');
        }
    }, [id, isProfileLoading, navigate, hasLoaded]);

    if (isProfileLoading) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <div className={styles.profileContainer}>
                <div className={styles.profileFrame}>
                    <img src={img} alt="Profile Picture" className={styles.profileImg} />
                </div>

                <div className={styles.profileInfo}>
                    <div>
                        <p className={styles.name}>{name || 'Unknown'}</p>
                    </div>
                    <div className={styles.statsInfo}>
                        <div className={styles.logsInfo}>
                            <div>
                                <img src={logImage} alt="Wood log" />
                            </div>
                            <div>
                                <p className={styles.logs}>{logs}</p>
                            </div>
                        </div>
                        <div className={styles.buttonWrapper}>
                            <BaseButton
                                size={ButtonSizeEnum.LARGE}
                                buttonClasses={styles.createButton}>
                                Create
                            </BaseButton>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <QuestsCardModule />
            </div>
        </div>
    );
};

export { ProfilePage };
