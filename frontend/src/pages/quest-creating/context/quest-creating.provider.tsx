import { FC, ReactNode, useState } from 'react';

import { QuestCreatingContext } from './quest-creating.context.tsx';
import { QuestType } from './types';

type QuestCreatingProviderProps = {
    children: ReactNode
}
const QuestCreatingProvider: FC<QuestCreatingProviderProps> = ({ children }) => {

    const [questSlides, setQuestSlides] = useState<QuestType[]>([]);

    const addSlide = (quest: QuestType): void => {
        setQuestSlides([...questSlides, quest]);
    };

    const removeSlide = (questId: string): void => {
        setQuestSlides(questSlides.filter((slide) => slide.id!== questId));
    };

    const updateSlide = (questId: string, updatedQuest: QuestType): void => {
        setQuestSlides(questSlides.map((slide) => slide.id === questId ? updatedQuest : slide));
    };

    return (
        <QuestCreatingContext.Provider value={
            {
                addSlide,
                removeSlide,
                updateSlide,
                questSlides
            }
        }>
            {children}
        </QuestCreatingContext.Provider>
    );
};

export { QuestCreatingProvider };
