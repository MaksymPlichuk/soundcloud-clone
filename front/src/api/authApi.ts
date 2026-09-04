import {createApi} from "@reduxjs/toolkit/query/react";
import {createBaseQuery} from "../utils/CreateBaseQuery.ts";
import {type AuthResponse} from "../types/Auth/AuthResponse.ts";
import {type LoginDto} from "../types/Auth/LoginDto.ts";
import {type RegisterDto} from "../types/Auth/RegisterDto.ts";

export const authApi = createApi({
    reducerPath: "authApi",
    baseQuery: createBaseQuery("Auth"),

    endpoints: (builder) => ({
        login: builder.mutation<AuthResponse, LoginDto>({
            query: (credentials: LoginDto) => ({
                url: "/login",
                method: "POST",
                body: credentials,
            }),
        }),
        register: builder.mutation<AuthResponse, RegisterDto>({
            query: (data: RegisterDto) => ({
                url: "/register",
                method: "POST",
                body: data,
            }),
        }),
    })
});
export const { useLoginMutation, useRegisterMutation } = authApi;