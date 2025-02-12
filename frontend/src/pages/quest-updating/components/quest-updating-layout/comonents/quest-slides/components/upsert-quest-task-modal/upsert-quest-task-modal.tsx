import { Dispatch, FC, SetStateAction } from 'react';
import { Controller, SubmitErrorHandler, SubmitHandler, useForm } from 'react-hook-form';

import { TaskTypeEnum, ToastModeEnum } from '@/common';
import { BaseInput, Modal, SelectInput } from '@/components';
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
                    name={'taskType'}
                    rules={{ required: 'Task type field is required' }}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Task type:'}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter quest title...'}
                        />
                    )}
                />
            </form>
        </Modal>
    );
};

export { UpsertQuestTaskModal };
