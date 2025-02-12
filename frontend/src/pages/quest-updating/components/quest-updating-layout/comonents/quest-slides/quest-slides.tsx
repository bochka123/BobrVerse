import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { FC } from 'react';
import { Link, useParams } from 'react-router-dom';

import { IconButton, WoodenContainer } from '@/components';
import { uuid } from '@/helpers';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';

import styles from './quest-slides.module.scss';

type QuestSlidesProps = {}
const QuestSlides: FC<QuestSlidesProps> = () => {

    const { questId } = useParams();

    const { addSlide,
        // removeSlide, updateSlide,
        questSlides
    } = useQuestUpdating();

    const handleAddSlide = (): void => {
        addSlide({ id: uuid(), content: '' });
    };
    
    return (
        <WoodenContainer className={styles.slidesContainer}>
            <div className={styles.slidesWrapper}>
                <div className={styles.slidesColumn}>
                    {
                        questSlides.map((slide, index) => (
                            <Link key={`slide-${slide.id}`} to={`/quests/edit/${questId}/task/${slide.id}`}
                                  className={styles.slide}>
                                <h2>{index + 1}</h2>
                            </Link>
                        ))
                    }
                </div>
                <IconButton icon={faPlus} onClick={handleAddSlide}/>
            </div>
        </WoodenContainer>
    );
};

export { QuestSlides };
