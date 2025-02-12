import { faStar as faStarRegular } from '@fortawesome/free-regular-svg-icons';
import { faStar as faStarSolid } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { FC, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

import { ToastModeEnum } from '@/common';
import { BaseButton, MultilineInput, WoodenContainer } from '@/components';
import { useToast } from '@/hooks';
import { ICreateQuestRatingDto } from '@/models/requests';
import { useRateQuestMutation } from '@/services';

import styles from './rate-quest.page.module.scss';

const RateQuestPage: FC = () => {

    const { responseId } = useParams();
    const navigate = useNavigate();
    const [rating, setRating] = useState(0);
    const [submitedRating, setSubmitedRating] = useState(0);
    const [comment, setComment] = useState<string>('');
    const [rateQuestMutation] = useRateQuestMutation();
    const { addToast } = useToast();

    const handleRating = (value: number): void => {
      setSubmitedRating(value);
    };
  
    const handleMouseEnter = (value: number): void => {
      setRating(value);
    };
  
    const handleMouseLeave = (): void => {
      setRating(submitedRating);
    };

    const rateQuest = (): void => {
        const requestData: ICreateQuestRatingDto = {
            questResponseId: String(responseId),
            comment,
            rating: submitedRating
        };

        rateQuestMutation(requestData)
            .unwrap()
            .then(() => navigate('/'))
            .catch(() => addToast(ToastModeEnum.ERROR, 'Failed to rate quest'));
    };
    
    return (
        <div className={styles.rateContainerWrapper}>
            <WoodenContainer className={styles.rateQuestContainer}>
                <div className={styles.rateQuestWrapper}>
                    <h3>Rate this quest</h3>
                    <div className={styles.starsContainer}>
                        {Array.from({ length: 5 }, (_, index) => {
                            const starIndex = index + 1;
                            
                            const icon = starIndex <= rating ? faStarSolid : faStarRegular;
                            
                            return (
                                <FontAwesomeIcon
                                    key={starIndex}
                                    icon={icon}
                                    onClick={() => handleRating(starIndex)}
                                    onMouseEnter={() => handleMouseEnter(starIndex)}
                                    onMouseLeave={handleMouseLeave}
                                    className={styles.starIcon}
                                />
                            );
                        })}
                    </div>
                    
                    <div className={styles.responseContainer}>
                        <MultilineInput
                            labelText={'Comment:'}
                            value={comment}
                            onChange={setComment}
                            placeholder={'Enter your comment...'}
                            rows={3}
                            cols={30}
                            maxLength={128}
                        />

                        <BaseButton onClick={rateQuest}>
                            Send
                        </BaseButton>
                    </div>
                </div>
            </WoodenContainer>
        </div>
    );
};

export { RateQuestPage };
