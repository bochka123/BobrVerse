import { IQuestTaskDto } from '../quest-task/quest-task.dto';

interface IQuestTaskResponseDto {
    xpGained?: number;
    isFinished: boolean;
    success: boolean;
    errorMessage: string;
    currentTask?: IQuestTaskDto;
    nextTask?: IQuestTaskDto;
}

export { type IQuestTaskResponseDto };
