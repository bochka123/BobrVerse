import { useContext } from 'react';

import { QuestUpdatingContext, QuestUpdatingContextValues } from '../context';

const useQuestUpdating = (): QuestUpdatingContextValues => {
    const context = useContext(QuestUpdatingContext);
    if (!context) {
        throw new Error('QuestUpdatingContext must be used within a QuestUpdatingProvider');
    }
    return context;
};

export { useQuestUpdating };
