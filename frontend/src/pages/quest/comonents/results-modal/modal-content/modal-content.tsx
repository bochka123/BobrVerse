import React, { FC } from 'react';

import { BaseButton } from '@/components';
import { IQuestTaskResponseDto } from '@/models/responses';

import styles from './modal-content.module.scss';

type ModalContentProp = {
    setVisible: React.Dispatch<React.SetStateAction<boolean>>;
    result?: IQuestTaskResponseDto;
    callback: () => void;
}

const ModalContent: FC<ModalContentProp> = ({ setVisible, result, callback }) => {

    const onClick = (): void => {
        setVisible(false);
        callback();
    };

  return (
    <div className={styles.resultsWrapper}>
        <p className={styles.description}>You finished this quest!</p>
        <p className={styles.description}>You've gained <span className={styles.xp}>{result?.xpGained || 0}XP</span></p>
        <BaseButton onClick={onClick}>Ok</BaseButton>
    </div>
  );
};

export { ModalContent };
