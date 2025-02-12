import { HttpType } from '@/common';
import { ICreateQuestRatingDto } from '@/models/requests';
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
    })
});

export const { useRateQuestMutation } = ratingsApiSlice;
