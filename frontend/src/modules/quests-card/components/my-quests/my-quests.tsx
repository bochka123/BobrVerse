import { FC } from 'react';

import { Loader } from '@/components';
import { useGetMyQuestsQuery } from '@/services';

import styles from './my-quests.module.scss';
import { MyQuestsItem } from './my-quests-item';

const MyQuests: FC = () => {
    
    const { data: questsData, isLoading: isQuestsLoading } = useGetMyQuestsQuery();

    return (
        <div className={styles.myQuestsWrapper}>
            <div className={styles.myQuestsContainer}>
                {
                    isQuestsLoading
                    ? <Loader size={30} />
                    : questsData?.data.map((x, key) => <MyQuestsItem quest={x} key={`my-quest-${key}`} />)
                }
            </div>
        </div>
    );
};

export { MyQuests };
