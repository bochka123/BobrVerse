/* eslint-disable no-unused-vars */
import { createContext } from 'react';

import { IQuestTaskDto } from '@/models/responses';

interface QuestUpdatingContextValues {
    addTask: (quest: IQuestTaskDto) => void;
    removeTask: (questId: string) => void;
    updateTask: (questId: string, updatedQuest: IQuestTaskDto) => void;
    questTasks: IQuestTaskDto[];
}

const QuestUpdatingContext = createContext<QuestUpdatingContextValues | undefined>(undefined);

export { QuestUpdatingContext, type QuestUpdatingContextValues };
