import React, { FC } from 'react';

import { Modal } from '@/components';

import { ModelContent } from './model-content';

type EditProfileProps = {
    visible: boolean;
    setVisible: React.Dispatch<React.SetStateAction<boolean>>;
}

const EditProfile: FC<EditProfileProps> = ({ visible, setVisible }) => {
    return (
        <Modal visible={visible} setVisible={setVisible} heading={'Settings'}>
            <ModelContent setVisible={setVisible} />
        </Modal>
    );
};

export { EditProfile };
