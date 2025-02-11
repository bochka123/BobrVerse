import { createBrowserRouter } from 'react-router-dom';

import { AuthPage, MainPage, ProfilePage, QuestPage, QuestUpdatingPage } from '@/pages';
import { SlideContent } from '@/pages/quest-updating/components';
import { ProtectedRoute } from '@/router/protected-route.tsx';

export const router = createBrowserRouter([
    {
        element: <ProtectedRoute />,
        children: [
            {
                element: <MainPage />,
                children: [
                    {
                        path: '',
                        element: <ProfilePage />
                    },
                    {
                        path: 'quests/:questId',
                        element: <QuestPage />
                    },
                    {
                        path: 'quests/edit/:questId',
                        element: <QuestUpdatingPage />,
                        children: [
                            {
                                path: 'task/:slideId',
                                element: <SlideContent />
                            }
                        ]
                    },
                ]
            },
        ]
    },
    {
        path: 'auth',
        element: <AuthPage />,
    },
    {
        path: '*',
        element: <h1>Page is not found</h1>,
    }
]);
