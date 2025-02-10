import React, { FC } from 'react';
import { Controller, SubmitHandler, useForm } from 'react-hook-form';
import { useDispatch } from 'react-redux';

import { InputTypes } from '@/common';
import { BaseButton } from '@/components';
import { useProfileHook } from '@/hooks';
import { IUpdateProfileRequestDto } from '@/models/requests';
import { useUpdateMutation } from '@/services';
import { setProfile } from '@/store/auth';

import styles from './model-content.module.scss';

type FormNames = {
    name: string;
}

type ModelContentProp = {
    setVisible: React.Dispatch<React.SetStateAction<boolean>>;
}

const ModelContent: FC<ModelContentProp> = ({ setVisible }) => {
    const { name } = useProfileHook();
    const [updateProfile] = useUpdateMutation();
    const dispatch = useDispatch();

    const { handleSubmit, control } = useForm<FormNames>({
        mode: 'onSubmit',
        defaultValues: {
            name
        }
    });

    const onSubmit: SubmitHandler<FormNames> = (data): void => {
        const requestData: IUpdateProfileRequestDto = {
            name: data.name,
        };

        updateProfile(requestData)
            .unwrap()
            .then((data) => {
                dispatch(setProfile(data.data));
                setVisible(false);
            })
            .catch((error) => { console.error('Failed to update:', error); });
    };

    const onError = (error: any): void => {
        console.error('Form validation failed:', error);
    };

    return (
        <form onSubmit={handleSubmit(onSubmit, onError)} className={styles.form}>
            <Controller
                control={control}
                name="name"
                render={({ field: { onChange, value } }) => (
                    <div className={styles.inputGroup}>
                        <input value={value} onChange={onChange} type={InputTypes.TEXT} placeholder="Email" />
                    </div>
                )}
            />
            <div className={styles.buttonsWrapper}>
                <BaseButton type={'submit'} className={styles.authButton}>
                    update
                </BaseButton>
            </div>
        </form>
    );
};

export { ModelContent };
