/* eslint-disable no-unused-vars */
import { ChangeEvent, FC } from 'react';

import { InputTypes } from '@/common';

import styles from './base-input.module.scss';

type BaseInputProps = {
    value: string | number | undefined;
    onChange: (value: string | number | undefined) => void;
    labelText?: string;
    placeholder?: string;
    type?: InputTypes;
}
const BaseInput: FC<BaseInputProps> = ({ value, onChange, labelText, placeholder, type = InputTypes.TEXT }) => {

    const handleChange = (e: ChangeEvent<HTMLInputElement>): void => {
        onChange && onChange(e.target.value);
    };

    return (
        <div>
            { labelText && <span className={styles.label}>{labelText}</span>}
            <div>
                <input
                    className={styles.button}
                    value={value}
                    onChange={handleChange}
                    placeholder={placeholder}
                    type={type}
                />
            </div>
        </div>
    );
};

export { BaseInput };
