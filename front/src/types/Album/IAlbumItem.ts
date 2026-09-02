import type {ISongForInfoItem} from "../Song/ISongForInfoItem.ts";
import type {IUserForInfoItem} from "../Auth/IUserForInfoItem.ts";

export interface IAlbumItem {
    id: number;
    name: string;
    description: string | null;
    author: IUserForInfoItem;
    songs: ISongForInfoItem[];
}