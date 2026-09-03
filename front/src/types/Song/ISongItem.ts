import type {ICommentItem} from "../Comment/ICommentItem.ts";
import type {IAlbumItem} from "../Album/IAlbumItem.ts";
import type {UserForInfo} from "../User/UserForInfo.ts";

export interface ISongItem {
    id: number;
    name: string;
    length: number;
    image: string | null;
    artist: UserForInfo;
    albums: IAlbumItem[];
    comments: ICommentItem[];
}