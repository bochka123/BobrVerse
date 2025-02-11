import { FC } from 'react';

import { BaseButton, WoodenContainer } from '@/components';

import styles from './quest-hints.module.scss';

type QuestHintsProps = {}
const QuestHints: FC<QuestHintsProps> = () => {
    return (
        <WoodenContainer className={styles.hintsContainer}>
            <div className={styles.hintsWrapper}>
                <h2>Objects</h2>
                <ul>
                    <li>tree</li>
                    <li>binary tree</li>
                </ul>
            </div>
            <div className={styles.hintsWrapper}>
                <h2>Commands</h2>
                <ul>
                    <li>go</li>
                    <li>play</li>
                    <li>set</li>
                </ul>
            </div>
            <BaseButton buttonClasses={styles.button}>
                Save
            </BaseButton>
        </WoodenContainer>
    );
};

export { QuestHints };
