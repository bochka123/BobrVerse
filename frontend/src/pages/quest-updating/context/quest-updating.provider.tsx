import { FC, ReactNode, useState } from 'react';

import { QuestUpdatingContext } from './quest-updating.context.tsx';
import { QuestType } from './types/index.ts';

type QuestUpdatingProviderProps = {
    children: ReactNode
}
const QuestUpdatingProvider: FC<QuestUpdatingProviderProps> = ({ children }) => {

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
        <QuestUpdatingContext.Provider value={
            {
                addSlide,
                removeSlide,
                updateSlide,
                questSlides
            }
        }>
            {children}
        </QuestUpdatingContext.Provider>
    );
};

export { QuestUpdatingProvider };
