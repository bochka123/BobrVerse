import { Dispatch, FC, SetStateAction } from 'react';

import { UploadPhoto } from '@/components';
import { DeletePhotoButton } from '@/modules/setting-profile/modal-content/components';
import img from '@/resources/profile.png';

import styles from './photo-picker.module.scss';

type PhotoPickerProps = {
    setImageUrl: Dispatch<SetStateAction<string | undefined>>;
    setFile: Dispatch<SetStateAction<File | null>>;
    imageUrl: string | undefined;
    onDeletePhoto: () => void;
    defaultImage?: string;
}
const PhotoPicker: FC<PhotoPickerProps> = ({ setImageUrl, setFile, imageUrl, onDeletePhoto, defaultImage }) => {
    return (
        <div className={styles.photoWrapper}>
            <img src={imageUrl || defaultImage || img} alt="Selected Image" className={styles.imagePreview}/>
            <UploadPhoto setImageUrl={setImageUrl} setFile={setFile}/>
            <DeletePhotoButton onClick={onDeletePhoto}/>
        </div>
    );
};

export { PhotoPicker };
