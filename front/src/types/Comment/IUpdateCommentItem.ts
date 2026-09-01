export interface IUpdateCommentItem {
    id: number;
    timeCode: number | null;
    commentText: string;
    authorId: number;
    songId: number;
}