import type {IUserForInfoItem} from "../Auth/IUserForInfoItem.ts";

export interface ISongForInfoItem {
    name: string;
    length: number;
    image: string | null;
    artist: IUserForInfoItem;
}