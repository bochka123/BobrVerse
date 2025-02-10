import { FC } from 'react';

import { QuestCreatingLayout } from '@/pages/quest-creating/components';

import { QuestCreatingProvider } from './context';

type QuestCreatingPageProps = {}
const QuestCreatingPage: FC<QuestCreatingPageProps> = () => {

    return (
        <QuestCreatingProvider>
            <QuestCreatingLayout />
        </QuestCreatingProvider>
    );
};

export { QuestCreatingPage };
