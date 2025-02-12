import { FC, useEffect, useState } from 'react';

import { Loader } from '@/components';
import { useConnection } from '@/hooks';
import { IQuestDto } from '@/models/responses';
import { useGetMyQuestsQuery } from '@/services';

import styles from './my-quests.module.scss';
import { MyQuestsItem } from './my-quests-item';

const MyQuests: FC = () => {
    
    const { data: questsData, isLoading: isQuestsLoading } = useGetMyQuestsQuery({}, { refetchOnMountOrArgChange: true });
    const [myQuests, setMyQuests] = useState<IQuestDto[] | undefined>(questsData?.data);
    const { connection } = useConnection();

    useEffect(() => {
        setMyQuests(questsData?.data);
    }, [isQuestsLoading]);

    useEffect(() => {
        const handleQuestCreated = (data: IQuestDto): void => {
            setMyQuests((prevState: IQuestDto[] | undefined) => {
                const updatedQuests = prevState ? [...prevState, data] : [data];
                return updatedQuests;
            });
        };
    
        if(connection != null)
            connection.on('questCreated', handleQuestCreated);
    
        return () => {
            if(connection != null)
                connection.off('questCreated', handleQuestCreated);
        };
    }, [connection]);

    return (
        <div className={styles.myQuestsWrapper}>
            <div className={styles.myQuestsContainer}>
                {
                    isQuestsLoading && myQuests
                    ? <Loader size={30} />
                    : myQuests?.map((x, key) => <MyQuestsItem quest={x} key={`my-quest-${key}`} />)
                }
            </div>
        </div>
    );
};

export { MyQuests };
