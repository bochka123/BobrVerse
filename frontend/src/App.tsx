import { FC } from 'react';
import { Provider } from 'react-redux';
import { RouterProvider } from 'react-router-dom';

import { router } from '@/router';
import { store } from '@/store';

import { PopoverProvider } from './providers';

const App: FC = () => {

    return (
        <Provider store={store}>
            <PopoverProvider>
                    <RouterProvider router={router} />
            </PopoverProvider>
        </Provider>
    );
};

export default App;
