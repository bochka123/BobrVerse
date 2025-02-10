import { FC } from 'react';

import { uuid } from '@/helpers';
import { useQuestCreating } from '@/pages/quest-creating/hooks';

import styles from './quest-creation.layout.module.scss';

type QuestCreatingLayoutProps = {}
const QuestCreatingLayout: FC<QuestCreatingLayoutProps> = () => {

    const { addSlide,
        // removeSlide, updateSlide,
        questSlides
    } = useQuestCreating();

    const handleAddSlide = (): void => {
        addSlide({ id: uuid() });
    };

    return (
        <div className={styles.main}>
            <div className={styles.slidesWrapper}>
                <div className={styles.slidesColumn}>
                    {
                        questSlides.map((_, index) => (
                            <div key={index} className={styles.slide}>
                                <h2>{index + 1}</h2>
                            </div>
                        ))
                    }
                </div>
                <button className={styles.addSlideButton} onClick={handleAddSlide}>+</button>
            </div>

            <div/>
            <div/>
        </div>
    );
};

export { QuestCreatingLayout };
