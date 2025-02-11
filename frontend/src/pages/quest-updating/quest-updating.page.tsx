import { FC } from 'react';

import { BackButton } from '@/components';

import { QuestUpdatingLayout } from './components';
import { QuestUpdatingProvider } from './context';

type QuestUpdatingPageProps = {}
const QuestUpdatingPage: FC<QuestUpdatingPageProps> = () => {
    return (
        <>
            <BackButton />
            <QuestUpdatingProvider>
                <QuestUpdatingLayout />
            </QuestUpdatingProvider>
        </>
    );
};

export { QuestUpdatingPage };
