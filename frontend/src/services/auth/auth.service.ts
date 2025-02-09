import { IAuthRequestDto, IGoogleAuthRequestDto } from '@/models/requests';
import { IApiResponseDto } from '@/models/responses';
import { apiSlice } from '@/services';


export const authApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        login: builder.mutation<IApiResponseDto<null>, IAuthRequestDto>({
            query: (data) => ({
                url: '/api/auth/login',
                method: 'POST',
                body: data,
            }),
        }),
        register: builder.mutation<IApiResponseDto<null>, IAuthRequestDto>({
            query: (data) => ({
                url: '/api/auth/register',
                method: 'POST',
                body: data,
            }),
        }),
        logOut: builder.mutation<IApiResponseDto<null>, void>({
            query: () => ({
                url: '/api/auth/register',
                method: 'POST',
            }),
        }),
        google: builder.mutation<IApiResponseDto<null>, IGoogleAuthRequestDto>({
            query: (data) => ({
                url: '/api/auth/google',
                method: 'POST',
                body: data
            }),
        }),
    })
});

export const { useLoginMutation, useRegisterMutation, useLogOutMutation, useGoogleMutation } = authApiSlice;
