import React, { FC } from 'react';
import Editor from 'react-simple-code-editor';

import { WoodenContainer } from '@/components';
import { highlight } from '@/helpers';

import styles from './quest-answer.module.scss';

type QuestAnswerProps = {
    code: string;
    setCode: React.Dispatch<React.SetStateAction<string>>;
}

const QuestAnswer: FC<QuestAnswerProps> = ({ code, setCode }) => {
    return (
        <WoodenContainer className={styles.answerContainer}>
            <div className={styles.editorWrapper}>
                <Editor
                    padding={5}
                    highlight={code => highlight(code)}
                    onValueChange={setCode}
                    value={code}
                    placeholder={'Enter your code here...'}
                />
            </div>
        </WoodenContainer>
    );
};

export { QuestAnswer };
