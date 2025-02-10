/* eslint-disable no-unused-vars */
import { ButtonHTMLAttributes, FC, FormEvent, MouseEvent,useState } from 'react';

import { useCommonButtonFunctions } from '../common';
import styles from './icon-button.module.scss';

type IconButtonProps = {
    color?: 'primary' | 'red' | 'orange' | 'green' | 'gray' | 'dark-gray',
    hoverColor?: 'primary' | 'red' | 'orange' | 'green' | 'gray' | 'dark-gray',
    size?: number,
    outline?: boolean,
    transparent?: boolean,
    onClick?: (
        e: MouseEvent<HTMLButtonElement> | FormEvent<HTMLFormElement>
    ) => void,
    disabled?: boolean,
} & ButtonHTMLAttributes<HTMLButtonElement>;

const IconButton: FC<IconButtonProps> = ({
                                             color = 'primary',
                                             hoverColor = color === 'primary' ? 'primary' : 'red',
                                             size = 20,
                                             outline = true,
                                             transparent = false,
                                             onClick,
                                             disabled,
                                             ...props
                                         }) => {

    const [isHovered, setIsHovered] = useState<boolean>(false);

    // eslint-disable-next-line @typescript-eslint/no-unsafe-call,@typescript-eslint/no-unsafe-assignment
    const { isFocused, handleFocus, handleBlur, handleClick } = useCommonButtonFunctions(props, onClick);

    const handleHover = (): void => {
        setIsHovered(true);
    };

    const handleLeave = (): void => {
        setIsHovered(false);
    };

    return (
        <button
            type={props.type ? props.type : 'button'}
            className={`
                ${styles.button}
                ${styles[color]}
                ${styles[`hover-${hoverColor}`]}
                ${styles[`outline-${String(outline)}`]}
                ${isFocused ? styles['focused'] : ''}
                ${transparent ? styles['transparent'] : ''}
                ${disabled ? styles.disabled : ''}
            `}
            /* eslint-disable-next-line @typescript-eslint/no-unsafe-call, @typescript-eslint/no-unsafe-return */
            onClick={(e) => handleClick(e)}
            /* eslint-disable-next-line @typescript-eslint/no-unsafe-assignment */
            onMouseDown={handleFocus}
            /* eslint-disable-next-line @typescript-eslint/no-unsafe-assignment */
            onMouseUp={handleBlur}
            onMouseLeave={() => {
                handleBlur();
                handleLeave();
            }}
            onMouseMove={handleHover}
            {...props}
        >
            icon
        </button>
    );
};

export { IconButton };
