import { FC, ReactNode, useState } from 'react';

import { IQuestTaskDto } from '@/models/responses';

import { QuestUpdatingContext } from './quest-updating.context.tsx';

type TaskWithIdType = { order: number, task?: IQuestTaskDto };

type QuestUpdatingProviderProps = {
    children: ReactNode
}
const QuestUpdatingProvider: FC<QuestUpdatingProviderProps> = ({ children }) => {

    const [questTasks, setQuestTasks] = useState<TaskWithIdType[]>([]);

    const addTask = (task: TaskWithIdType): void => {
        setQuestTasks([...questTasks, task]);
    };

    const removeTask = (taskOrder: number): void => {
        setQuestTasks(questTasks.filter(task => task.order !== taskOrder));
    };

    const updateTask = (taskOrder: number, updatedQuest: IQuestTaskDto): void => {
        setQuestTasks(questTasks.map(task => task.order === taskOrder
            ? { order: taskOrder, task: updatedQuest }
            : task
        ));
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
