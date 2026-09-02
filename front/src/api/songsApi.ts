import { createApi } from "@reduxjs/toolkit/query/react";
import { createBaseQuery } from "../utils/CreateBaseQuery.ts";

import type { ISongItem } from "../types/Song/ISongItem.ts";
import type { ICreateSongItem } from "../types/Song/ICreateSongItem.ts";
import type { IUpdateSongItem } from "../types/Song/IUpdateSongItem.ts";

type ServiceResponse<T> = {
    message: string;
    isSuccess: boolean;
    payload: T;
};

export const songsApi = createApi({
    reducerPath: "songsApi",
    baseQuery: createBaseQuery("Songs"),
    tagTypes: ["Song"],

    endpoints: (builder) => ({
        getSongs: builder.query<ISongItem[], void>({
            query: () => ({
                url: "/",
                method: "GET",
            }),

            transformResponse: (
                response: ServiceResponse<ISongItem[]>
            ) => response.payload,

            providesTags: ["Song"],
        }),

        getSongById: builder.query<ISongItem, number>({
            query: (id) => ({
                url: `/by-id/${id}`,
                method: "GET",
            }),

            transformResponse: (
                response: ServiceResponse<ISongItem>
            ) => response.payload,

            providesTags: (_result, _error, id) => [
                { type: "Song", id },
            ],
        }),

        createSong: builder.mutation<ISongItem, ICreateSongItem>({
            query: (songData) => ({
                url: "/",
                method: "POST",
                body: songData,
            }),

            transformResponse: (
                response: ServiceResponse<ISongItem>
            ) => response.payload,

            invalidatesTags: ["Song"],
        }),

        updateSong: builder.mutation<ISongItem, IUpdateSongItem>({
            query: (songData) => ({
                url: "/",
                method: "PUT",
                body: songData,
            }),

            transformResponse: (
                response: ServiceResponse<ISongItem>
            ) => response.payload,

            invalidatesTags: ["Song"],
        }),

        deleteSong: builder.mutation<void, number>({
            query: (id) => ({
                url: `?id=${id}`,
                method: "DELETE",
            }),

            invalidatesTags: ["Song"],
        }),
    }),
});

export const {
    useGetSongsQuery,
    useGetSongByIdQuery,
    useCreateSongMutation,
    useUpdateSongMutation,
    useDeleteSongMutation,
} = songsApi;