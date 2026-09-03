import type {UserForInfo} from "../User/UserForInfo.ts";
import type {ISongForInfoItem} from "../Song/ISongForInfoItem.ts";

export interface ICommentItem {
    timeCode: number | null;
    commentText: string;
    author: UserForInfo;
    song: ISongForInfoItem;
}