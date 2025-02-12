import { useParams } from 'react-router-dom';

import { ToastModeEnum } from '@/common';
import { useToast } from '@/hooks';
import { IQuestTaskDto } from '@/models/responses';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';
import { useDeleteQuestTaskPhotoMutation, useUploadQuestTaskPhotoMutation } from '@/services';

const useQuestUpdatingPhoto = (task?: IQuestTaskDto) => {

    const { updateTask } = useQuestUpdating();

    const { taskId } = useParams<{ questId: string, taskId: string }>();

    const { addToast } = useToast();

    const [uploadPhotoTrigger] = useUploadQuestTaskPhotoMutation();
    
    const uploadPhoto = (file: File, imageUrl: string): void => {
        if (!task) return;
        const formData = new FormData();
        formData.append(file.name, file, `/${file.name}`);

        uploadPhotoTrigger({ taskId: taskId as string, photoDto: formData })
            .unwrap()
            .then(() => {
                updateTask(taskId as string, { ...task, url: imageUrl });
                addToast(ToastModeEnum.SUCCESS, 'Successfully updated task image');
            })
            .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to update task image'));
    };

    const [deletePhotoTrigger] = useDeleteQuestTaskPhotoMutation();
    
    const deletePhoto = (): void => {
        if (!task) return;
        deletePhotoTrigger(taskId as string)
            .unwrap()
            .then(() => {
                updateTask(taskId as string, { ...task, url: '' });
                addToast(ToastModeEnum.SUCCESS, 'Successfully deleted task image');
            })
            .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to delete task image'));
    };

    return {
        uploadPhoto,
        deletePhoto,
    };
};

export { useQuestUpdatingPhoto };
