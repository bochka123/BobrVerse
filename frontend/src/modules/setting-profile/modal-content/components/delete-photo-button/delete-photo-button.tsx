import { faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { FC } from 'react';

import styles from './delete-photo-button.module.scss';

type DeletePhotoButtonProps = {
    onClick: () => void;
}
const DeletePhotoButton: FC<DeletePhotoButtonProps> = ({ onClick }) => {
    return (
        <div onClick={onClick} className={styles.deleteButtonWrapper}>
            <FontAwesomeIcon icon={faTrashCan} />
        </div>
    );
};

export { DeletePhotoButton };
