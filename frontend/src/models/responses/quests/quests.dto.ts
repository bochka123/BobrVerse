import { QuestStatusEnum } from '@/common';

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
    status: QuestStatusEnum;
    timeLimitInSeconds?: number;
    url?: string;
    taskIds: string[];
}

export { type IQuestDto };
