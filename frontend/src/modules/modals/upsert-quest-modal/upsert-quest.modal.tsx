import { Dispatch, FC, SetStateAction } from 'react';
import { Controller, SubmitErrorHandler, SubmitHandler, useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';

import { InputTypes, ToastModeEnum } from '@/common';
import { Modal } from '@/components';
import { BaseButton, BaseInput } from '@/components/primitives';
import { getFormErrorMessage } from '@/helpers';
import { useConnection, useToast } from '@/hooks';
import { ICreateQuestDto } from '@/models/requests';
import { useCreateQuestMutation } from '@/services';

import styles from './upsert-quest.modal.module.scss';

type FormNames = {
    title: string;
    description: string;
    xpForComplete: number;
    xpForSuccess: number;
    timeLimitMinutes?: number;
    timeLimitSeconds?: number;
}

type UpsertQuestModalProps = {
    visible: boolean;
    setVisible: Dispatch<SetStateAction<boolean>>;
}
const UpsertQuestModal: FC<UpsertQuestModalProps> = ({ visible, setVisible }) => {

    const [createQuest] = useCreateQuestMutation();
    const navigate = useNavigate();
    const { addToast } = useToast();
    const { connection } = useConnection();

    const { handleSubmit, control } = useForm<FormNames>();

    const onSubmit: SubmitHandler<FormNames> = (data): void => {
        const minutes = Number(data.timeLimitMinutes) || 0;
        const seconds = Number(data.timeLimitSeconds) || 0;
        const timeLimitInSeconds = minutes * 60 + seconds;

        const requestData: ICreateQuestDto = {
            title: data.title,
            description: data.description,
            xpForComplete: data.xpForComplete,
            xpForSuccess: data.xpForSuccess,
            timeLimitInSeconds,
        };

        createQuest(requestData)
            .unwrap()
            .then((data) => {
                navigate(`quests/edit/${data.data.id}`);
                if(connection != null)
                    connection.invoke('notifyCreated', data.data);
            })
            .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to create quest'));
    };

    const onError: SubmitErrorHandler<FormNames> = (error): void => {
        addToast(ToastModeEnum.ERROR, getFormErrorMessage(error));
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
                    rules={{ required: 'XP for complete is required', min: 0 }}
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
                    rules={{ required: 'XP for success is required', min: 0 }}
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
                
                <div className={styles.inputGroup}>
                    <Controller
                        control={control}
                        name={'timeLimitMinutes'}
                        rules={{ min: 0, max: 60 }}
                        render={({ field: { onChange, value } }) => (
                            <BaseInput
                                labelText={'Minutes limit:'}
                                type={InputTypes.NUMBER}
                                value={value}
                                onChange={onChange}
                                placeholder={'00'}
                            />
                        )}
                    />
                    <p>:</p>
                    <Controller
                        control={control}
                        name={'timeLimitSeconds'}
                        rules={{ min: 0, max: 60 }}
                        render={({ field: { onChange, value } }) => (
                            <BaseInput
                                labelText={'Seconds limit:'}
                                type={InputTypes.NUMBER}
                                value={value}
                                onChange={onChange}
                                placeholder={'00'}
                            />
                        )}
                    />
                </div>

                <BaseButton type={'submit'}>
                    Create
                </BaseButton>
            </form>
        </Modal>
    );
};

export { UpsertQuestModal };
