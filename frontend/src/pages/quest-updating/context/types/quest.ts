type QuestType = {
    id: string;
    title?: string;
    content: string;
    hints?: string[];
    answer?: string;
    isCompleted?: boolean;
};

export { type QuestType };
