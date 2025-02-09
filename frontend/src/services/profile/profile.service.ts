import { ICreateProfileRequestDto } from '@/models/requests';
import { IApiResponseDto, IProfileDto, IProfileLevelDto } from '@/models/responses';
import { apiSlice } from '@/services';

export const profileApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        getMyProfile: builder.query<IApiResponseDto<IProfileDto>, void>({
            query: () => ({ url: '/api/BobrProfile/myprofile' }),
        }),
        createProfile: builder.mutation<IApiResponseDto<IProfileDto>, ICreateProfileRequestDto>({
            query: (profile) => ({
                url: '/api/BobrProfile/create',
                method: 'POST',
                body: profile,
            }),
        }),
        getLevel: builder.query<IApiResponseDto<IProfileLevelDto>, void>({
            query: () => ({ url: '/api/BobrLevel' }),
        }),
    })
});

export const { useGetMyProfileQuery, useCreateProfileMutation, useGetLevelQuery } = profileApiSlice;
