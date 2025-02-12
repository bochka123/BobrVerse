import { faPlus, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FC } from 'react';
import { Control, Controller, useFieldArray } from 'react-hook-form';

import { InputTypes } from '@/common';
import { BaseInput, IconButton } from '@/components';
import { uuid } from '@/helpers';

import { UpsertQuestTaskModalFormNames } from '../../upsert-quest-task-modal.tsx';
import styles from './required-resources-input.module.scss';

type RequiredResourcesInputProps = {
    control: Control<UpsertQuestTaskModalFormNames, any>;
}
const RequiredResourcesInput: FC<RequiredResourcesInputProps> = ({ control }) => {

    const { fields, append, remove } = useFieldArray({ control, name: 'requiredResources' });

    return (
        <div className={styles.resourcesContainer}>
            <span className={styles.label}>{'Required resources:'}</span>

            <div className={styles.resourceItemsContainer}>
                {
                    fields.map((field, index) => (
                        <div key={`required-resource-item-${field.id}`} className={styles.resourceItemWrapper}>
                            <Controller
                                control={control}
                                name={`requiredResources.${index}.name`}
                                rules={{ required: 'Resource name is required' }}
                                render={({ field }) => (
                                    <BaseInput labelText="Name:" {...field} placeholder="Enter resource name..."/>
                                )}
                            />
                            <Controller
                                control={control}
                                name={`requiredResources.${index}.quantity`}
                                rules={{ required: 'Quantity is required' }}
                                render={({ field }) => (
                                    <BaseInput labelText="Quantity:" type={InputTypes.NUMBER} {...field} min={0}/>
                                )}
                            />
                            <Controller
                                control={control}
                                name={`requiredResources.${index}.length`}
                                render={({ field }) => (
                                    <BaseInput labelText="Length:" type={InputTypes.NUMBER} {...field} min={0}/>
                                )}
                            />
                            <Controller
                                control={control}
                                name={`requiredResources.${index}.weigth`}
                                render={({ field }) => (
                                    <BaseInput labelText="Weight:" type={InputTypes.NUMBER} {...field} min={0}/>
                                )}
                            />
                            <IconButton icon={faRemove} onClick={() => remove(index)}/>
                        </div>
                    ))
                }
            </div>

            <IconButton
                icon={faPlus}
                onClick={() => append({
                    id: uuid(),
                    name: '',
                    quantity: 0,
                    length: 0,
                    weigth: 0
                })}
            />
        </div>
    );
};

export { RequiredResourcesInput };
