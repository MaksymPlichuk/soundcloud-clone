import type {UserForInfo} from "../User/UserForInfo.ts";
import type {ISongForInfo} from "../Song/ISongForInfo.ts";

export interface IAlbumItem {
    id: number;
    name: string;
    description: string | null;
    authorId: number;
    author: UserForInfo;
    songs: ISongForInfo[];
    image: string | null;
}