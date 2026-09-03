import type {UserForInfo} from "../User/UserForInfo.ts";

export interface ISongForInfoItem {
    name: string;
    length: number;
    image: string | null;
    artist: UserForInfo;
}