interface ICreateQuestDto {
    title: string;
    description: string;
    xpForComplete: number;
    xpForSuccess: number;
    timeLimitInSeconds?: number;
}

export { type ICreateQuestDto };
