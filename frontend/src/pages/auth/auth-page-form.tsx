import { FC } from 'react';
import { Controller, SubmitHandler, useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';

import { InputTypes } from '@/common';
import { BaseButton } from '@/components';
import { useAuth } from '@/hooks';
import { IAuthRequestDto } from '@/models/requests';
import { useLoginMutation, useRegisterMutation } from '@/services';

import styles from './auth-page.module.scss';
import { GoogleAuthButton } from './components';

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
    const { logIn: setAuthenticated } = useAuth();
    
    const navigate = useNavigate();

    const { handleSubmit, control } = useForm<FormNames>();

    const onSubmit: SubmitHandler<FormNames> = (data): void => {
        const requestData: IAuthRequestDto = {
            email: data.email,
            password: data.password,
        };

        authType === 'signIn'
            ? logIn(requestData)
                .unwrap()
                .then(() => {
                    setAuthenticated();
                    navigate('/');
                })
                .catch((error) => { console.error('Failed to log in:', error); })
            : signUp(requestData)
                .unwrap()
                .then(() => {
                    setAuthenticated();
                    navigate('/');
                })
                .catch((error) => { console.error('Failed to register:', error); });
    };
    
    const onError = (error: any): void => {
        console.error('Form validation failed:', error);
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
            <div className={styles.buttonsWrapper}>
                <BaseButton type={'submit'} className={styles.authButton}>
                    {authType === 'signIn' ? 'Login' : 'Register'}
                </BaseButton>
                <GoogleAuthButton/>
            </div>
        </form>
    );
};

export { AuthPageForm };
