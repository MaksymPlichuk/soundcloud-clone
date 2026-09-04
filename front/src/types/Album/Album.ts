import type { UserForInfo } from "../User/UserForInfo";
import type { ISongForInfo } from "../Song/ISongForInfo";

export type Album = {
    id: number;
    name: string;
    description?: string | null;
    authorId: number;
    author: UserForInfo;
    songs: ISongForInfo[];
    image?: string | null;
};
export type CreateAlbumForm = {
    name: string;
    description?: string;
    authorId: number;
    image?: File;
    songIds: number[];
};

export type UpdateAlbumForm = {
    id: number;
    name: string;
    description?: string;
    authorId: number;
    image?: File;
    songIds: number[];
};