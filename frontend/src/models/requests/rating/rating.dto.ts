interface ICreateQuestRatingDto {
    questResponseId: string;
    comment: string;
    rating: number;
}

interface IQuestRatingDTO{
    rating: number;
    authorName: string;
    questTitle: string;
    averageRating: number;
    votesCount: number;
}

export { type ICreateQuestRatingDto, type IQuestRatingDTO };
