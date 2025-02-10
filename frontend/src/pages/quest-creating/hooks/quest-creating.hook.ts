import { useContext } from 'react';

import { QuestCreatingContext, QuestCreatingContextValues } from '@/pages/quest-creating/context';

const useQuestCreating = (): QuestCreatingContextValues => {
    const context = useContext(QuestCreatingContext);
    if (!context) {
        throw new Error('QuestCreatingContext must be used within a QuestCreatingProvider');
    }
    return context;
};

export { useQuestCreating };
