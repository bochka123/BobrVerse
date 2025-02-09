import { FC } from 'react';
import { Controller, SubmitHandler, useForm } from 'react-hook-form';

import { InputTypes } from '@/common';
import { IAuthRequestDto } from '@/models/requests';
import { useLoginMutation, useRegisterMutation } from '@/services/auth';

import styles from './auth-page.module.scss';

type FormNames = {
    email: string;
    password: string;
}

type AuthPageFormProps = {
    authType: 'signIn' | 'signUp';
}
const AuthPageForm: FC<AuthPageFormProps> = ({ authType }) => {

    const [logIn] = useLoginMutation();
    const [signUp] = useRegisterMutation();

    const { handleSubmit, control } = useForm<FormNames>();

    const onSubmit: SubmitHandler<FormNames> = (data): void => {
        const requestData: IAuthRequestDto = {
            email: data.email,
            password: data.password,
        };

        authType === 'signIn'
            ? logIn(requestData)
                .unwrap()
                .then((response) => { console.log('Logged in successfully:', response); })
                .catch((error) => { console.error('Failed to log in:', error); })
            : signUp(requestData)
                .unwrap()
                .then((response) => { console.log('Registered successfully:', response); })
                .catch((error) => { console.error('Failed to register:', error); });
    };
    
    const onError = (error: any): void => {
        console.log('Form validation failed:', error);
    };

    return (
        <form onSubmit={handleSubmit(onSubmit, onError)} className={styles.form}>
            <Controller
                control={control}
                name="email"
                rules={{ required: 'Email field is required', pattern: /^[^\s@]+@[^\s@]+\.[^\s@]+$/ }}
                render={({ field: { onChange, value } }) => (
                    <div className={styles.inputGroup}>
                        <input value={value} onChange={onChange} type={InputTypes.EMAIL} placeholder="Email"/>
                    </div>
                )}
            />
            <Controller
                control={control}
                name="password"
                rules={{ required: 'Password field is required' }}
                render={({ field: { onChange, value } }) => (
                    <div className={styles.inputGroup}>
                        <input value={value} onChange={onChange} type={InputTypes.PASSWORD} placeholder="Password" />
                    </div>
                )}
            />
            <button type="submit" className={styles.authButton}>
                {authType ==='signIn'? 'Login' : 'Register'}
            </button>
        </form>
    );
};

export { AuthPageForm };
