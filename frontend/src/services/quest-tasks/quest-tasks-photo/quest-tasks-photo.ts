import { HttpType } from '@/common';
import { IApiResponseDto, IFileDto } from '@/models/responses';
import { apiSlice } from '@/services';

export const questTasksPhotoApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        uploadQuestTaskPhoto: builder.mutation<IApiResponseDto<IFileDto>, { taskId: string, photoDto: FormData }>({
            query: ({ taskId, photoDto }) => ({
                url: `/api/QuizTask/uploadPhoto/${taskId}`,
                method: HttpType.PUT,
                body: photoDto
            }),
        }),
        deleteQuestTaskPhoto: builder.mutation<IApiResponseDto<null>, string>({
            query: (taskId) => ({
                url: `/api/QuizTask/deletePhoto/${taskId}`,
                method: HttpType.DELETE,
            }),
        }),
    })
});

export const { useUploadQuestTaskPhotoMutation, useDeleteQuestTaskPhotoMutation } = questTasksPhotoApiSlice;
