import type {ISongItem} from "../Song/ISongItem.ts";
import type {UserForInfo} from "../User/UserForInfo.ts";

export interface IAlbumItem {
    id: number;
    name: string;
    description: string | null;
    authorId: number;
    author: UserForInfo;
    songs: ISongItem[];
    image: string | null;
}