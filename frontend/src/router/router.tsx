import { createBrowserRouter } from 'react-router-dom';

import { AuthPage, MainPage, ProfilePage } from '@/pages';

export const router = createBrowserRouter([
    {
        element: <MainPage />,
        children: [
            {
                path: '',
                element: <ProfilePage />
            }
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
