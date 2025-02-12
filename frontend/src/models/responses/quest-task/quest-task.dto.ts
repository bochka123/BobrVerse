import { IResourceDto } from '@/models/requests';

interface IQuestTaskDto {
    id: string;
    taskType: string;
    url?: string;
    shortDescription?: string;
    description: string;
    isRequiredForNextStage: boolean;
    maxAttempts?: number;
    timeLimitInSeconds?: number;
    isTemplate: boolean;
    order: number;
    requiredResources: IResourceDto[];
    maxCollectCalls?: number;
    nextTaskId?: string;
}

interface IQuestTaskTypeInfoDto {
    name: string;
    taskType: string;
    description: string;
    parameters: Record<string, string>;
}

export { type IQuestTaskDto, type IQuestTaskTypeInfoDto };
