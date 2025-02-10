import { IApiResponseDto, IFileDto, IProfileDto, IProfileLevelDto } from '@/models/responses';
import { apiSlice } from '@/services';

export const profileApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        getMyProfile: builder.query<IApiResponseDto<IProfileDto>, void>({
            query: () => ({ url: '/api/BobrProfile/myprofile' }),
        }),
        getLevel: builder.query<IApiResponseDto<IProfileLevelDto>, void>({
            query: () => ({ url: '/api/BobrLevel' }),
        }),
        uploadPhoto: builder.mutation<IApiResponseDto<IFileDto>, FormData>({
            query: (photoDto: FormData) => ({ 
                url: '/api/BobrProfile/UploadPhoto',
                method: 'POST',
                body: photoDto
            }),
        }),
    })
});

export const { useGetMyProfileQuery, useGetLevelQuery, useUploadPhotoMutation } = profileApiSlice;
