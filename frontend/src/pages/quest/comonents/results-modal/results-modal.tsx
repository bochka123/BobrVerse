import React, { FC } from 'react';

import { Modal } from '@/components';
import { IQuestTaskResponseDto } from '@/models/responses';

import { ModalContent } from './modal-content';

type ResultsModalProps = {
    visible: boolean;
    setVisible: React.Dispatch<React.SetStateAction<boolean>>;
    result?: IQuestTaskResponseDto;
    callback: () => void;
}

const ResultsModal: FC<ResultsModalProps> = ({ visible, setVisible, result, callback }) => {
  return (
    <Modal visible={visible} setVisible={setVisible} heading={'Results'}>
        <ModalContent setVisible={setVisible} result={result} callback={callback} />
    </Modal>
  );
};

export { ResultsModal };
