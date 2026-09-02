export interface IUpdateAlbumItem {
    id: number;
    name: string;
    description: string | null;
    authorId: number;
    songIds: number[];
}