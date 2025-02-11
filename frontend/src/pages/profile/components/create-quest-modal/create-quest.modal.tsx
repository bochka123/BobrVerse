import { Dispatch, FC, SetStateAction } from 'react';
import { Controller, SubmitHandler, useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';

import { InputTypes, ToastModeEnum } from '@/common';
import { Modal } from '@/components';
import { BaseButton, BaseInput } from '@/components/primitives';
import { useToast } from '@/hooks';
import { ICreateQuestDto } from '@/models/requests';
import { useCreateQuestMutation } from '@/services';

import styles from './create-quest.modal.module.scss';

type FormNames = {
    title: string;
    description: string;
    xpForComplete: number;
    xpForSuccess: number;
    timeLimitInSeconds?: number;
}

type CreateQuestModalProps = {
    visible: boolean;
    setVisible: Dispatch<SetStateAction<boolean>>;
}
const CreateQuestModal: FC<CreateQuestModalProps> = ({ visible, setVisible }) => {

    const [createQuest] = useCreateQuestMutation();
    const navigate = useNavigate();
    const { addToast } = useToast();

    const { handleSubmit, control } = useForm<FormNames>();

    const onSubmit: SubmitHandler<FormNames> = (data): void => {
        const requestData: ICreateQuestDto = {
            title: data.title,
            description: data.description,
            xpForComplete: data.xpForComplete,
            xpForSuccess: data.xpForSuccess,
            timeLimitInSeconds: data.timeLimitInSeconds,
        };

        createQuest(requestData)
            .unwrap()
            .then((data) => {
                navigate(`quests/edit/${data.data.id}`);
            })
            .catch(() => addToast(ToastModeEnum.ERROR, 'Faled to create quest'));
    };

    const onError = (error: any): void => {
        console.error('Quest creating failed:', error);
    };

    return (
        <Modal visible={visible} setVisible={setVisible} heading={'Create quest'}>
            <form onSubmit={handleSubmit(onSubmit, onError)} className={styles.form}>
                <Controller
                    control={control}
                    name={'title'}
                    rules={{ required: 'Title field is required' }}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Quest title:'}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter quest title...'}
                        />
                    )}
                />

                <Controller
                    control={control}
                    name={'description'}
                    rules={{ required: 'Description field is required' }}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Quest description:'}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter quest description...'}
                        />
                    )}
                />

                <Controller
                    control={control}
                    name={'xpForComplete'}
                    rules={{ required: 'XP for complete is required' }}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Quest XP for complete:'}
                            type={InputTypes.NUMBER}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter quest XP for complete...'}
                        />
                    )}
                />

                <Controller
                    control={control}
                    name={'xpForSuccess'}
                    rules={{ required: 'XP for success is required' }}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Quest XP for success:'}
                            type={InputTypes.NUMBER}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter quest XP for success...'}
                        />
                    )}
                />

                <Controller
                    control={control}
                    name={'timeLimitInSeconds'}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Quest time limit:'}
                            type={InputTypes.NUMBER}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter quest time limit (in seconds)'}
                        />
                    )}
                />

                <BaseButton type={'submit'}>
                    Create
                </BaseButton>
            </form>
        </Modal>
    );
};

export { CreateQuestModal };
