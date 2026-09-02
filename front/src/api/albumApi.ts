import { createApi } from "@reduxjs/toolkit/query/react";
import { createBaseQuery } from "../utils/CreateBaseQuery.ts";

import type { Album } from "../types/Album/Album.ts";
import type { CreateAlbumForm } from "../types/Album/Album.ts";
import type { UpdateAlbumForm } from "../types/Album/Album.ts";

type ServiceResponse<T> = {
    message: string;
    isSuccess: boolean;
    payload: T;
};

export const albumApi = createApi({
    reducerPath: "albumApi",
    baseQuery: createBaseQuery("Albums"),
    tagTypes: ["Album"],

    endpoints: (builder) => ({

        // GET /api/Albums
        getAlbums: builder.query<Album[], void>({
            query: () => ({
                url: "/",
                method: "GET",
            }),

            transformResponse: (
                response: ServiceResponse<Album[]>
            ) => response.payload,

            providesTags: ["Album"],
        }),

        // GET /api/Albums/by-id/{id}
        getAlbumById: builder.query<Album, number>({
            query: (id) => ({
                url: `/by-id/${id}`,
                method: "GET",
            }),

            transformResponse: (
                response: ServiceResponse<Album>
            ) => response.payload,

            providesTags: (_result, _error, id) => [
                { type: "Album", id },
            ],
        }),

        // POST /api/Albums
        createAlbum: builder.mutation<Album, CreateAlbumForm>({
            query: (albumData) => {
                const formData = new FormData();

                formData.append("Name", albumData.name);
                formData.append(
                    "AuthorId",
                    albumData.authorId.toString()
                );

                if (albumData.description) {
                    formData.append(
                        "Description",
                        albumData.description
                    );
                }

                if (albumData.image) {
                    formData.append("Image", albumData.image);
                }

                albumData.songIds.forEach((songId) => {
                    formData.append(
                        "SongIds",
                        songId.toString()
                    );
                });

                return {
                    url: "/",
                    method: "POST",
                    body: formData,
                };
            },

            transformResponse: (
                response: ServiceResponse<Album>
            ) => response.payload,

            invalidatesTags: ["Album"],
        }),

        // PUT /api/Albums
        updateAlbum: builder.mutation<Album, UpdateAlbumForm>({
            query: (albumData) => {
                const formData = new FormData();

                formData.append(
                    "Id",
                    albumData.id.toString()
                );

                formData.append("Name", albumData.name);

                formData.append(
                    "AuthorId",
                    albumData.authorId.toString()
                );

                if (albumData.description) {
                    formData.append(
                        "Description",
                        albumData.description
                    );
                }

                if (albumData.image) {
                    formData.append(
                        "Image",
                        albumData.image
                    );
                }

                albumData.songIds.forEach((songId) => {
                    formData.append(
                        "SongIds",
                        songId.toString()
                    );
                });

                return {
                    url: "/",
                    method: "PUT",
                    body: formData,
                };
            },

            transformResponse: (
                response: ServiceResponse<Album>
            ) => response.payload,

            invalidatesTags: (_result, _error, data) => [
                "Album",
                { type: "Album", id: data.id },
            ],
        }),

        // DELETE /api/Albums?id={id}
        deleteAlbum: builder.mutation<void, number>({
            query: (id) => ({
                url: `?id=${id}`,
                method: "DELETE",
            }),

            invalidatesTags: ["Album"],
        }),
    }),
});

export const {
    useGetAlbumsQuery,
    useGetAlbumByIdQuery,
    useCreateAlbumMutation,
    useUpdateAlbumMutation,
    useDeleteAlbumMutation,
} = albumApi;