import { FC } from 'react';
import { Navigate, Outlet } from 'react-router-dom';

import { useAuth } from '@/hooks';

// TODO: Relocate
const ProtectedRoute: FC = () => {
    const { isAuthenticated } = useAuth();
        
    return (
        isAuthenticated ? <Outlet /> : <Navigate to={'/auth'} />
    );
};

export { ProtectedRoute };
