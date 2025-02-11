import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { FC } from 'react';
import { useNavigate } from 'react-router-dom';

import styles from './back-button.module.scss';

type BackButtonProps = {}
const BackButton: FC<BackButtonProps> = () => {
    
    const navigate = useNavigate();
    
    return (
        <FontAwesomeIcon className={styles.button} icon={faArrowLeft} onClick={() => navigate(-1)} />
    );
};

export { BackButton };
