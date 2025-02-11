import React, { FC, useState } from 'react';
import { Controller, SubmitErrorHandler, SubmitHandler, useForm } from 'react-hook-form';
import { useDispatch } from 'react-redux';

import { InputTypes, ToastModeEnum } from '@/common';
import { BaseButton } from '@/components';
import { getFormErrorMessage } from '@/helpers';
import { useProfileHook, useToast } from '@/hooks';
import { IUpdateProfileRequestDto } from '@/models/requests';
import { useUpdateMutation, useUploadPhotoMutation } from '@/services';
import { setProfile, setUrl } from '@/store/auth';

import styles from './model-content.module.scss';

type FormNames = {
    name: string;
}

type ModelContentProp = {
    setVisible: React.Dispatch<React.SetStateAction<boolean>>;
}

const ModelContent: FC<ModelContentProp> = ({ setVisible }) => {
    const { name, url } = useProfileHook();
    const [updateProfile] = useUpdateMutation();
    const [uploadPhoto] = useUploadPhotoMutation();
    const dispatch = useDispatch();
    const { addToast } = useToast();
    const [imageUrl, setImageUrl] = useState<string | undefined>(url);
    const [file, setFile] = useState<File | null>(null);

    const { handleSubmit, control } = useForm<FormNames>({
        mode: 'onSubmit',
        defaultValues: {
            name
        }
    });

    const onSubmit: SubmitHandler<FormNames> = (data): void => {

        if (file) {
            const formData = new FormData();
            formData.append(file.name, file, `/${file.name}`);

            uploadPhoto(formData)
                .unwrap()
                .then((data) => {
                    dispatch(setUrl(data.data));
                    addToast(ToastModeEnum.SUCCESS, 'Successfully updated profile image');
                })
                .catch(() => {
                    addToast(ToastModeEnum.ERROR, 'Failed to update profile image');
                });
        }

        const requestData: IUpdateProfileRequestDto = {
            name: data.name,
        };

        updateProfile(requestData)
            .unwrap()
            .then((data) => {
                dispatch(setProfile(data.data));
                addToast(ToastModeEnum.SUCCESS, 'Profile updated successfully');
                setVisible(false);
            })
            .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to update profile'));
    };

    const fileSelected = (event: React.ChangeEvent<HTMLInputElement>): void => {
        // @ts-ignore
        const file = event.target.files[0];

        if (file) {
            setImageUrl(URL.createObjectURL(file));
            setFile(file);
        }
    };

    const onError: SubmitErrorHandler<FormNames> = (error): void => {
        addToast(ToastModeEnum.ERROR, getFormErrorMessage(error));
    };

    return (
        <form onSubmit={handleSubmit(onSubmit, onError)} className={styles.form}>
            {imageUrl && <img src={imageUrl} alt="Selected Image" className={styles.imagePreview} />}

            <input type="file" accept="image/*" onChange={fileSelected} />
            {/* <input type="file" onChange={fileSelected} /> */}
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
