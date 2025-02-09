import { IAuthRequestDto } from '@/models/requests';
import { IApiResponseDto } from '@/models/responses';
import { apiSlice } from '@/services';


export const authApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        login: builder.mutation<IApiResponseDto, IAuthRequestDto>({
            query: (data) => ({
                url: '/api/auth/login',
                method: 'POST',
                body: data,
            }),
        }),
        register: builder.mutation<IApiResponseDto, IAuthRequestDto>({
            query: (data) => ({
                url: '/api/auth/register',
                method: 'POST',
                body: data,
            }),
        }),
        logOut: builder.mutation<IApiResponseDto, void>({
            query: () => ({
                url: '/api/auth/register',
                method: 'POST',
            }),
        }),
    })
});

export const { useLoginMutation, useRegisterMutation, useLogOutMutation } = authApiSlice;
