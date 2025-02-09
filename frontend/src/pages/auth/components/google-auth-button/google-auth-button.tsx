import { GoogleLogin, GoogleOAuthProvider } from '@react-oauth/google';
import { FC } from 'react';
import { useNavigate } from 'react-router-dom';

import { IGoogleAuthRequestDto } from '@/models/requests';
import { useGoogleMutation } from '@/services';

import styles from './google-auth-button.module.scss';

const clientId = import.meta.env.VITE_GOOGLE_CLIENT_ID;

const GoogleAuthButton: FC = () => {
    const [googleLogIn] = useGoogleMutation();
    const navigate = useNavigate();

    const onSuccess = (response: any): void => {
        const requestData: IGoogleAuthRequestDto = {
            credential: response.credential
        };

        googleLogIn(requestData)
            .unwrap()
            .then(() => navigate('/'))
            .catch((error) => { console.error('Failed to log in:', error); });
    };

    const onError = (): void => {
        console.error('Failed to log in...');
    };

    return (
        <GoogleOAuthProvider clientId={clientId}>
            <div className={styles.buttonWrapper}>
                <GoogleLogin
                    onSuccess={onSuccess}
                    onError={onError}
                    useOneTap
                />
            </div>
        </GoogleOAuthProvider>
    );
};

export { GoogleAuthButton };
