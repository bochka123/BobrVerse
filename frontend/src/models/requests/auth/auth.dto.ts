interface IAuthRequestDto {
    email: string;
    password: string;
}

interface IGoogleAuthRequestDto {
    credential: string;
}

export { type IAuthRequestDto, type IGoogleAuthRequestDto };
