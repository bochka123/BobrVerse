import { ChangeEventHandler, FC } from 'react';

import styles from './checkbox-input.module.scss';

type CheckboxInputProps = {
    value: string;
    onChange: ChangeEventHandler<HTMLInputElement>;
    labelText?: string;
}
const CheckboxInput: FC<CheckboxInputProps> = ({ value, onChange, labelText }) => {

    return (
        <div className={styles.checkboxWrapper}>
            { labelText && <span className={styles.label}>{labelText}</span>}
            <input type={'checkbox'} value={value} onChange={onChange} className={styles.checkbox} />
        </div>
    );
};

export { CheckboxInput };
