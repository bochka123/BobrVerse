import { Dispatch, FC, SetStateAction } from 'react';
import { Controller, SubmitErrorHandler, SubmitHandler, useForm } from 'react-hook-form';

import { TaskTypeEnum, ToastModeEnum } from '@/common';
import { BaseInput, CheckboxInput, Modal, SelectInput } from '@/components';
import { getFormErrorMessage } from '@/helpers';
import { useToast } from '@/hooks';

import styles from './upsert-quest-task-modal.module.scss';

type FormNames = {
    taskType: string;
    shortDescription: string;
    description: string;
    isRequiredForNextStage: boolean;
    maxAttempts: number;
    timeLimitInSeconds: number;
    isTemplate: boolean;
}

type UpsertQuestTaskModalProps = {
    visible: boolean;
    setVisible: Dispatch<SetStateAction<boolean>>
}
const UpsertQuestTaskModal: FC<UpsertQuestTaskModalProps> = ({ visible, setVisible }) => {

    const { addToast } = useToast();

    const { handleSubmit, control } = useForm<FormNames>();

    const onSubmit: SubmitHandler<FormNames> = (data): void => {
        
    };

    const onError: SubmitErrorHandler<FormNames> = (error): void => {
        addToast(ToastModeEnum.ERROR, getFormErrorMessage(error));
    };
    
    return (
        <Modal visible={visible} setVisible={setVisible} heading={'Create quest task'}>
            <form onSubmit={handleSubmit(onSubmit, onError)} className={styles.form}>
                <Controller
                    control={control}
                    name={'taskType'}
                    rules={{ required: 'Task type field is required' }}
                    render={({ field: { onChange, value } }) => (
                        <SelectInput
                            labelText={'Task type:'}
                            value={value}
                            onChange={onChange}
                            options={Object.entries(TaskTypeEnum).map(([_, value]) => value.toString())}
                        />
                    )}
                />
                <Controller
                    control={control}
                    name={'shortDescription'}
                    rules={{ required: 'Short description field is required' }}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Short description:'}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter quest short description...'}
                        />
                    )}
                />
                <Controller
                    control={control}
                    name={'description'}
                    rules={{ required: 'Description field is required' }}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Description:'}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter quest description...'}
                        />
                    )}
                />
                <Controller
                    control={control}
                    name={'isRequiredForNextStage'}
                    rules={{ required: 'Is required for next stage field is required' }}
                    render={({ field: { onChange, value } }) => (
                        <CheckboxInput
                            labelText={'Is required for next stage:'}
                            value={value}
                            onChange={onChange}
                        />
                    )}
                />
            </form>
        </Modal>
    );
};

export { UpsertQuestTaskModal };
