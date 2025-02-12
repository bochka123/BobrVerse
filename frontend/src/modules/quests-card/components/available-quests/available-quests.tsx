import { FC } from 'react';

import { Loader } from '@/components';
import { useGetAvailableQuestsQuery } from '@/services';

import styles from './available-quests.module.scss';
import { AvailableQuestsItem } from './available-quests-item';

const AvaiableQuests: FC = () => {
    
    const { data: questsData, isLoading: isQuestsLoading } = useGetAvailableQuestsQuery();

    return (
        <div className={styles.myQuestsWrapper}>
            <div className={styles.myQuestsContainer}>
                {
                    isQuestsLoading
                    ? <Loader size={30} />
                    : questsData?.data.map((x, key) => <AvailableQuestsItem quest={x} key={`available-quest-${key}`} />)
                }
            </div>
        </div>
    );
};

export { AvaiableQuests };
