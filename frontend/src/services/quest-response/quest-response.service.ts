import { HttpType } from '@/common';
import { IApiResponseDto, IQuestResponseDto } from '@/models/responses';

import { apiSlice } from '../api';

export const questResponseApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        getUserQuestResponses: builder.query<IApiResponseDto<IQuestResponseDto[]>, { startIndex: number, endIndex: number }>({
            query: (requestData: { startIndex: number, endIndex: number }) => 
                ({ url: `/api/QuestResponse/getUserQuestResponses/${requestData.startIndex}/${requestData.endIndex}` }),
        }),
        createQuestResponse: builder.mutation<IApiResponseDto<IQuestResponseDto>, string>({
            query: (questId: string) => ({ 
                url: `/api/QuestResponse/create?questId=${questId}`,
                method: HttpType.POST
            }),
        }),
    })
});

export const { useGetUserQuestResponsesQuery, useCreateQuestResponseMutation } = questResponseApiSlice;
