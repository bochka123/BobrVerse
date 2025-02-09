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

interface IFileDto {
    url: string;
}

export { type IFileDto, type IProfileDto, type IProfileLevelDto };
