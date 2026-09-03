import {createSlice, type PayloadAction} from "@reduxjs/toolkit";

import {isTokenExpired, jwtGetUser} from "./jwt.ts";
import type {RootState} from "../store";
import type {UserForInfo} from "../types/User/UserForInfo.ts";
import type {AuthResponse} from "../types/Auth/AuthResponse.ts";

interface AuthState {
    user: UserForInfo | null;
    token: string | null;
    isAuthenticated: boolean;
}

const storedToken = localStorage.getItem("token");
const initialState: AuthState = {
    user: storedToken ? jwtGetUser(storedToken) : null,
    token: storedToken,
    isAuthenticated: !!storedToken, //!! конверт на bool
};

const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        setCredentials: (
            state,
            action: PayloadAction<string>,
        ) => {
            state.user = jwtGetUser(action.payload);
            state.token = action.payload;
            state.isAuthenticated = true;
            //console.log(action.payload);

            localStorage.setItem("token", action.payload);
        },
        logout: (state) => {
            state.user = null;
            state.token = null;
            state.isAuthenticated = false;
            localStorage.removeItem("token");
        },
    },
})
export const {setCredentials, logout} = authSlice.actions;
export default authSlice.reducer;

export const selectIsAuth = (state: RootState): boolean => {
    return !isTokenExpired(state.auth.token);
}