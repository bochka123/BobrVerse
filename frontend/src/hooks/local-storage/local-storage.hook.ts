import { Dispatch, SetStateAction, useEffect, useState } from 'react';

const useLocalStorage = <T>(key: string, initialValue: T): [value: T, setValue: Dispatch<SetStateAction<T>>] => {
    const [value, setValue] = useState(() => {
        const storedValue = window.localStorage.getItem(key);
        return storedValue ? JSON.parse(storedValue) : initialValue;
    });

    useEffect(() => {
        window.localStorage.setItem(key, JSON.stringify(value));
    }, [key, value]);

    return [value, setValue];
};

export { useLocalStorage };
