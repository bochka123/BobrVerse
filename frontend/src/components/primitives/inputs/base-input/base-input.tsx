import { ChangeEvent, FC } from 'react';

type BaseInputProps = {
    value: string;
    onChange: (value: string) => void;
    labelText?: string;
}
const BaseInput: FC<BaseInputProps> = ({ value, onChange, labelText }) => {

    const handleChange = (e: ChangeEvent<HTMLInputElement>): void => {
        onChange && onChange(e.target.value);
    };

    return (
        <div>
            { labelText && <span>{labelText}</span>}
            <div>
                <input value={value} onChange={handleChange} />
            </div>
        </div>
    );
};

export { BaseInput };
