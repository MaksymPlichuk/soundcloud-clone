export interface ICreateAlbumItem {
    name: string;
    description: string | null;
    authorId: number;
    songIds: number[];
}