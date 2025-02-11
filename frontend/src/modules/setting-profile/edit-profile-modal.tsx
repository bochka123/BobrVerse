import React, { FC } from 'react';

import { Modal } from '@/components';

import { ModalContent } from './modal-content';

type EditProfileModalProps = {
    visible: boolean;
    setVisible: React.Dispatch<React.SetStateAction<boolean>>;
}

const EditProfileModal: FC<EditProfileModalProps> = ({ visible, setVisible }) => {
    return (
        <Modal visible={visible} setVisible={setVisible} heading={'Settings'}>
            <ModalContent setVisible={setVisible} />
        </Modal>
    );
};

export { EditProfileModal };
