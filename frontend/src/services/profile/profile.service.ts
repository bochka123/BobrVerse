import { HttpType } from '@/common';
import { IUpdateProfileRequestDto } from '@/models/requests';
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
        update: builder.mutation<IApiResponseDto<IProfileDto>, IUpdateProfileRequestDto>({
            query: (requestDto: IUpdateProfileRequestDto) => ({ 
                url: '/api/BobrProfile/update',
                method: HttpType.PUT,
                body: requestDto
            }),
        }),
        uploadPhoto: builder.mutation<IApiResponseDto<IFileDto>, FormData>({
            query: (photoDto: FormData) => ({ 
                url: '/api/BobrProfile/uploadPhoto',
                method: HttpType.PUT,
                body: photoDto
            }),
        }),
        deletePhoto: builder.mutation<IApiResponseDto<null>, void>({
            query: () => ({
                url: '/api/BobrProfile/deletePhoto',
                method: HttpType.DELETE,
            }),
        }),
    })
});

export const { useGetMyProfileQuery, useLazyGetMyProfileQuery, useGetLevelQuery, useUpdateMutation, useUploadPhotoMutation, useDeletePhotoMutation } = profileApiSlice;
