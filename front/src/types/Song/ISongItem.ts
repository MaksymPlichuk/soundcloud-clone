import type {ICommentItem} from "../Comment/ICommentItem.ts";
import type {IAlbumItem} from "../Album/IAlbumItem.ts";
import type {IUserForInfoItem} from "../Auth/IUserForInfoItem.ts";

export interface ISongItem {
    id: number;
    name: string;
    length: number;
    image: string | null;
    artist: IUserForInfoItem;
    albums: IAlbumItem[];
    comments: ICommentItem[];
}