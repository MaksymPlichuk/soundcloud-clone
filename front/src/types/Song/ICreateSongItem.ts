export interface ICreateSongItem {
    name: string;
    length: number;
    image: string | null;
    songFile: File;
    artistId: number;
    albumIds: number[];
    commentIds: number[];
}