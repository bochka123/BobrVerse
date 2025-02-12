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
    requiredResources: IResourceDto[];
    codeTemplate?: string;
    order: number;
}

export { type IQuestTaskDto };
