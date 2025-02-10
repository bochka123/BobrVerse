import { useLocalStorage } from '@/hooks';

type ReturnType = {
    isAuthenticated: boolean;
    logIn: () => void;
    logOut: () => void;
}
const useAuth = (): ReturnType => {

    const [value, setValue] = useLocalStorage<boolean>('isAuthenticated', false);

    return {
        isAuthenticated: value,
        logIn: () => setValue(true),
        logOut: () => setValue(false),
    };
};

export { useAuth };
