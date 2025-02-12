import { HttpType } from '@/common';
import { ICreateQuestTaskDto } from '@/models/requests';
import { IApiResponseDto, IQuestTaskDto } from '@/models/responses';
import { apiSlice } from '@/services';

export const questTasksApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        createQuestTask: builder.mutation<IApiResponseDto<IQuestTaskDto>, ICreateQuestTaskDto>({
            query: (requestDto: ICreateQuestTaskDto) => ({
                url: '/api/QuizTask',
                method: HttpType.POST,
                body: requestDto
            }),
        }),
        updateQuestTask: builder.mutation<IApiResponseDto<IQuestTaskDto>, IQuestTaskDto>({
            query: (requestDto: IQuestTaskDto) => ({
                url: '/api/QuizTask',
                method: HttpType.PUT,
                body: requestDto
            }),
        }),
        deleteQuestTask: builder.mutation<IApiResponseDto<boolean>, string>({
            query: (id: string) => ({
                url: `/api/QuizTask/${id}`,
                method: HttpType.DELETE
            }),
        }),
        getQuestTaskById: builder.query<IApiResponseDto<IQuestTaskDto>, string>({
            query: (id: string) => ({ url: `/api/QuizTask/getQuestTask/${id}` }),
        }),
    })
});

export const {
    useCreateQuestTaskMutation,
    useUpdateQuestTaskMutation,
    useDeleteQuestTaskMutation,
    useGetQuestTaskByIdQuery
} = questTasksApiSlice;
