import { Dispatch, FC, SetStateAction } from 'react';
import { Controller, SubmitErrorHandler, SubmitHandler, useForm } from 'react-hook-form';

import { InputTypes, TaskTypeEnum, ToastModeEnum } from '@/common';
import { BaseButton, BaseInput, CheckboxInput, Modal, SelectInput } from '@/components';
import { getFormErrorMessage, uuid } from '@/helpers';
import { useToast } from '@/hooks';
import { ICreateQuestTaskDto,IResourceDto } from '@/models/requests';
import { IQuestTaskDto } from '@/models/responses';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';
import { useCreateQuestTaskMutation, useUpdateQuestTaskMutation } from '@/services';

import { RequiredResourcesInput } from './components';
import styles from './upsert-quest-task-modal.module.scss';

type UpsertQuestTaskModalFormNames = {
    taskType: string;
    shortDescription: string;
    description: string;
    isRequiredForNextStage: boolean;
    maxAttempts: number;
    timeLimitInSeconds: number;
    isTemplate: boolean;

    requiredResources?: IResourceDto[];

    forestSize?: number;
    treesToCut?: number;
    cutLargest: boolean;
}

type UpsertQuestTaskModalProps = {
    visible: boolean;
    setVisible: Dispatch<SetStateAction<boolean>>;
    questId: string;
    taskForEditing?: IQuestTaskDto;
}
const UpsertQuestTaskModal: FC<UpsertQuestTaskModalProps> = ({ visible, setVisible, questId, taskForEditing }) => {

    const { addToast } = useToast();
    
    const [createTaskTrigger] = useCreateQuestTaskMutation();
    const [updateTaskTrigger] = useUpdateQuestTaskMutation();

    const { addTask, updateTask } = useQuestUpdating();

    const { handleSubmit, control, watch } = useForm<UpsertQuestTaskModalFormNames>({
        defaultValues: taskForEditing ? {
            requiredResources: taskForEditing.requiredResources,
            description: taskForEditing.description,
            isRequiredForNextStage: taskForEditing.isRequiredForNextStage,
            isTemplate: taskForEditing.isTemplate,
            maxAttempts: taskForEditing.maxAttempts,
            taskType: taskForEditing.taskType,
            shortDescription: taskForEditing.shortDescription,
            timeLimitInSeconds: taskForEditing.timeLimitInSeconds,
            forestSize: taskForEditing.forestSize,
            treesToCut: taskForEditing.treesToCut,
            cutLargest: taskForEditing.cutLargest,
        } : {
            requiredResources: [{ id: uuid(), name: 'wood', quantity: 5 }],
        }
    });

    const taskType = watch('taskType');

    const onSubmit: SubmitHandler<UpsertQuestTaskModalFormNames> = (data): void => {
        const requestData: ICreateQuestTaskDto = {
            questId: questId,
            isRequiredForNextStage: data.isRequiredForNextStage,
            isTemplate: data.isTemplate,
            maxAttempts: data.maxAttempts,
            timeLimitInSeconds: data.timeLimitInSeconds,
            taskType: data.taskType as TaskTypeEnum,
            shortDescription: data.shortDescription,
            description: data.description,
            requiredResources: data.requiredResources,
            forestSize: data.forestSize,
            treesToCut: data.treesToCut,
            cutLargest: !!data.cutLargest,
        };

        const trigger = taskForEditing
            ? () => updateTaskTrigger({ ...requestData, id: taskForEditing.id  })
            : () => createTaskTrigger(requestData);

        trigger()
            .unwrap()
            .then((data) => {
                addToast(ToastModeEnum.ERROR, taskForEditing
                    ? 'Quest task updated successfully'
                    : 'Quest task created successfully'
                );
                taskForEditing
                    ? updateTask(data.data.id, data.data)
                    : addTask({ id: data.data.id, task: data.data });

                setVisible(false);
            })
            .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to create quest'));
    };

    const onError: SubmitErrorHandler<UpsertQuestTaskModalFormNames> = (error): void => {
        addToast(ToastModeEnum.ERROR, getFormErrorMessage(error));
    };
    
    return (
        <Modal visible={visible} setVisible={setVisible} heading={taskForEditing ? 'Update quest task' : 'Create quest task'}>
            <form onSubmit={handleSubmit(onSubmit, onError)} className={styles.form}>
                <div className={styles.inputsWrapper}>
                    <div className={styles.baseInputsWrapper}>
                        <Controller
                            control={control}
                            name={'taskType'}
                            rules={{ required: 'Task type field is required' }}
                            render={({ field: { onChange, value } }) => (
                                <SelectInput
                                    labelText={'Task type:'}
                                    value={value}
                                    onChange={onChange}
                                    options={Object.entries(TaskTypeEnum).map(([_, value]) => value )}
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
                    </div>

                    {
                        taskType === TaskTypeEnum.COLLECT_RESOURCES &&
                        <RequiredResourcesInput control={control} />
                    }

                    {
                        taskType === TaskTypeEnum.CUT_TREES_IN_FOREST &&
                        <div className={styles.form}>
                            <Controller
                                control={control}
                                name={'forestSize'}
                                rules={{ required: 'Forest size is required' }}
                                render={({ field: { onChange, value } }) => (
                                    <BaseInput
                                        labelText={'Forest size:'}
                                        value={value}
                                        onChange={onChange}
                                        placeholder={'Enter forest size...'}
                                        type={InputTypes.NUMBER}
                                        min={0}
                                    />
                                )}
                            />
                            <Controller
                                control={control}
                                name={'treesToCut'}
                                rules={{ required: 'Trees to cut is required' }}
                                render={({ field: { onChange, value } }) => (
                                    <BaseInput
                                        labelText={'Trees to cut is:'}
                                        value={value}
                                        onChange={onChange}
                                        placeholder={'Enter trees to cut is...'}
                                        type={InputTypes.NUMBER}
                                        min={0}
                                    />
                                )}
                            />
                            <Controller
                                control={control}
                                name={'cutLargest'}
                                render={({ field: { onChange, value } }) => (
                                    <CheckboxInput
                                        labelText={'Cut largest:'}
                                        value={value}
                                        onChange={onChange}
                                    />
                                )}
                            />
                        </div>
                    }
                </div>

                <BaseButton type={'submit'}>
                    { taskForEditing ? 'Update task' : 'Create task' }
                </BaseButton>
            </form>
        </Modal>
    );
};

export { UpsertQuestTaskModal, type UpsertQuestTaskModalFormNames };
