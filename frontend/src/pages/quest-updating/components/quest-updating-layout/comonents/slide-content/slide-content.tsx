import { FC, useContext } from 'react';
import { useParams } from 'react-router-dom';

import { QuestUpdatingContext, QuestUpdatingContextValues } from '@/pages/quest-updating/context';

import styles from './slide-content.module.scss';

const SlideContent: FC = () => {
    const { slideId } = useParams<{ slideId: string }>();
    const { questSlides, updateSlide } = useContext(QuestUpdatingContext) as QuestUpdatingContextValues;

    const slide = questSlides.find(slide => slide.id === slideId);

    if (!slide) {
        return <div>Slide not found</div>;
    }

    const handleUpdateSlide = (updatedContent: string): void => {
        updateSlide(String(slideId), { ...slide, content: updatedContent });
    };

    return (
        <div className={styles.mainContentWrapper}>
            <h1>Editing Slide {slide.id}</h1>
            <textarea
                value={slide.content || ''}
                onChange={(e) => handleUpdateSlide(e.target.value)}
            />
        </div>
    );
};

export { SlideContent };
