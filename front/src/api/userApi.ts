import { createApi } from "@reduxjs/toolkit/query/react";
import { createBaseQuery } from "../utils/CreateBaseQuery";
import type { IUserForInfo } from "../types/User/IUserForInfo";

type ServiceResponse<T> = {
    success: boolean;
    message: string;
    data: T;
};

export const userApi = createApi({
    reducerPath: "userApi",

    baseQuery: createBaseQuery("Users"),

    tagTypes: ["User"],

    endpoints: (builder) => ({
        getUsers: builder.query<IUserForInfo[], void>({
            query: () => "",

            transformResponse: (
                response: ServiceResponse<IUserForInfo[]>
            ) => response.data,

            providesTags: ["User"],
        }),
    }),
});

export const {
    useGetUsersQuery,
} = userApi;