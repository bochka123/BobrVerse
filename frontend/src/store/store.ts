import { configureStore } from '@reduxjs/toolkit';

const store = configureStore({
    reducer: {

    }
});

type RootState = ReturnType<typeof store.getState>;

export { type RootState, store };
