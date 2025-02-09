import { IProfileDto } from '../profile';
import { IQuestDto } from '../quests';

interface IQuestResponseDto {
    id: string;
    questId: string;
    quest: IQuestDto;
    profileId: string;
    profile: IProfileDto;
    questTitle: string;
    questDescription: string;
    xpEarned: number;
    isCompleted: boolean;
    taskStatuses: []; //change
    totalXp: number;
    startedAt: string;
    completedAt?: string;
}

export { type IQuestResponseDto };
