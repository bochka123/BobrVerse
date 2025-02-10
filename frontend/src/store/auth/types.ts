import { IProfileLevelDto } from '@/models/responses';

interface AuthState {
    id: string;
    name: string;
    level: IProfileLevelDto;
    xp: number;
    logs: number;
}

export { type AuthState };
