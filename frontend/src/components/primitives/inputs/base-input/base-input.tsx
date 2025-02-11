import { ChangeEvent, FC } from 'react';

import styles from './base-input.module.scss';

type BaseInputProps = {
    value: string;
    onChange: (value: string) => void;
    labelText?: string;
    placeholder?: string;
    type: string;
}
const BaseInput: FC<BaseInputProps> = ({ value, onChange, labelText, placeholder, type }) => {

    const handleChange = (e: ChangeEvent<HTMLInputElement>): void => {
        onChange && onChange(e.target.value);
    };

    return (
        <div>
            { labelText && <span>{labelText}</span>}
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
