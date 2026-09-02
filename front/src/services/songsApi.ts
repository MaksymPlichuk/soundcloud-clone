import {createApi} from "@reduxjs/toolkit/query/react";
import {createBaseQuery} from "../utils/CreateBaseQuery.ts";
import type {ISongItem} from "../types/Song/ISongItem.ts";
import type {ICreateSongItem} from "../types/Song/ICreateSongItem.ts";
import type {IUpdateSongItem} from "../types/Song/IUpdateSongItem.ts";

export const songsApi = createApi({
    baseQuery: createBaseQuery('songs'),
    tagTypes: ['songs'],
    reducerPath: "songsApi",

    endpoints: (builder) => ({

        getSongs: builder.query<ISongItem[], void>({
            query: () => {
                return {
                    url: '/',
                    method: 'GET'
                }
            }
        }),
        createSong: builder.mutation<void, ICreateSongItem>({
            query: (songData) => {
                return{
                    url: "/",
                    method: "POST",
                    body: songData
                }
            }
        }),
        updateSong: builder.mutation<void, IUpdateSongItem>({
            query: (songData) => {
                return {
                    url: "/",
                    method: "PUT",
                    body: songData
                }
            }
        })

    })
});

export const {
    useGetSongsQuery,
    useCreateSongMutation,
    useUpdateSongMutation,
} = songsApi;