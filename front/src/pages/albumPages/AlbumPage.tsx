import React, {useEffect} from "react";
import { useParams } from "react-router-dom";
import { useGetAlbumByIdQuery } from "../../api/albumApi";
// import AlbumList from "./AlbumList";

const AlbumsPage = () => {
    const { id } = useParams();

    useEffect(() => {
        console.log(id)
    }, [])

    const {data: album, isLoading,isError} = useGetAlbumByIdQuery(Number(id), { skip: !id });

    if (isLoading) {
        return (
            <div className="flex min-h-screen items-center justify-center bg-gray-950 text-cyan-400">
                Loading albums...
            </div>
        );
    }

    if (isError || !album) {
        return (
            <div className="flex min-h-screen items-center justify-center bg-gray-950 text-red-400">
                Failed to load album.
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-gray-950 px-6 py-10 text-gray-100">
            <div className="mx-auto max-w-7xl">
                <div className="mb-10">
                    <span className="text-sm font-semibold uppercase tracking-widest text-cyan-400">
                        Library
                    </span>
                    <h1 className="mt-2 text-4xl font-bold">
                        Album Details
                    </h1>
                </div>

                <div className="max-w-sm rounded overflow-hidden shadow-lg m-5 bg-gray-900 border border-gray-800">
                    {album.image ? (
                        <img className="w-full h-64 object-cover" src={album.image} alt={album.name} />
                    ) : (
                        <div className="w-full h-64 flex items-center justify-center bg-gray-800 text-gray-500">
                            No image available
                        </div>
                    )}

                    <div className="px-6 py-4">
                        <div className="text-xs text-gray-500 mb-1">ID: {album.id}</div>
                        <div className="font-bold text-2xl text-cyan-400 mb-2">{album.name}</div>

                        <p className="text-gray-300 text-base mb-4">
                            {album.description || "No description provided."}
                        </p>

                        <div className="mt-4 border-t border-gray-700 pt-4">
                            <h3 className="text-lg font-semibold mb-2">Author Info</h3>
                            <div className="text-gray-400">Author ID: {album.authorId}</div>
                            <div className="text-gray-400">Author Name: {album.author.userName}</div>
                        </div>

                        <div className="mt-4 border-t border-gray-700 pt-4">
                            <h3 className="text-lg font-semibold mb-2">Songs ({album.songs?.length || 0})</h3>
                            {album.songs && album.songs.length > 0 ? (
                                <ul className="list-decimal list-inside text-gray-400">
                                    {album.songs.map((song) => (
                                        <li key={song.id} className="truncate">
                                            {song.name} <span className="text-xs text-gray-500">({song.length}s)</span>
                                        </li>
                                    ))}
                                </ul>
                            ) : (
                                <p className="text-gray-500 text-sm">No songs found in this album.</p>
                            )}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AlbumsPage;