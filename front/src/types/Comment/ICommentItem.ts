import type {IUserForInfoItem} from "../Auth/IUserForInfoItem.ts";
import type {ISongForInfoItem} from "../Song/ISongForInfoItem.ts";

export interface ICommentItem {
    timeCode: number | null;
    commentText: string;
    author: IUserForInfoItem;
    song: ISongForInfoItem;
}