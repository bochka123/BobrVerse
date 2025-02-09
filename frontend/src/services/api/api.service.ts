import {
    FetchArgs,
    fetchBaseQuery,
    FetchBaseQueryError,
} from '@reduxjs/toolkit/query';
import { BaseQueryFn, createApi } from '@reduxjs/toolkit/query/react';

const baseQuery = fetchBaseQuery({
    baseUrl: import.meta.env.VITE_API_URL,
    credentials: 'include',
    prepareHeaders: (headers) => {
        return headers;
    }
});

const baseQueryWithRefresh: BaseQueryFn<FetchArgs, unknown, FetchBaseQueryError> = async (args, api, extraOptions) => {
    let result = await baseQuery(args, api, extraOptions);

    if (result.error?.status === 401) {
        console.log('dropped 401');
    }

    return result;
};

export const apiSlice = createApi({
    baseQuery: baseQueryWithRefresh,
    endpoints: () => ({}),
});
