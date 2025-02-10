import { useMemo } from 'react';
import { useSelector } from 'react-redux';

import { IProfileDto } from '@/models/responses';
import {
    selectCurrentId,
    selectCurrentLevel,
    selectCurrentLogs,
    selectCurrentName,
    selectCurrentXP
} from '@/store/auth';

const useProfileHook = (): IProfileDto => {
    const id = useSelector(selectCurrentId);
    const name = useSelector(selectCurrentName);
    const level = useSelector(selectCurrentLevel);
    const xp = useSelector(selectCurrentXP);
    const logs = useSelector(selectCurrentLogs);

    return useMemo(() => ({
        id: id,
        name: name,
        level: level,
        xp: xp,
        logs: logs,
    }), [id, name, level, xp, logs]);
};

export { useProfileHook };
