import { createBrowserRouter } from 'react-router-dom';

import { AuthPage } from '@/pages';

export const router = createBrowserRouter([
    {
        path: 'auth',
        element: <AuthPage />,
    },
    {
        path: '*',
        element: <h1>Page is not found</h1>,
    }
]);
