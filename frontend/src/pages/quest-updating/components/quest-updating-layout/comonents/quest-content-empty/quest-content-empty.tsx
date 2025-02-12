import { FC } from 'react';

import { WoodenContainer } from '@/components';

import styles from './quest-content-empty.module.scss';

type QuestContentEmptyProps = {}
const QuestContentEmpty: FC<QuestContentEmptyProps> = () => {
    return (
        <WoodenContainer className={styles.container}>
            <h2>Choose task on left bar for editing</h2>
        </WoodenContainer>
    );
};

export { QuestContentEmpty };
