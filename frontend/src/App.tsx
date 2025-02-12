import { FC } from 'react';
import { Provider } from 'react-redux';
import { RouterProvider } from 'react-router-dom';

import { useConnection } from '@/hooks';
import { router } from '@/router';
import { store } from '@/store';

import { ConnectionIdContext } from './context/hub';
import { PopoverProvider, ToastProvider } from './providers';

const App: FC = () => {
    const { connectionId } = useConnection();

    return (
        <Provider store={store}>
            <ConnectionIdContext.Provider value={connectionId}>
                <ToastProvider>
                    <PopoverProvider>
                        <RouterProvider router={router} />
                    </PopoverProvider>
                </ToastProvider>
            </ConnectionIdContext.Provider>
        </Provider>
    );
};

export default App;
