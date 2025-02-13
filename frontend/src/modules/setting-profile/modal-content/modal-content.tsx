import React, { FC, useState } from 'react';
import { Controller, SubmitErrorHandler, SubmitHandler, useForm } from 'react-hook-form';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';

import { ToastModeEnum } from '@/common';
import { BaseButton, BaseInput } from '@/components';
import { getFormErrorMessage } from '@/helpers';
import { useProfileHook, useToast } from '@/hooks';
import { IUpdateProfileRequestDto } from '@/models/requests';
import { PhotoPicker } from '@/modules';
import {
    apiSlice,
    useDeletePhotoMutation,
    useLogOutMutation,
    useUpdateMutation,
    useUploadPhotoMutation
} from '@/services';
import { setProfile, setUrl } from '@/store/auth';

import styles from './model-content.module.scss';

type FormNames = {
    name: string;
}

type ModalContentProp = {
    setVisible: React.Dispatch<React.SetStateAction<boolean>>;
}

const ModalContent: FC<ModalContentProp> = ({ setVisible }) => {
    const { name, url } = useProfileHook();

    const [updateProfile] = useUpdateMutation();
    const [uploadPhoto] = useUploadPhotoMutation();
    const [deletePhoto] = useDeletePhotoMutation();
    const [logoutMutation] = useLogOutMutation();

    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { addToast } = useToast();

    const [imageUrl, setImageUrl] = useState<string | undefined>(url);
    const [file, setFile] = useState<File | null>(null);

    const { handleSubmit, control } = useForm<FormNames>({
        mode: 'onSubmit',
        defaultValues: {
            name
        }
    });

    const onDeletePhoto = (): void => {
        setImageUrl(undefined);
        setFile(null);
    };

    const onSubmit: SubmitHandler<FormNames> = (data): void => {

        if (file && imageUrl !== url) {
            const formData = new FormData();
            formData.append(file.name, file, `/${file.name}`);

            uploadPhoto(formData)
                .unwrap()
                .then((data) => {
                    dispatch(setUrl(data.data));
                    addToast(ToastModeEnum.SUCCESS, 'Successfully updated profile image');
                    setVisible(false);
                })
                .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to update profile image'));
        }

        if (url && !imageUrl) {
            deletePhoto()
                .unwrap()
                .then(() => {
                    dispatch(setUrl(''));
                    addToast(ToastModeEnum.SUCCESS, 'Successfully deleted profile image');
                    setVisible(false);
                })
                .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to delete profile image'));
        }

        if (name !== data.name) {
            const requestData: IUpdateProfileRequestDto = {
                name: data.name,
            };

            updateProfile(requestData)
                .unwrap()
                .then((data) => {
                    dispatch(setProfile(data.data));
                    addToast(ToastModeEnum.SUCCESS, 'Successfully updated profile name');
                    setVisible(false);
                })
                .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to update profile'));
        }
    };

    const onError: SubmitErrorHandler<FormNames> = (error): void => {
        addToast(ToastModeEnum.ERROR, getFormErrorMessage(error));
    };

    const logout = (): void => {
        logoutMutation()
            .unwrap()
            .then(() => {
                navigate('/auth');
                addToast(ToastModeEnum.SUCCESS, 'Successfully logout');
                dispatch(apiSlice.util.resetApiState());
            })
            .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to logout'));
    };

    return (
        <form onSubmit={handleSubmit(onSubmit, onError)} className={styles.form}>
            <PhotoPicker
                setImageUrl={setImageUrl}
                setFile={setFile}
                imageUrl={imageUrl}
                onDeletePhoto={onDeletePhoto}
            />

            <Controller
                control={control}
                name="name"
                render={({ field: { onChange, value } }) => (
                    <BaseInput
                        value={value}
                        onChange={onChange}
                        placeholder={'Enter new name...'}
                        labelText={'New name:'}
                    />
                )}
            />

            <BaseButton type={'submit'}>Update</BaseButton>
            <BaseButton onClick={logout}>Log Out</BaseButton>
        </form>
    );
};

export { ModalContent };
