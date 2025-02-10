import { IQuestDto } from '../quests';

interface IQuizTaskDto {
    id: string;
    questId: string;
    quest: IQuestDto;
    taskStatuses: []; //change
    url?: string;
    taskType: string; //change
    description: string;
    isRequiredForNextStage: boolean;
    maxAttempts?: number;
    timeLimit?: number;
}

export { type IQuizTaskDto };
