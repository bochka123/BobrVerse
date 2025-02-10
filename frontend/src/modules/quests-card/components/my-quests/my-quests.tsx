import { FC } from 'react';

import { IQuestDto } from '@/models/responses';

import styles from './my-quests.module.scss';
import { MyQuestsItem } from './my-quests-item';

const MyQuests: FC = () => {

    const quests: IQuestDto[] = [
        {
            title: 'Quest 1',
            description: '',
            id: '',
            createdAt: '',
            updatedAt: '',
            tasks: [],
            questResponses: [],
            xpForComplete: 0,
            xpForSuccess: 0
        },
        {
            title: 'Quest 2',
            description: '',
            id: '',
            createdAt: '',
            updatedAt: '',
            tasks: [],
            questResponses: [],
            xpForComplete: 0,
            xpForSuccess: 0
        },
        {
            title: 'Quest 3',
            description: '',
            id: '',
            createdAt: '',
            updatedAt: '',
            tasks: [],
            questResponses: [],
            xpForComplete: 0,
            xpForSuccess: 0
        }
    ];

    return (
        <div className={styles.myQuestsWrapper}>
            <div className={styles.myQuestsContainer}>
                {
                    quests.map((x, key) => <MyQuestsItem quest={x} key={key} />)
                }
            </div>
        </div>
    );
};

export { MyQuests };
