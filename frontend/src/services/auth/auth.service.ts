import { IAuthRequestDto } from '@/models/requests';
import { IApiResponseDto } from '@/models/responses';
import { apiSlice } from '@/services';


export const authApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        login: builder.mutation<IAuthRequestDto, IApiResponseDto>({
            query: (data) => ({
                url: '/api/auth/login',
                method: 'POST',
                body: data,
            }),
        }),
        register: builder.mutation<IAuthRequestDto, IApiResponseDto>({
            query: (data) => ({
                url: '/api/auth/register',
                method: 'POST',
                body: data,
            }),
        }),
        logOut: builder.mutation<void, IApiResponseDto>({
            query: () => ({
                url: '/api/auth/register',
                method: 'POST',
            }),
        }),
    })
});

export const { useLoginMutation, useRegisterMutation, useLogOutMutation } = authApiSlice;
