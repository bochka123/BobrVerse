import { FC } from 'react';

import { Loader } from '@/components';
import { useGetUserQuestResponsesQuery } from '@/services';

import styles from './passed-quests.module.scss';
import { PassedQuestsItem } from './passed-quests-item';

const PassedQuests: FC = () => {
    const { data: questsData, isLoading: isQuestsLoading } = useGetUserQuestResponsesQuery({ startIndex: 0, endIndex: 10 });
    console.log(questsData);

    return (
        <div className={styles.passedQuestsWrapper}>
            <div className={styles.passedQuestsContainer}>
                {
                    isQuestsLoading
                    ? <Loader size={30} />
                    :  questsData?.data.map((x, key) => <PassedQuestsItem quest={x} key={`passed-quest-${key}`} />)
                }
            </div>
        </div>
    );
};

export { PassedQuests };
