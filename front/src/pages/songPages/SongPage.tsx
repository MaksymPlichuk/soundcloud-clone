import React, {useEffect} from "react";
import { useParams } from "react-router-dom";
import {useGetSongByIdQuery} from "../../api/songsApi.ts";
import APP_ENV from "../../env";
// import AlbumList from "./AlbumList";

const SongPage = () => {
    const { id } = useParams();

    useEffect(() => {
        console.log(id)
    }, [])

    const {data: song, isLoading,isError} = useGetSongByIdQuery(Number(id), { skip: !id });

    if (isLoading) {
        return (
            <div className="flex min-h-screen items-center justify-center bg-gray-950 text-cyan-400">
                Loading song...
            </div>
        );
    }

    if (isError || !song) {
        return (
            <div className="flex min-h-screen items-center justify-center bg-gray-950 text-red-400">
                Failed to load song.
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
                        Song Details
                    </h1>
                </div>

                <div className="max-w-sm rounded overflow-hidden shadow-lg m-5 bg-gray-900 border border-gray-800">
                    {song.image ? (
                        <img className="w-full h-64 object-cover" src={`${APP_ENV.IMG_URL}${song.image}`} alt={song.name} />
                    ) : (
                        <div className="w-full h-64 flex items-center justify-center bg-gray-800 text-gray-500">
                            No image available
                        </div>
                    )}

                    <div className="px-6 py-4">
                        <div className="text-xs text-gray-500 mb-1">ID: {song.id}</div>
                        <div className="font-bold text-2xl text-cyan-400 mb-2">{song.name}</div>

                        <div className="mt-4 border-t border-gray-700 pt-4">
                            <h3 className="text-lg font-semibold mb-2">Author Info</h3>
                            <div className="text-gray-400">Author ID: {song.artist.id}</div>
                            <div className="text-gray-400">Author: <span className={"font-bold"}>{song.artist.userName}</span> </div>
                        </div>

                        <div className="mt-4 border-t border-gray-700 pt-4">
                            <h3 className="text-lg font-semibold mb-2">Albums ({song.albums?.length || 0})</h3>
                            {song.albums && song.albums.length > 0 ? (
                                <ul className="list-decimal list-inside text-gray-400">
                                    {song.albums.map((album) => (
                                        <li key={album.id} className="truncate">
                                            {album.name} {album.image ? (
                                            <img className="object-cover" src={`${APP_ENV.IMG_URL}${album.image}`} alt={album.name} />
                                        ) : (
                                            <div className="flex items-center justify-center bg-gray-800 text-gray-500">
                                                No image available
                                            </div>
                                        )}
                                            <span className="text-xs text-gray-500">({album.author.userName})</span>
                                        </li>
                                    ))}
                                </ul>
                            ) : (
                                <p className="text-gray-500 text-sm">No albums found for this song.</p>
                            )}
                        </div>
                        <div className="mt-4 border-t border-gray-700 pt-4">
                            <h3 className="text-lg font-semibold mb-2">Comments ({song.comments?.length || 0})</h3>
                            {song.comments && song.comments.length > 0 ? (
                                <ul className="list-decimal list-inside text-gray-400">
                                    {song.comments.map((comment) => (
                                        <li key={id} className="truncate">
                                            {comment.timeCode}<span className="text-xs text-gray-500">({comment.author.userName})</span>
                                            <p className="text-gray-400">{comment.commentText}</p>
                                        </li>
                                    ))}
                                </ul>
                            ) : (
                                <p className="text-gray-500 text-sm">No comments found for this song.</p>
                            )}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default SongPage;