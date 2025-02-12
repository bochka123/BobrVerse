import { FC } from 'react';

import { WoodenContainer } from '@/components';

import styles from './quest-updating-content-empty.module.scss';

const QuestUpdatingContentEmpty: FC = () => {
    return (
        <WoodenContainer className={styles.container}>
            <h2>Choose task on left bar for editing</h2>
        </WoodenContainer>
    );
};

export { QuestUpdatingContentEmpty };
