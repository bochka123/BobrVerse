import { FC, ReactNode } from 'react';

import styles from './wooden-container.module.scss';

type WoodenContainerProps = {
    children: ReactNode,
}
const WoodenContainer: FC<WoodenContainerProps> = ({ children }) => {
    return (
        <div className={styles.container}>
            {children}
        </div>
    );
};

export { WoodenContainer };
