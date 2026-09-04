export interface ICreateSongItem {
    name: string;
    length: number;
    image: File | undefined;
    songFile: File;
    artistId: number;
    albumIds: number[];
    commentIds: number[];
}