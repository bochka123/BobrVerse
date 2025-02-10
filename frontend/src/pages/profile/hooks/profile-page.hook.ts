import { useEffect } from 'react';
import { useDispatch } from 'react-redux';

import { useGetMyProfileQuery } from '@/services';
import { setProfile } from '@/store/auth';

type ReturnType = {
    isProfileLoading: boolean;
}
const useProfilePageHook = (): ReturnType => {
    const { data: profileData, isLoading: isProfileLoading } = useGetMyProfileQuery();

    const dispatch = useDispatch();

    useEffect(() => {
        if (profileData?.data) 
            dispatch(setProfile(profileData.data));
        
    }, [profileData]);

    return {
        isProfileLoading: isProfileLoading,
    };
};

export { useProfilePageHook };
