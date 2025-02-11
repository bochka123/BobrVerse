import { FC } from 'react';

import { BackButton } from '@/components';
import { QuestCreatingLayout } from '@/pages/quest-creating/components';

import { QuestCreatingProvider } from './context';

type QuestCreatingPageProps = {}
const QuestCreatingPage: FC<QuestCreatingPageProps> = () => {
    return (
        <>
            <BackButton />
            <QuestCreatingProvider>
                <QuestCreatingLayout />
            </QuestCreatingProvider>
        </>
    );
};

export { QuestCreatingPage };
