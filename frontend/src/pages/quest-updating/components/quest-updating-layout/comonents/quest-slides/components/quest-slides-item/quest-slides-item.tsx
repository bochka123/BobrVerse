import { faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { FC, MouseEventHandler, useRef } from 'react';
import { Link } from 'react-router-dom';

import { useQuestUpdating } from '@/pages/quest-updating/hooks';

import styles from './quest-slides-item.module.scss';

type QuestSlidesItemProps = {
    slideId: string;
    questId: string;
    slideNumber: number;
}
const QuestSlidesItem: FC<QuestSlidesItemProps> = ({ slideId, questId, slideNumber }) => {
    const slideRef = useRef<HTMLAnchorElement>(null);

    const { removeSlide } = useQuestUpdating();

    const onDeleteClick: MouseEventHandler<SVGSVGElement> = (event) => {
        event?.preventDefault();
        if (!slideRef.current) return;

        slideRef.current.style.transition = 'height 0.3s ease, opacity 0.3s ease';
        slideRef.current.style.overflow = 'hidden';
        slideRef.current.style.height = '0px';
        slideRef.current.style.opacity = '0';

        setTimeout(() => {
            removeSlide(slideId);
        }, 300);
    };

    return (
        <Link ref={slideRef} to={`/quests/edit/${questId}/task/${slideId}`} className={styles.slide}>
            <h2 className={styles.slideNumber}>{slideNumber}</h2>
            <FontAwesomeIcon className={styles.slideDelete} icon={faTrashCan} onClick={onDeleteClick}/>
        </Link>
    );
};

export { QuestSlidesItem };
