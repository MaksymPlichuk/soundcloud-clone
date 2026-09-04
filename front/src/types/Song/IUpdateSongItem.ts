export interface IUpdateSongItem {
    id: number;
    name: string;
    length: number;
    image: string | null;
    songFile: File;
    artistId: number;
    albumIds: number[];
    commentIds: number[];
}