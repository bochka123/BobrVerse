interface ICreateProfileRequestDto {
    name: string;
}

interface IUpdateProfileRequestDto extends ICreateProfileRequestDto { }

export { type ICreateProfileRequestDto, type IUpdateProfileRequestDto };
