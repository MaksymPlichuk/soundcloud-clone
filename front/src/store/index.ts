import {configureStore} from "@reduxjs/toolkit";
import {songsApi} from "../api/songsApi.ts";
import {type TypedUseSelectorHook, useDispatch, useSelector} from "react-redux";
import {albumApi} from "../api/albumApi.ts";
import {userApi} from "../api/userApi.ts";

export const store = configureStore({
    reducer: {
        [songsApi.reducerPath]: songsApi.reducer,
        [albumApi.reducerPath]: albumApi.reducer,
        [userApi.reducerPath]: userApi.reducer,
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(
            songsApi.middleware,
            albumApi.middleware,
            userApi.middleware,
        )
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = (): AppDispatch => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;