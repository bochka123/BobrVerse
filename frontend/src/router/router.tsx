import { createBrowserRouter } from 'react-router-dom';

export const router = createBrowserRouter([
    {
        path: '*',
        element: <h1>Page is not found</h1>,
    }
]);
