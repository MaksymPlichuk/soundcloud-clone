import type { UserForInfo } from "../User/UserForInfo";

export interface ISongForInfo {
    id: number;
    name: string;
    length: number;
    songFile: string;
    image?: string | null;
    artist: UserForInfo;
}