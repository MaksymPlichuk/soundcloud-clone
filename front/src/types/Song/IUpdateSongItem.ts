export interface IUpdateSongItem {
    id: number;
    name: string;
    length: number;
    image: string | null;
    artistId: number;
    albumIds: number[];
    commentIds: number[];
}