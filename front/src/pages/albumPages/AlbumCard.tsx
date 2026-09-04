import {useNavigate} from "react-router-dom";
import type {IAlbumItem} from "../../types/Album/IAlbumItem.ts";

type AlbumCardProps = {
    album : IAlbumItem,
    onClick?: () => void;
};

const AlbumCard = ({album, onClick }: AlbumCardProps) => {
    const navigate = useNavigate();

    const handleClick = () => {
        if (onClick) {
            onClick();
            return;
        }

        navigate(`/album/${album.id}`);
    };

    const authorName = album.author?.userName ?? "Unknown artist";

    return (
        <article
            onClick={handleClick}
            className="
                group cursor-pointer overflow-hidden rounded-2xl
                border border-gray-800 bg-gray-900/80 shadow-lg shadow-black/20
                transition-all duration-300
                hover:-translate-y-1 hover:border-cyan-400/60 hover:bg-gray-800
                hover:shadow-xl hover:shadow-cyan-500/10"
        >
            <div className="relative aspect-square overflow-hidden bg-gray-950">
                {album.image ? (
                    <img
                        src={album.image}
                        alt={album.name}
                        className="
                            h-full w-full object-cover
                            transition-transform duration-500
                            group-hover:scale-105"
                    />
                ) : (
                    <div
                        className="
                            flex h-full w-full items-center justify-center
                            bg-gradient-to-br from-gray-900 via-gray-950 to-cyan-950/40"
                    >
                        <span
                            className="
                                text-7xl font-light text-cyan-400
                                drop-shadow-[0_0_20px_rgba(34,211,238,0.35)]"
                        >
                            ♪
                        </span>
                    </div>
                )}

                <div
                    className="
                        absolute inset-0 flex items-center justify-center
                        bg-black/0 transition duration-300
                        group-hover:bg-black/30"
                >
                    <div
                        className="
                            flex h-12 w-12 scale-75 items-center justify-center
                            rounded-full bg-cyan-400 text-xl text-gray-950
                            opacity-0 shadow-lg transition-all duration-300
                            group-hover:scale-100 group-hover:opacity-100"
                    >
                        ▶
                    </div>
                </div>
            </div>

            <div className="p-5">
                <div className="mb-2 flex items-start justify-between gap-3">
                    <h2 className="
                            min-w-0 flex-1 truncate text-lg font-bold text-gray-100
                            transition-colors group-hover:text-cyan-400"
                        title={album.name}
                    >
                        {album.name}
                    </h2>

                    <span className="shrink-0 text-xs text-gray-600">#{album.id}</span>
                </div>

                <p className="truncate text-sm font-medium text-cyan-400/80">
                    {authorName}
                </p>

                {album.description && (
                    <p className="mt-3 line-clamp-2 text-sm leading-6 text-gray-500">
                        {album.description}
                    </p>
                )}

                <div
                    className="
                        mt-5 flex items-center justify-between
                        border-t border-gray-800 pt-4"
                >
                    <span className="text-xs font-medium uppercase tracking-wider text-gray-600">
                        Album
                    </span>

                    <span className="text-sm text-gray-600 transition-colors group-hover:text-cyan-400">
                        Open →
                    </span>
                </div>
            </div>
        </article>
    );
};

export default AlbumCard;

