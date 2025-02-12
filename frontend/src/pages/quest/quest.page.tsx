import { FC, useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { clearInterval,setInterval } from 'worker-timers';

import { BackButton, Loader } from '@/components';
import { IApiResponseDto, IQuestResponseDto, IQuestTaskDto } from '@/models/responses';
import { useCreateQuestTaskResponseMutation } from '@/services';

import { QuestAnswer, QuestHints, QuestQuestion } from './comonents';
import styles from './quest.page.module.scss';

const formatTime = (seconds: number | string): string => {
    if (typeof seconds !== 'number') return 'x';
    
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
};

type QuestPageProps = {}

const QuestPage: FC<QuestPageProps> = () => {
    const { questId } = useParams();
    const [createQuestTaskResponse] = useCreateQuestTaskResponseMutation();
    const [timeLeft, setTimeLeft] = useState<number | string>('x');
    const [questResponse, setQuestResponse] = useState<IQuestResponseDto>();
    const [currentTask, setCurrentTask] = useState<IQuestTaskDto>();
    const [nextTask, setNextTask] = useState<IQuestTaskDto>();

    useEffect(() => {
        if(questId != null)
            createQuestTaskResponse(questId)
                .unwrap()
                .then((data: IApiResponseDto<IQuestResponseDto>): void => {
                    setQuestResponse(data.data);
                    setCurrentTask(data.data.currentTask);
                    setNextTask(data.data.nextTask);
                    setTimeLeft(data.data.currentTask?.timeLimitInSeconds || 'x');
                });
    }, [questId]);

    useEffect(() => {
        const handleBeforeUnload = (event: BeforeUnloadEvent): void => {
            event.preventDefault();
            event.returnValue = '';
          };
      
          window.addEventListener('beforeunload', handleBeforeUnload);
      
          return () => {
            window.removeEventListener('beforeunload', handleBeforeUnload);
          };
    }, []);

    useEffect(() => {
        if (typeof timeLeft !== 'number') return;

        const timer = setInterval(() => {
            setTimeLeft((prevTime) => typeof prevTime === 'number' && prevTime > 0 ? prevTime - 1 : 0);
        }, 1000);

        return () => clearInterval(timer);
    }, [timeLeft]);

    const fetchNextTask = (): void => {

    };

    const finishQuest = (): void => {
        // navigate to response/:responseId
    };

    return (
        <>
            <BackButton />
            {
                !questResponse || !currentTask
                ? <Loader />
                : <div className={styles.container}>
                    <div className={styles.leftPanelWrapper}>
                        <h1>{questResponse.questTitle}</h1>
                        <QuestQuestion task={currentTask} />
                        <QuestAnswer />
                    </div>
                    <div className={styles.rightPanelWrapper}>
                        <div className={styles.timeWrapper}><h1>Time left: {formatTime(timeLeft)}</h1></div>
                        <QuestHints task={currentTask} fetchNextTask={fetchNextTask} isNextTaskExists={nextTask != null} finishQuest={finishQuest} />
                    </div>
                </div>
            }
        </>
    );
};

export { QuestPage };
