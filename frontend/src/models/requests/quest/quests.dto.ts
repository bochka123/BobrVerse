import { QuestStatusEnum } from '@/common';

interface ICreateQuestDto {
    title: string;
    description: string;
    xpForComplete: number;
    xpForSuccess: number;
    timeLimitInSeconds?: number;
}

interface IUpdateQuestDto extends ICreateQuestDto {
    id: string;
    status: QuestStatusEnum;
}

export { type ICreateQuestDto, type IUpdateQuestDto };
