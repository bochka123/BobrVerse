import { Dispatch, FC, SetStateAction } from 'react';
import { Controller, SubmitErrorHandler, SubmitHandler, useForm } from 'react-hook-form';

import { InputTypes, TaskTypeEnum, ToastModeEnum } from '@/common';
import { BaseButton, BaseInput, CheckboxInput, Modal, SelectInput } from '@/components';
import { getFormErrorMessage } from '@/helpers';
import { useToast } from '@/hooks';
import { ICreateQuestTaskDto } from '@/models/requests';
import { useCreateQuestTaskMutation } from '@/services';

import styles from './upsert-quest-task-modal.module.scss';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';

type FormNames = {
    taskType: string;
    shortDescription: string;
    description: string;
    isRequiredForNextStage: string;
    maxAttempts: number;
    timeLimitInSeconds: number;
    isTemplate: string;
}

type UpsertQuestTaskModalProps = {
    visible: boolean;
    setVisible: Dispatch<SetStateAction<boolean>>;
    questId: string;
}
const UpsertQuestTaskModal: FC<UpsertQuestTaskModalProps> = ({ visible, setVisible, questId }) => {

    const { addToast } = useToast();
    
    const [createTask] = useCreateQuestTaskMutation();

    const { addSlide } = useQuestUpdating();

    const { handleSubmit, control } = useForm<FormNames>();

    const onSubmit: SubmitHandler<FormNames> = (data): void => {
        const requestData: ICreateQuestTaskDto = {
            questId: questId,
            isRequiredForNextStage: !!data.isRequiredForNextStage,
            isTemplate: !!data.isTemplate,
            maxAttempts: data.maxAttempts,
            timeLimitInSeconds: data.timeLimitInSeconds,
            taskType: data.taskType as TaskTypeEnum,
            shortDescription: data.shortDescription,
            description: data.description,
            requiredResources: [],
        };

        createTask(requestData)
            .unwrap()
            .then((data) => {
                addSlide({
                    id: data.data.id,
                    title: data.data.shortDescription,
                    content: data.data.description,
                });
                setVisible(false);
            })
            .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to create quest'));
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
                            options={Object.entries(TaskTypeEnum).map(([key, value]) => ({
                                name: value,
                                value: key,
                            }))}
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
                            placeholder={'Enter short description...'}
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
                            placeholder={'Enter description...'}
                        />
                    )}
                />
                <Controller
                    control={control}
                    name={'maxAttempts'}
                    rules={{ required: 'Max attempts field is required' }}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Max attempts:'}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter max attempts...'}
                            type={InputTypes.NUMBER}
                            min={0}
                        />
                    )}
                />
                <Controller
                    control={control}
                    name={'timeLimitInSeconds'}
                    rules={{ required: 'Time limit in seconds field is required' }}
                    render={({ field: { onChange, value } }) => (
                        <BaseInput
                            labelText={'Time limit in seconds:'}
                            value={value}
                            onChange={onChange}
                            placeholder={'Enter time limit in seconds...'}
                            type={InputTypes.NUMBER}
                            min={0}
                        />
                    )}
                />
                <Controller
                    control={control}
                    name={'isRequiredForNextStage'}
                    render={({ field: { onChange, value } }) => (
                        <CheckboxInput
                            labelText={'Is required for next stage:'}
                            value={value}
                            onChange={onChange}
                        />
                    )}
                />
                <Controller
                    control={control}
                    name={'isTemplate'}
                    render={({ field: { onChange, value } }) => (
                        <CheckboxInput
                            labelText={'Is template:'}
                            value={value}
                            onChange={onChange}
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

export { UpsertQuestTaskModal };
