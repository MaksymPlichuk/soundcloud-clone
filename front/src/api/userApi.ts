import { createApi } from "@reduxjs/toolkit/query/react";
import { createBaseQuery } from "../utils/CreateBaseQuery";
import type {UserForInfo} from "../types/User/UserForInfo.ts";

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
        getUsers: builder.query<UserForInfo[], void>({
            query: () => "",

            transformResponse: (
                response: ServiceResponse<UserForInfo[]>
            ) => response.data,

            providesTags: ["User"],
        }),
    }),
});

export const {
    useGetUsersQuery,
} = userApi;