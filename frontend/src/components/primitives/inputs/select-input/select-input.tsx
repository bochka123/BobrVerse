import { ChangeEventHandler, FC, useId } from 'react';

import styles from './select-input.module.scss';

type SelectInputProps = {
    value: string;
    onChange: ChangeEventHandler<HTMLSelectElement>;
    options: { name: string, value: string }[];
    labelText?: string;
}
const SelectInput: FC<SelectInputProps> = ({ value, onChange, options, labelText }) => {
    const id = useId();

    return (
        <div className={styles.selectWrapper}>
            { labelText && <span className={styles.label}>{labelText}</span>}
            <select value={value} onChange={onChange} className={styles.select}>
                {
                    [{ name: 'Не вибрано', value: '' }, ...options].map((option, index) => (
                        <option key={`select-option-${id}-${index}`} value={option.value} defaultValue={undefined}>
                            {option.name}
                        </option>
                    ))
                }
            </select>
        </div>
    );
};

export { SelectInput };
