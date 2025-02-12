import { IQuestTaskDto } from '../quest-task';

interface IQuestResponseDto {
    id: string;
    questId: string;
    questTitle: string;
    questDescription: string;
    startedAt?: string;
    completedAt?: string;
    firstTask?: IQuestTaskDto;
    secondTask?: IQuestTaskDto;
}

export { type IQuestResponseDto };
