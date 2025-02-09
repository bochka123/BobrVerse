import { FC } from 'react';

import styles from './auth-page.module.scss';
import { AuthPageForm } from './auth-page-form.tsx';

const AuthPage: FC = () => {
    
    return (
        <div className={styles.authContainer}>
            <div className={styles.authCard}>
                <h2>Sign In</h2>
                <AuthPageForm />
                <button className={styles.googleButton}>
                    <img src="/google-icon.png" alt="Google logo" /> Sign in with Google
                </button>
                <p className={styles.authFooter}>Don't have an account? <a href="#">Sign Up</a></p>
            </div>
        </div>
    );
};

export { AuthPage };
