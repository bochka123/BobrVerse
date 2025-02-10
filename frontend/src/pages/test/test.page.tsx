import { FC } from 'react';

import { useUploadPhotoMutation } from '@/services';

const TestPage: FC = () => {
    const [uploadPhoto] = useUploadPhotoMutation();

    const fileSelected = (event: any): void => {
        // @ts-ignore
        const file = event.target.files[0];

        if (file) {
            const formData = new FormData();
            formData.append(file.name, file, `/${file.name}`);

            uploadPhoto(formData)
                .unwrap()
                .then((data) => {
                    console.log('Success: ' + data);
                })
                .catch((e) => {
                    console.log('Error: ' + e);
                });
        }
    };

    return (
        <div>
            <input type="file" onChange={fileSelected} />
        </div>
    );
};

export { TestPage };
