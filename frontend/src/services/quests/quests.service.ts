
import { HttpType } from '@/common';
import { ICreateQuestDto } from '@/models/requests';
import { IApiResponseDto, IQuestDto } from '@/models/responses';
import { apiSlice } from '@/services';

export const questsApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        getMyQuests: builder.query<IApiResponseDto<IQuestDto[]>, void>({
            query: () => ({ url: '/api/Quest/my' }),
        }),
        getAvailableQuests: builder.query<IApiResponseDto<IQuestDto[]>, void>({
            query: () => ({ url: '/api/Quest/active' }),
        }),
        createQuest: builder.mutation<IApiResponseDto<IQuestDto>, ICreateQuestDto>({
            query: (requestDto: ICreateQuestDto) => ({ 
                url: '/api/Quest/create',
                method: HttpType.POST,
                body: requestDto
            }),
        }),
        getQuestById: builder.query<IApiResponseDto<IQuestDto>, string>({
            query: (questId: string) => ({ url: `/api/Quest/getQuestById/${questId}` }),
        }),
    })
});

export const { useGetMyQuestsQuery, useGetAvailableQuestsQuery, useCreateQuestMutation, useGetQuestByIdQuery } = questsApiSlice;
