import { HttpType } from '@/common';
import { ICreateQuestRatingDto, IQuestRatingDTO } from '@/models/requests';
import { IApiResponseDto } from '@/models/responses';

import { apiSlice } from '../api';

export const ratingsApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        rateQuest: builder.mutation<IApiResponseDto<null>, ICreateQuestRatingDto>({
            query: (requestDto: ICreateQuestRatingDto) => ({ 
                url: '/api/QuestRating',
                method: HttpType.POST,
                body: requestDto
            }),
        }),
        getQuestsRatings: builder.query<IApiResponseDto<IQuestRatingDTO[]>, { start: number, end: number }>({
            query: ({ start, end }) => ({
                url: `/api/QuestRating/quests/${start}/${end}`,
                method: 'GET',
            }),
        }), 
    })
});

export const { useRateQuestMutation, useGetQuestsRatingsQuery } = ratingsApiSlice;
