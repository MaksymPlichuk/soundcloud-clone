
import {useGetSongsQuery} from "../../api/songsApi.ts";
import type {ISongItem} from "../../types/Song/ISongItem.ts";
import SongCard from "./SongCard.tsx";


const SongList = () => {

    const {data: songs, isLoading, isError} = useGetSongsQuery();

    return (
        <div className="min-h-screen bg-gray-950 px-6 py-10 text-gray-100">
            <div className="mx-auto max-w-6xl">

                <div className="grid justify-center p-5 rounded-b-3xl h-full grid-cols-4 gap-5">
                    {isLoading ? (<div>Loading...</div>) :
                        isError ? (<div>Error loading</div>) :
                            (
                                songs &&
                                songs.map((song: ISongItem) => (
                                    <SongCard song={song} key={song.id}/>
                                )))
                    }
                </div>
            </div>
        </div>
    )
};

export default SongList;