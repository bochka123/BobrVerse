import { useEffect } from 'react';
import { useDispatch } from 'react-redux';

import { useGetMyProfileQuery } from '@/services';
import { setProfile, setUrl } from '@/store/auth';

type ReturnType = {
    isProfileLoading: boolean;
}
const useProfilePageHook = (): ReturnType => {
    const { data: profileData, isLoading: isProfileLoading } = useGetMyProfileQuery();

    const dispatch = useDispatch();

    useEffect(() => {
        if (profileData?.data){
            dispatch(setProfile(profileData.data));
            dispatch(setUrl(profileData.data));
        }
        
    }, [profileData]);

    return {
        isProfileLoading: isProfileLoading,
    };
};

export { useProfilePageHook };
