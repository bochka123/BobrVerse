import { FC, useEffect } from 'react';
import { Outlet, useParams } from 'react-router-dom';

import { Loader } from '@/components';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';
import { useGetQuestByIdQuery } from '@/services';

import { QuestUpdatingTasks } from './comonents';
import styles from './quest-updating.layout.module.scss';

type QuestUpdatingLayoutProps = {}
const QuestUpdatingLayout: FC<QuestUpdatingLayoutProps> = () => {
    
    const { questId } = useParams();
    const { data: questData, isLoading: isQuestLoading } = useGetQuestByIdQuery(questId as string);
    
    const { setupTasks } = useQuestUpdating();

    useEffect(() => {
        if (questData?.data) {
            setupTasks(questData.data.taskIds.map(taskId => ({ id: taskId })));
        }
    }, [questData?.data.taskIds]);

    if (isQuestLoading) return <Loader size={30} />;
    
    return (
        <div className={styles.main}>
            <QuestUpdatingTasks />
            <Outlet />
        </div>
    );
};

export { QuestUpdatingLayout };
