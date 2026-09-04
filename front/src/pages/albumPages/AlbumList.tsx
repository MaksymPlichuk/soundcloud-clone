import {useGetAlbumsQuery} from "../../api/albumApi.ts";
import AlbumCard from "./AlbumCard.tsx";
import type {IAlbumItem} from "../../types/Album/IAlbumItem.ts";


const AlbumList = () => {

    const {data: albums, isLoading, isError} = useGetAlbumsQuery();

    return (
        <div className="min-h-screen bg-gray-950 px-6 py-10 text-gray-100">
            <div className="mx-auto max-w-6xl">

                <div className="grid justify-center p-5 rounded-b-3xl h-full grid-cols-4 gap-5">
                    {isLoading ? (<div>Loading...</div>) :
                        isError ? (<div>Error</div>) :
                            (
                                albums &&
                                albums.map((album: IAlbumItem) => (
                                    <AlbumCard album={album} key={album.id}/>
                                )))
                    }
                </div>
            </div>
        </div>
    )
};

export default AlbumList;