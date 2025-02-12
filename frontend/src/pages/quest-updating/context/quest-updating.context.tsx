/* eslint-disable no-unused-vars */
import { createContext } from 'react';

import { IQuestTaskDto } from '@/models/responses';

type TaskWithIdType = { id: string, task?: IQuestTaskDto };

interface QuestUpdatingContextValues {
    setupTasks: (tasks: TaskWithIdType[]) => void;
    addTask: (task: TaskWithIdType) => void
    removeTask: (taskId: string) => void;
    updateTask: (taskId: string, updatedQuest: IQuestTaskDto) => void;
    questTasks: TaskWithIdType[];
    getTaskById: (taskId: string) => IQuestTaskDto | undefined;
}

const QuestUpdatingContext = createContext<QuestUpdatingContextValues | undefined>(undefined);

export { QuestUpdatingContext, type QuestUpdatingContextValues };
