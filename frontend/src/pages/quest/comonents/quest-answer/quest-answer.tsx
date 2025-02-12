import React, { FC } from 'react';
import Editor from 'react-simple-code-editor';

import { WoodenContainer } from '@/components';
import { highlight } from '@/helpers';
import { IResourceDto } from '@/models/requests';
import { IQuestTaskTypeInfoDto } from '@/models/responses';

import styles from './quest-answer.module.scss';

type QuestAnswerProps = {
    code: string;
    setCode: React.Dispatch<React.SetStateAction<string>>;
    taskType?: IQuestTaskTypeInfoDto;
    requiredResources?: IResourceDto[];
}

const QuestAnswer: FC<QuestAnswerProps> = ({ code, setCode, taskType, requiredResources }) => {
    return (
        <WoodenContainer className={styles.answerContainer}>
            <div className={styles.editorWrapper}>
                <Editor
                    padding={5}
                    highlight={code => highlight(
                        code,
                        requiredResources?.map(res => res.name) || [],
                        taskType?.keywords || []
                    )}
                    onValueChange={setCode}
                    value={code}
                    placeholder={'Enter your code here...'}
                />
            </div>
        </WoodenContainer>
    );
};

export { QuestAnswer };
