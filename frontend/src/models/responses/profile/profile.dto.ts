interface IProfileLevelDto {
    level: number;
    requiredXP: number;
    title: string;
    description: string;
}

interface IProfileDto {
    id: string;
    name: string;
    level: IProfileLevelDto;
    xp: number;
    logs: number;
}

export { type IProfileDto, type IProfileLevelDto };
