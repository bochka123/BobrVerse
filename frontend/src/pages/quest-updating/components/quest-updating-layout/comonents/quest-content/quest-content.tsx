import { FC } from 'react';

// import { useParams } from 'react-router-dom';
import { WoodenContainer } from '@/components';

// import { QuestUpdatingContext, QuestUpdatingContextValues } from '@/pages/quest-updating/context';
import styles from './quest-content.module.scss';

const QuestContent: FC = () => {
    // const { slideId } = useParams<{ slideId: string }>();
    // const { questTasks, updateTask } = useContext(QuestUpdatingContext) as QuestUpdatingContextValues;

    // const slide = questTasks.find(slide => slide.order === slideId);

    // if (!slide) {
    //     return <WoodenContainer className={styles.container}>Slide not found</WoodenContainer>;
    // }

    // const handleUpdateSlide = (updatedContent: string): void => {
    //     updateTask(String(slideId), { ...slide, content: updatedContent });
    // };

    return (
        <WoodenContainer className={styles.container}>
            <></>
            {/*<h1>Editing Slide {slide.id}</h1>*/}
            {/*<textarea*/}
            {/*    value={slide.content || ''}*/}
            {/*    onChange={(e) => handleUpdateSlide(e.target.value)}*/}
            {/*/>*/}
        </WoodenContainer>
    );
};

export { QuestContent };
