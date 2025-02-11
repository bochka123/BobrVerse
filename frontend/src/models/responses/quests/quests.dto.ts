import { IProfileDto } from '../profile';
import { IQuestResponseDto } from '../quest-response';
import { IQuizTaskDto } from '../quiz-task';

interface IQuestDto {
    id: string;
    authorId?: string;
    author?: IProfileDto;
    title: string;
    description: string;
    createdAt: string;
    updatedAt: string;
    tasks: IQuizTaskDto[];
    questResponses: IQuestResponseDto[];
    xpForComplete: number;
    xpForSuccess: number;
    status: string;
    timeLimitInSeconds?: number;
    url?: string;
}

export { type IQuestDto };
