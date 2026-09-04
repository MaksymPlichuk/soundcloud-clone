import {useCreateSongMutation, useGetSongsQuery} from "../api/songsApi.ts";
import {useEffect} from "react";
import type {ICreateSongItem} from "../types/Song/ICreateSongItem.ts";
import {Link} from "react-router-dom";
import SongCard from "./songPages/SongCard.tsx";
import SongList from "./songPages/SongList.tsx";

const MainPage = () => {

    const {data: songs, isLoading, isError} = useGetSongsQuery();
    const [createSong] = useCreateSongMutation();

    useEffect(() => {
        if (songs) {
            console.log(songs);
        }
    }, [songs])

    const testCreate = async () => {
        const data: ICreateSongItem = {
            name: "test",
            length: 0,
            image: "test",
            artistId: 8,
            albumIds: [],
            commentIds: [],
        }
        try {
            console.log(data)
            let resp = await createSong(data).unwrap();
            console.log(resp);
        } catch (error) {
            console.log(error);
        }

    }

    return (
        <>
            <div className="flex justify-center flex-col ">
                    <SongList/>
            </div>
        </>
    );
}
export default MainPage;