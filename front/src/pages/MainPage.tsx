import {useCreateSongMutation, useGetSongsQuery} from "../api/songsApi.ts";
import {useEffect} from "react";
import type {ICreateSongItem} from "../types/Song/ICreateSongItem.ts";
import {Link} from "react-router-dom";

const MainPage = () => {

    const {data: songs, isLoading, isError} = useGetSongsQuery();
    const [createSong] = useCreateSongMutation();

    useEffect(() => {
        if (songs) {
            console.log(songs);}
    }, [songs])

    const testCreate = async () => {
        const data:ICreateSongItem = {
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
        }
        catch (error) {
            console.log(error);
        }

    }

    return (
        <>
            <div className="flex justify-center mt-5 flex-col">

            <div className="grid grid-cols-5 row-auto justify-center rounded-b-3xl h-full gap-5 mb-10">

                <div className="max-w-sm rounded overflow-hidden shadow-lg m-5">
                    <img className="w-full" alt={"test"}/>
                    <div className="px-6 py-4">
                        <div className="font-bold text-xl mb-2">name</div>
                        <p className="text-gray-700 text-base">
                            desc
                        </p>
                    </div>
                    <div className="px-6 pt-4 pb-2">
                        <span
                            className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">#photography</span>
                        <span
                            className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">#travel</span>
                        <span
                            className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">#winter</span>
                    </div>
                </div>
                <div className="px-6 pt-4 pb-2 bg-amber-300" onClick={testCreate}></div>
                <Link to={"/items"}>

                <div className={"bg-emerald-500 h-full"}>f</div>
                </Link>
                <div className={"bg-emerald-300 h-full"}>f</div>
            </div>
                <div className={"bg-emerald-300 h-full"}>f</div>
                <div className={"bg-emerald-300 h-full"}>f</div><div className={"bg-emerald-300 h-full"}>f</div>
                <div className={"bg-emerald-300 h-full"}>f</div><div className={"bg-emerald-300 h-full"}>f</div>
                <div className={"bg-emerald-300 h-full"}>f</div><div className={"bg-emerald-300 h-full"}>f</div>
                <div className={"bg-emerald-300 h-full"}>f</div>
            <div className="text-center text-cyan-700">sdsdsdssd</div>
            </div>
        </>
    );
}
export default MainPage;