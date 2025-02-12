import { FC, ReactNode, useState } from 'react';

import { IQuestTaskDto } from '@/models/responses';

import { QuestUpdatingContext } from './quest-updating.context.tsx';

type QuestUpdatingProviderProps = {
    children: ReactNode
}
const QuestUpdatingProvider: FC<QuestUpdatingProviderProps> = ({ children }) => {

    const [questTasks, setQuestTasks] = useState<IQuestTaskDto[]>([]);

    const addTask = (quest: IQuestTaskDto): void => {
        setQuestTasks([...questTasks, quest]);
    };

    const removeTask = (questId: string): void => {
        setQuestTasks(questTasks.filter((slide) => slide.id!== questId));
    };

    const updateTask = (questId: string, updatedQuest: IQuestTaskDto): void => {
        setQuestTasks(questTasks.map((slide) => slide.id === questId ? updatedQuest : slide));
    };

    return (
        <QuestUpdatingContext.Provider value={
            {
                addTask,
                removeTask,
                updateTask,
                questTasks,
            }
        }>
            {children}
        </QuestUpdatingContext.Provider>
    );
};

export { QuestUpdatingProvider };
