import { faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { FC, MouseEventHandler, useRef } from 'react';
import { Link } from 'react-router-dom';

import { ToastModeEnum } from '@/common';
import { useToast } from '@/hooks';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';
import { useDeleteQuestTaskMutation } from '@/services';

import styles from './quest-updating-slides-item.module.scss';

type QuestTasksItemProps = {
    taskId: string;
    questId: string;
    slideNumber: number;
}
const QuestUpdatingTasksItem: FC<QuestTasksItemProps> = ({ taskId, questId, slideNumber }) => {
    const slideRef = useRef<HTMLAnchorElement>(null);

    const [deleteTask] = useDeleteQuestTaskMutation();

    const { removeTask } = useQuestUpdating();

    const { addToast } = useToast();

    const onDeleteClick: MouseEventHandler<SVGSVGElement> = (event) => {
        event?.preventDefault();


        deleteTask(taskId)
            .unwrap()
            .then(() => {
                addToast(ToastModeEnum.ERROR, 'Quest deleted created successfully');

                if (!slideRef.current) return;

                slideRef.current.style.transition = 'height 0.3s ease, opacity 0.3s ease';
                slideRef.current.style.overflow = 'hidden';
                slideRef.current.style.height = '0px';
                slideRef.current.style.opacity = '0';

                setTimeout(() => {
                    removeTask(taskId);
                }, 300);
            })
            .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to delete quest'));
    };

    return (
        <Link ref={slideRef} to={`/quests/edit/${questId}/task/${taskId}`} className={styles.slide}>
            <h2 className={styles.slideNumber}>{slideNumber}</h2>
            <FontAwesomeIcon className={styles.slideDelete} icon={faTrashCan} onClick={onDeleteClick}/>
        </Link>
    );
};

export { QuestUpdatingTasksItem };
