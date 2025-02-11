import { useMemo } from 'react';
import { useSelector } from 'react-redux';

import { IProfileDto } from '@/models/responses';
import {
    selectCurrentId,
    selectCurrentLevel,
    selectCurrentLogs,
    selectCurrentName,
    selectCurrentUrl,
    selectCurrentXP } from '@/store/auth';

const useProfileHook = (): IProfileDto => {
    const id = useSelector(selectCurrentId);
    const name = useSelector(selectCurrentName);
    const level = useSelector(selectCurrentLevel);
    const xp = useSelector(selectCurrentXP);
    const logs = useSelector(selectCurrentLogs);
    const url = useSelector(selectCurrentUrl);

    return useMemo(() => ({
        id,
        name,
        level,
        xp,
        logs,
        url
    }), [id, name, level, xp, logs, url]);
};

export { useProfileHook };
