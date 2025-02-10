import { FC, ReactNode } from 'react';

import styles from './wooden-container.module.scss';

type WoodenContainerProps = {
    children: ReactNode,
    className?: string,
}
const WoodenContainer: FC<WoodenContainerProps> = ({ children, className }) => {
    return (
        <div className={`${styles.container} ${className}`}>
            {children}
        </div>
    );
};

export { WoodenContainer };
