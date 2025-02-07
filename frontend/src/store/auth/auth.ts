import { createSlice } from '@reduxjs/toolkit';

import { AuthState } from './types.ts';

const initialState: AuthState = {};

const authSlice = createSlice({
    name: 'auth',
    initialState: initialState,
    reducers: {
        setCredentials: (state, action) => {
            console.log('setting credentials:', action.payload);
        },
        logOut: (state) => {
            console.log('logging out');
        }
    }
});

export const { setCredentials, logOut } = authSlice.actions;
export default authSlice.reducer;
