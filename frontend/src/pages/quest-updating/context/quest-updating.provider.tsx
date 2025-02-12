import { FC, ReactNode, useState } from 'react';

import { IQuestTaskDto } from '@/models/responses';

import { QuestUpdatingContext } from './quest-updating.context.tsx';

type TaskWithIdType = { id: string, task?: IQuestTaskDto };

type QuestUpdatingProviderProps = {
    children: ReactNode
}
const QuestUpdatingProvider: FC<QuestUpdatingProviderProps> = ({ children }) => {

    const [questTasks, setQuestTasks] = useState<TaskWithIdType[]>([]);

    const setupTasks = (tasks: TaskWithIdType[]): void => {
        setQuestTasks(tasks);
    };

    const addTask = (task: TaskWithIdType): void => {
        setQuestTasks([...questTasks, task]);
    };

    const removeTask = (taskId: string): void => {
        setQuestTasks(questTasks.filter(task => task.id !== taskId));
    };

    const updateTask = (taskId: string, updatedQuest: IQuestTaskDto): void => {
        setQuestTasks(questTasks.map(task => task.id === taskId
            ? { id: taskId, task: updatedQuest }
            : task
        ));
    };

    return (
        <QuestUpdatingContext.Provider value={
            {
                setupTasks,
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
