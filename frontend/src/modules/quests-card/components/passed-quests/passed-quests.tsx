import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { FC, useState } from 'react';

import { IconButton, Loader } from '@/components';
import { useGetUserQuestResponsesQuery } from '@/services';

import styles from './passed-quests.module.scss';
import { PassedQuestsItem } from './passed-quests-item';

const PAGE_SIZE = 10;

const PassedQuests: FC = () => {
    const [startIndex, setStartIndex] = useState(0);
    const { data: questsData, isLoading: isQuestsLoading } = useGetUserQuestResponsesQuery({
        startIndex,
        endIndex: startIndex + PAGE_SIZE,
    }, 
    { refetchOnMountOrArgChange: true });

    const handleNext = (): void => {
        setStartIndex((prev) => prev + PAGE_SIZE);
    };

    const handlePrevious = (): void => {
        setStartIndex((prev) => Math.max(0, prev - PAGE_SIZE));
    };

    return (
        <div className={styles.passedQuestsWrapper}>
            <div className={styles.passedQuestsContainer}>
                {
                    isQuestsLoading
                    ? <Loader size={30} />
                    :  questsData?.data.map((x, key) => <PassedQuestsItem quest={x} key={`passed-quest-${key}`} />)
                }
            </div>

            <div className={styles.paginationControls}>
                <IconButton
                    onClick={handlePrevious}
                    disabled={startIndex === 0}
                    icon={faArrowLeft} />

                <IconButton
                    onClick={handleNext}
                    disabled={!questsData?.data || questsData.data.length < PAGE_SIZE}
                    icon={faArrowRight} />
            </div>
        </div>
    );
};

export { PassedQuests };
