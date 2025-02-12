import { HttpType } from '@/common';
import { ICreateQuestTaskResponseDto } from '@/models/requests';
import { IApiResponseDto, IQuestTaskResponseDto } from '@/models/responses';

import { apiSlice } from '../api';

export const questTaskResponseApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        createQuestTaskResponse: builder.mutation<IApiResponseDto<IQuestTaskResponseDto>, ICreateQuestTaskResponseDto>({
            query: (requestData: ICreateQuestTaskResponseDto) => ({ 
                url: '/api/QuestTaskReposponse/create',
                method: HttpType.POST,
                body: requestData
            }),
        }),
    })
});

export const { useCreateQuestTaskResponseMutation } = questTaskResponseApiSlice;
