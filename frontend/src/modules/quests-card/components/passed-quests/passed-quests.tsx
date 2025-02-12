import { FC } from 'react';

import { IQuestDto } from '@/models/responses';

import styles from './passed-quests.module.scss';
import { PassedQuestsItem } from './passed-quests-item';

const PassedQuests: FC = () => {
    const quests: IQuestDto[] = [
        {
            title: 'Quest 1',
            description: '',
            id: '',
            createdAt: '',
            updatedAt: '',
            tasks: [],
            xpForComplete: 0,
            xpForSuccess: 0,
            status: ''
        },
        {
            title: 'Quest 2',
            description: '',
            id: '',
            createdAt: '',
            updatedAt: '',
            tasks: [],
            xpForComplete: 0,
            xpForSuccess: 0,
            status: ''
        }
    ];

    return (
        <div className={styles.passedQuestsWrapper}>
            <div className={styles.passedQuestsContainer}>
                {
                    quests.map((x, key) => <PassedQuestsItem quest={x} key={`quest-${key}`} />)
                }
            </div>
        </div>
    );
};

export { PassedQuests };
