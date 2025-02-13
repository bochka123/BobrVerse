import { faGear } from '@fortawesome/free-solid-svg-icons';
import { FC, useState } from 'react';

import { BaseButton, IconButton, Loader } from '@/components';
import { ButtonSizeEnum } from '@/components/primitives/buttons/common';
import { useProfileHook } from '@/hooks';
import { EditProfileModal, QuestsCardModule } from '@/modules';
import { UpsertQuestModal } from '@/modules/modals';
import logImage from '@/resources/log.png';
import img from '@/resources/profile.png';

import { useProfilePageHook } from './hooks';
import styles from './profile.page.module.scss';

const ProfilePage: FC = () => {

    const { isProfileLoading } = useProfilePageHook();
    const { name, logs, url, xp, level } = useProfileHook();
    const [settingsVisible, setSettingsVisible] = useState<boolean>(false);
    const [createQuestModalVisible, setCreateQuestModalVisible] = useState<boolean>(false);

    if (isProfileLoading) {
        return <Loader />;
    }

    return (
        <>
            <div>
                <div className={styles.profileContainer}>
                    <div className={styles.profileFrame}>
                        <img src={(url == undefined || url == '') ? img : url} alt="Profile Picture" className={styles.profileImg} />
                    </div>

                    <div className={styles.profileInfo}>
                        <div>
                            <p className={styles.name}>{name || 'Unknown'}</p>
                            <p className={styles.xp}>{level.title}</p>
                            <p className={styles.xp}>{xp} XP</p>
                            <p className={styles.xp}>{level.level} Level</p>
                            <p className={styles.xp}>{level.description}</p>
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
                                    buttonClasses={styles.createButton}
                                    enableHover={false}
                                    onClick={() => setCreateQuestModalVisible(true)}
                                >
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
            <div className={styles.settingsButton}>
                <IconButton icon={faGear} onClick={() => setSettingsVisible(true)} />
            </div>

            <EditProfileModal visible={settingsVisible} setVisible={setSettingsVisible} />
            <UpsertQuestModal visible={createQuestModalVisible} setVisible={setCreateQuestModalVisible} />
        </>
    );
};

export { ProfilePage };
