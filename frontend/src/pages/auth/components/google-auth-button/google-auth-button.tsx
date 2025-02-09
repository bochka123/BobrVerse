import { CredentialResponse, GoogleLogin, GoogleOAuthProvider } from '@react-oauth/google';
import { FC } from 'react';

import styles from './google-auth-button.module.scss';

type GoogleAuthButtonProps = {}
const GoogleAuthButton: FC<GoogleAuthButtonProps> = () => {
    const clientId = import.meta.env.VITE_GOOGLE_CLIENT_ID;

    const onSuccess = (response: CredentialResponse): void => {
        console.log('Google OAuth response:', response);
    };

    const onError = (): void => {
        console.error('Google OAuth failed');
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
