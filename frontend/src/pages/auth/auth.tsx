import { FC } from 'react';

import { InputTypes } from '@/common';

import styles from './auth.module.scss';

const AuthPage: FC = () => {
    return (
        <div className={styles.authContainer}>
            <div className={styles.authCard}>
                <h2>Sign In</h2>
                <form>
                    <div className={styles.inputGroup}>
                        <input type={InputTypes.EMAIL} placeholder="Email" required />
                    </div>
                    <div className={styles.inputGroup}>
                        <input type={InputTypes.PASSWORD} placeholder="Password" required />
                    </div>
                    <button type="submit" className={styles.authButton}>Login</button>
                </form>
                <button className={styles.googleButton}>
                    <img src="/google-icon.png" alt="Google logo" /> Sign in with Google
                </button>
                <p className={styles.authFooter}>Don't have an account? <a href="#">Sign Up</a></p>
            </div>
        </div>
    );
};

export { AuthPage };
