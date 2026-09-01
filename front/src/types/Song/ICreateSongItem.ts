export interface ICreateSongItem {
    name: string;
    length: number;
    image: string | null;
    artistId: number;
    albumIds: number[];
    commentIds: number[];
}