import { ChangeEvent, FC } from 'react';

import { InputTypes } from '@/common';
import styles from './number-input.module.scss';

type NumberInputProps = {
    value: number;
    onChange: (value: number) => void;
    labelText?: string;
    placeholder?: string;
}
const NumberInput: FC<NumberInputProps> = ({ value, onChange, labelText, placeholder }) => {

    const handleChange = (e: ChangeEvent<HTMLInputElement>): void => {
        onChange && onChange(Number(e.target.value));
    };
    
    return (
        <div>
            {labelText && <span>{labelText}</span>}
            <div>
                <input
                    className={styles.button}
                    value={value}
                    onChange={handleChange}
                    placeholder={placeholder}
                    type={InputTypes.NUMBER}
                />
            </div>
        </div>
    );
};

export { NumberInput };
