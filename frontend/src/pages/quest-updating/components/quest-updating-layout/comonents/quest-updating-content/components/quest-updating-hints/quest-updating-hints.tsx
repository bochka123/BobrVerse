import { faSave } from '@fortawesome/free-solid-svg-icons';
import { FC } from 'react';

import { IconButton, WoodenContainer } from '@/components';

import styles from './quest-updating-hints.module.scss';

type QuestUpdatingHintsProps = {}
const QuestUpdatingHints: FC<QuestUpdatingHintsProps> = () => {
    return (
        <WoodenContainer className={styles.hintsContainer}>
            <div className={styles.innerContainer}>
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
            </div>
            <IconButton icon={faSave}/>
        </WoodenContainer>
    );
};

export { QuestUpdatingHints };
