import AlbumList from "./AlbumList";
import { useGetAlbumsQuery } from "../../api/albumApi";

const AlbumsPage = () => {
    const {
        data: albums = [],
        isLoading,
        isError,
    } = useGetAlbumsQuery();

    if (isLoading) {
        return (
            <div className="flex min-h-screen items-center justify-center bg-gray-950 text-cyan-400">
                Loading albums...
            </div>
        );
    }

    if (isError) {
        return (
            <div className="flex min-h-screen items-center justify-center bg-gray-950 text-red-400">
                Failed to load albums.
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
                        Albums
                    </h1>
                </div>

                <AlbumList albums={albums} />
            </div>
        </div>
    );
};

export default AlbumsPage;