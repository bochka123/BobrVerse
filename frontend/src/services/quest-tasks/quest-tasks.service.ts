import { HttpType } from '@/common';
import { ICreateQuestTaskDto, IUpdateQuestTaskDto } from '@/models/requests';
import { IApiResponseDto, IQuestTaskDto, IQuestTaskTypeInfoDto } from '@/models/responses';
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
        updateQuestTask: builder.mutation<IApiResponseDto<IQuestTaskDto>, IUpdateQuestTaskDto>({
            query: (requestDto: IUpdateQuestTaskDto) => ({
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
        getTaskTypeInfos: builder.query<IApiResponseDto<IQuestTaskTypeInfoDto[]>, void>({
            query: () => ({ url: '/api/QuizTask/taskInfos' }),
        }),
        
    })
});

export const {
    useCreateQuestTaskMutation,
    useUpdateQuestTaskMutation,
    useDeleteQuestTaskMutation,
    useLazyGetQuestTaskByIdQuery,
    useGetTaskTypeInfosQuery,
} = questTasksApiSlice;
