import { IProfileDto } from '../profile';
import { IQuestTaskDto } from '../quest-task';

interface IQuestDto {
    id: string;
    authorId?: string;
    author?: IProfileDto;
    title: string;
    description: string;
    createdAt: string;
    updatedAt: string;
    tasks: IQuestTaskDto[];
    xpForComplete: number;
    xpForSuccess: number;
    status: string;
    timeLimitInSeconds?: number;
    url?: string;
}

export { type IQuestDto };
