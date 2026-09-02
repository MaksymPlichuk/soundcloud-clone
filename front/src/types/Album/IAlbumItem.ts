import type {ISongItem} from "../Song/ISongItem.ts";
import type {IUserForInfoItem} from "../Auth/IUserForInfoItem.ts";

export interface IAlbumItem {
    id: number;
    name: string;
    description: string | null;
    authorId: number;
    author: IUserForInfoItem;
    songs: ISongItem[];
    image: string | null;
}