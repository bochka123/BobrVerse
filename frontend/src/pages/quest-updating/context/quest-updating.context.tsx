/* eslint-disable no-unused-vars */
import { createContext } from 'react';

import { QuestType } from './types';

interface QuestUpdatingContextValues {
    addSlide: (quest: QuestType) => void;
    removeSlide: (questId: string) => void;
    updateSlide: (questId: string, updatedQuest: QuestType) => void;
    questSlides: QuestType[];
}

const QuestUpdatingContext = createContext<QuestUpdatingContextValues | undefined>(undefined);

export { QuestUpdatingContext, type QuestUpdatingContextValues };
