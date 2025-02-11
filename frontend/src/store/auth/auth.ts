import { createSlice } from '@reduxjs/toolkit';

import { IProfileDto, IProfileLevelDto } from '@/models/responses';
import { RootState } from '@/store';

import { AuthState } from './types.ts';

const initialState: AuthState = {
    id: '',
    name: '',
    level: {
        level: 0,
        requiredXP: 0,
        title: '',
        description: '',
    },
    xp: 0,
    logs: 0,
    url: ''
};

const authSlice = createSlice({
    name: 'auth',
    initialState: initialState,
    reducers: {
        setProfile: (state, action) => {
            const { id, name, level, xp, logs } = action.payload as IProfileDto;
            state.id = id;
            state.name = name;
            state.level = level;
            state.xp = xp;
            state.logs = logs;
        },
        setUrl: (state, action) => {
            const { url } = action.payload as IProfileDto;
            state.url = url;
        },
        setLevel: (state, action) => {
            state.level = action.payload as IProfileLevelDto;
        },
        logOut: (state) => {
            state.id = initialState.id;
            state.name = initialState.name;
            state.level = initialState.level;
            state.xp = initialState.xp;
            state.logs = initialState.logs;
        }
    }
});

export const { setProfile, setUrl, setLevel, logOut } = authSlice.actions;
export default authSlice.reducer;
export const selectCurrentId = (state: RootState): string => state.auth.id;
export const selectCurrentLevel = (state: RootState): IProfileLevelDto => state.auth.level;
export const selectCurrentLogs = (state: RootState): number => state.auth.logs;
export const selectCurrentName = (state: RootState): string => state.auth.name;
export const selectCurrentUrl = (state: RootState): string | undefined => state.auth.url;
export const selectCurrentXP = (state: RootState): number => state.auth.xp;
