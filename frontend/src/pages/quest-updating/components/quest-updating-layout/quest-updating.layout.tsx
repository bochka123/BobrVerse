import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { FC } from 'react';
import { Link, Outlet, useParams } from 'react-router-dom';

import { IconButton } from '@/components';
import { uuid } from '@/helpers';

import { useQuestUpdating } from '../../hooks';
import { QuestHints } from './comonents';
import styles from './quest-updating.layout.module.scss';

type QuestUpdatingLayoutProps = {}
const QuestUpdatingLayout: FC<QuestUpdatingLayoutProps> = () => {
    const { questId } = useParams();

    const { addSlide,
        // removeSlide, updateSlide,
        questSlides
    } = useQuestUpdating();

    const handleAddSlide = (): void => {
        addSlide({ id: uuid(), content: '' });
    };

    return (
        <div className={styles.main}>
            <div className={styles.slidesWrapper}>
                <div className={styles.slidesColumn}>
                    {
                        questSlides.map((slide, index) => (
                            <Link key={`slide-${slide.id}`} to={`/quests/edit/${questId}/task/${slide.id}`} className={styles.slide}>
                                <h2>{index + 1}</h2>
                            </Link>
                        ))
                    }
                </div>
                <IconButton icon={faPlus} onClick={handleAddSlide} />
            </div>

            <div className={styles.mainContentWrapper}>
                <Outlet />
            </div>
            <div className={styles.rightPanelWrapper}>
                <QuestHints />
            </div>
        </div>
    );
};

export { QuestUpdatingLayout };
