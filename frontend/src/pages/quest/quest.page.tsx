import { FC, useCallback,useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { clearInterval, setInterval } from 'worker-timers';

import { BackButton, Loader } from '@/components';
import { ICreateQuestTaskResponseDto } from '@/models/requests';
import { IApiResponseDto, IQuestResponseDto, IQuestTaskDto, IQuestTaskResponseDto } from '@/models/responses';
import {
    useCreateQuestResponseMutation,
    useCreateQuestTaskResponseMutation,
    useGetTaskTypeInfosQuery
} from '@/services';

import { QuestAnswer, QuestHints, QuestQuestion, ResultsModal } from './comonents';
import styles from './quest.page.module.scss';

const formatTime = (seconds: number | string): string => {
    if (typeof seconds !== 'number' || seconds < 0) return 'x';

    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds.toString().padStart(2, '0')}`;
};

type QuestPageProps = {};

const QuestPage: FC<QuestPageProps> = () => {
    const { questId } = useParams();
    const [createQuestResponse] = useCreateQuestResponseMutation();
    const [createQuestTaskResponse] = useCreateQuestTaskResponseMutation();
    const [timeLeft, setTimeLeft] = useState<number | string>('x');
    const [questResponse, setQuestResponse] = useState<IQuestResponseDto>();
    const [currentTask, setCurrentTask] = useState<IQuestTaskDto>();
    const [nextTask, setNextTask] = useState<IQuestTaskDto>();
    const [questResults, setQuestResults] = useState<IQuestTaskResponseDto>();
    const [visible, setVisible] = useState<boolean>(false);
    const [code, setCode] = useState('');
    const [taskStartTime, setTaskStartTime] = useState<number | null>(null);
    const navigate = useNavigate();

    const { data: taskTypesData, isLoading: isTaskTypeDataLoading } = useGetTaskTypeInfosQuery();
    const currentTaskType = taskTypesData?.data
        .find((info) => info.taskType === currentTask?.taskType);

    useEffect(() => {
        if (questId != null) {
            createQuestResponse(questId)
                .unwrap()
                .then((data: IApiResponseDto<IQuestResponseDto>): void => {
                    setQuestResponse(data.data);
                    setCurrentTask(data.data.currentTask);
                    setNextTask(data.data.nextTask);
                    setTimeLeft(data.data.currentTask?.timeLimitInSeconds || 'x');
                    setTaskStartTime(Date.now());
                })
                .catch((error) => {
                    console.error('Failed to create quest response:', error);
                });
        }
    }, [questId, createQuestResponse]);

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
            setTimeLeft((prevTime) => {
                if (typeof prevTime === 'number' && prevTime > 0) {
                    return prevTime - 1;
                } else {
                    clearInterval(timer);
                    handleTaskResponse();
                    return 0;
                }
            });
        }, 1000);

        return () => clearInterval(timer);
    }, [timeLeft]);

    const handleTaskResponse = useCallback((): void => {
        if (questResponse && currentTask && taskStartTime) {
            const endTime = Date.now();
            const spentTimeInSeconds = Math.floor((endTime - taskStartTime) / 1000);

            const requestData: ICreateQuestTaskResponseDto = {
                questResponseId: questResponse.id,
                questTaskId: currentTask.id,
                text: JSON.stringify(code),
                spentTime: spentTimeInSeconds.toString()
            };

            createQuestTaskResponse(requestData)
                .unwrap()
                .then((data) => {
                    if (data.data.isFinished) {
                        setVisible(true);
                        setQuestResults(data.data);
                    } else {
                        setCurrentTask(nextTask);
                        setNextTask(data.data.nextTask);
                        setTaskStartTime(Date.now());
                    }
                })
                .catch((error) => {

                    console.error('Failed to create task response:', error);
                });
        }
    }, [questResponse, currentTask, code, createQuestTaskResponse, navigate, taskStartTime, nextTask]);

    const modalNavigateCallback = useCallback((): void => {
        if(questResponse != null)
            navigate(`response/${questResponse.id}/rate`);
    }, [questResponse, navigate]);

    const fetchNextTask = useCallback((): void => handleTaskResponse(), [handleTaskResponse]);
    const modalNavigate = useCallback((): void => modalNavigateCallback(), [modalNavigateCallback]);

    return (
        <>
            <BackButton />
            {!questResponse || !currentTask || isTaskTypeDataLoading ? (
                <Loader />
            ) : (
                <div className={styles.container}>
                    <div className={styles.leftPanelWrapper}>
                        <h1>{questResponse.questTitle}</h1>
                        <QuestQuestion task={currentTask} taskType={currentTaskType} />
                        <QuestAnswer code={code} setCode={setCode} />
                    </div>
                    <div className={styles.rightPanelWrapper}>
                        <div className={styles.timeWrapper}>
                            <h1>Time left: {formatTime(timeLeft)}</h1>
                        </div>
                        <QuestHints
                            task={currentTask}
                            fetchNextTask={fetchNextTask}
                            isNextTaskExists={nextTask != null}
                        />
                    </div>
                </div>
            )}

            <ResultsModal visible={visible} setVisible={setVisible} result={questResults} callback={modalNavigate} />
        </>
    );
};

export { QuestPage };
