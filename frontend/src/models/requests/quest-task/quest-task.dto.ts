import { TaskTypeEnum } from '@/common';

import { IResourceDto } from '../resource';

interface ICreateQuestTaskDto {
    questId: string;
    taskType: TaskTypeEnum;
    shortDescription?: string;
    description: string;
    isRequiredForNextStage: boolean;
    maxAttempts?: number;
    timeLimitInSeconds?: number;
    isTemplate: boolean;
    requiredResources: IResourceDto[];
    codeTemplate?: string;
}


export { type ICreateQuestTaskDto };
