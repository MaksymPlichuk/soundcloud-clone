import type { IUserForInfo } from "../User/IUserForInfo";

export interface ISongForInfo {
    id: number;
    name: string;
    length: number;
    songFile: string;
    image?: string | null;
    artist: IUserForInfo;
}