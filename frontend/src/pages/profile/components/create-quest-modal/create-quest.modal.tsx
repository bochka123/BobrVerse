import { Dispatch, FC, SetStateAction } from 'react';
import { Controller, SubmitHandler, useForm } from 'react-hook-form';

import { Modal } from '@/components';
import { BaseInput } from '@/components/primitives';

import styles from './create-quest.modal.module.scss';

type FormNames = {
    name: string;
}

type CreateQuestModalProps = {
    visible: boolean;
    setVisible: Dispatch<SetStateAction<boolean>>;
}
const CreateQuestModal: FC<CreateQuestModalProps> = ({ visible, setVisible }) => {

    // const navigate = useNavigate();
    //navigate('quests/create')

    const { handleSubmit, control } = useForm<FormNames>();

    const onSubmit: SubmitHandler<FormNames> = (data): void => {
        console.log(data);
    };

    const onError = (error: any): void => {
        console.error('Quest creating failed:', error);
    };

    return (
        <Modal visible={visible} setVisible={setVisible} heading={'Create quest'}>
            <form onSubmit={handleSubmit(onSubmit, onError)} className={styles.form}>
                <Controller
                    control={control}
                    name={'name'}
                    rules={{ required: 'Name field is required' }}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Quest name:'}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter quest name...'}
                        />
                    )}
                />
            </form>
        </Modal>
    );
};

export { CreateQuestModal };
