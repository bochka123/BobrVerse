import { FC, useEffect } from 'react';
import { Outlet, useParams } from 'react-router-dom';

import { Loader } from '@/components';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';
import { useGetQuestByIdQuery } from '@/services';

import { QuestHints, QuestSlides } from './comonents';
import styles from './quest-updating.layout.module.scss';

type QuestUpdatingLayoutProps = {}
const QuestUpdatingLayout: FC<QuestUpdatingLayoutProps> = () => {
    
    const { questId } = useParams();
    const { data: questData, isLoading: isQuestLoading } = useGetQuestByIdQuery(questId as string);
    
    const { addTask } = useQuestUpdating();

    useEffect(() => {
        if (questData?.data) {
            for (let i = 0; i < questData.data.numberOfTasks; i++) {
                addTask({ order: i });
            }
        }
    }, [questData]);

    if (isQuestLoading) return <Loader size={30} />;
    
    return (
        <div className={styles.main}>
            <QuestSlides />
            <Outlet />
            <QuestHints />
        </div>
    );
};

export { QuestUpdatingLayout };
