import { IQuestTaskDto } from '../quest-task';

interface IQuestResponseDto {
    id: string;
    questId: string;
    questTitle: string;
    questDescription: string;
    startedAt?: string;
    completedAt?: string;
    currentTask?: IQuestTaskDto;
    nextTask?: IQuestTaskDto;
}

export { type IQuestResponseDto };
