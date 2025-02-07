import './App.css';

import { FC } from 'react';
import { Provider } from 'react-redux';

import { store } from './store';

const App: FC = () => {

    return (
        <Provider store={store}>
            <></>
        </Provider>
    );
};

export default App;
