import { createContext } from 'react';

import { QuestType } from './types';

interface QuestCreatingContextValues {
    addSlide: (quest: QuestType) => void;
    removeSlide: (questId: string) => void;
    updateSlide: (questId: string, updatedQuest: QuestType) => void;
    questSlides: QuestType[];
}

const QuestCreatingContext = createContext<QuestCreatingContextValues | undefined>(undefined);

export { QuestCreatingContext, type QuestCreatingContextValues };
