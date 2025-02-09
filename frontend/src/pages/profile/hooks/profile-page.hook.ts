import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';

import { useGetMyProfileQuery } from '@/services';
import { setProfile } from '@/store/auth';

type ReturnType = {
    isProfileLoading: boolean;
    hasLoaded: boolean;
}
const useProfilePageHook = (): ReturnType => {
    const { data: profileData, isLoading: isProfileLoading } = useGetMyProfileQuery();
    const [hasLoaded, setHasLoaded] = useState(false);

    const dispatch = useDispatch();

    useEffect(() => {
        if (profileData?.data) {
            dispatch(setProfile(profileData.data));
            setHasLoaded(true);
        }
    }, [profileData]);

    return {
        isProfileLoading: isProfileLoading,
        hasLoaded: hasLoaded,
    };
};

export { useProfilePageHook };
