/* eslint-disable no-unused-vars */
import { createContext } from 'react';

import { IQuestTaskDto } from '@/models/responses';

type TaskWithIdType = { order: number, task?: IQuestTaskDto };

interface QuestUpdatingContextValues {
    addTask: (task: TaskWithIdType) => void
    removeTask: (taskOrder: number) => void;
    updateTask: (taskOrder: number, updatedQuest: IQuestTaskDto) => void;
    questTasks: TaskWithIdType[];
}

const QuestUpdatingContext = createContext<QuestUpdatingContextValues | undefined>(undefined);

export { QuestUpdatingContext, type QuestUpdatingContextValues };
