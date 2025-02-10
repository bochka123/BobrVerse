import { Dispatch, FC, SetStateAction } from 'react';

import { Modal } from '@/components';

import styles from './create-quest.modal.module.scss';

type CreateQuestModalProps = {
    visible: boolean;
    setVisible: Dispatch<SetStateAction<boolean>>;
}
const CreateQuestModal: FC<CreateQuestModalProps> = ({ visible, setVisible }) => {

    // const navigate = useNavigate();
    //navigate('quests/create')

    return (
        <Modal visible={visible} setVisible={setVisible} heading={'Create quest'}>
            <div className={styles.content}>

            </div>
        </Modal>
    );
};

export { CreateQuestModal };
