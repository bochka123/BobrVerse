import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { FC, useState } from 'react';
import { useParams } from 'react-router-dom';

import { IconButton, WoodenContainer } from '@/components';
import { uuid } from '@/helpers';
import { useQuestUpdating } from '@/pages/quest-updating/hooks';

import { QuestSlidesItem, UpsertQuestTaskModal } from './components';
import styles from './quest-slides.module.scss';

type QuestSlidesProps = {}
const QuestSlides: FC<QuestSlidesProps> = () => {

    const { questId } = useParams();

    const [addTaskModalVisible, setAddTaskModalVisible  ] = useState(false);

    const { addSlide, questSlides } = useQuestUpdating();

    const handleAddSlide = (): void => {
        setAddTaskModalVisible(true);
        // addSlide({ id: uuid(), content: '' });
    };
    
    return (
        <>
            <WoodenContainer className={styles.slidesContainer}>
                <div className={styles.slidesWrapper}>
                    <div className={styles.slidesColumn}>
                        {
                            questSlides.map((slide, index) => (
                                <QuestSlidesItem
                                    questId={questId as string}
                                    slideId={slide.id}
                                    slideNumber={index + 1}
                                    key={`slide-${slide.id}`}
                                />
                            ))
                        }
                    </div>
                    <IconButton icon={faPlus} onClick={handleAddSlide}/>
                </div>
            </WoodenContainer>

            <UpsertQuestTaskModal visible={addTaskModalVisible} setVisible={setAddTaskModalVisible} />
        </>
    );
};

export { QuestSlides };
