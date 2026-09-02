import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { useNavigate } from "react-router-dom";

import {
    useCreateAlbumMutation,
} from "../../api/albumApi";

import {
    useGetUsersQuery,
} from "../../api/userApi";

import {
    useGetSongsQuery,
} from "../../api/songsApi";

const albumSchema = z.object({
    name: z
        .string()
        .min(1, "Введіть назву альбому")
        .max(
            200,
            "Назва не може бути довшою за 200 символів"
        ),

    authorId: z
        .number()
        .int()
        .positive("Виберіть автора"),

    description: z
        .string()
        .max(
            2000,
            "Опис не може бути довшим за 2000 символів"
        )
        .optional(),

    image: z
        .instanceof(File)
        .optional(),

    songIds: z.array(z.number()),
});

type AlbumFormData = z.infer<typeof albumSchema>;

const AlbumCreateForm = () => {
    const navigate = useNavigate();

    const {
        register,
        handleSubmit,
        setValue,
        watch,
        formState: { errors },
    } = useForm<AlbumFormData>({
        resolver: zodResolver(albumSchema),

        defaultValues: {
            name: "",
            authorId: 0,
            description: "",
            songIds: [],
        },
    });

    const image = watch("image");
    const selectedSongIds = watch("songIds");

    const {
        data: users = [],
        isLoading: usersLoading,
        isError: usersError,
    } = useGetUsersQuery();

    const {
        data: songs = [],
        isLoading: songsLoading,
        isError: songsError,
    } = useGetSongsQuery();

    const [
        createAlbum,
        {
            isLoading: isCreating,
            error: createError,
        },
    ] = useCreateAlbumMutation();

    const toggleSong = (songId: number) => {
        const current = selectedSongIds ?? [];

        if (current.includes(songId)) {
            setValue(
                "songIds",
                current.filter((id) => id !== songId),
                {
                    shouldValidate: true,
                }
            );
        } else {
            setValue(
                "songIds",
                [...current, songId],
                {
                    shouldValidate: true,
                }
            );
        }
    };

    const onSubmit = async (data: AlbumFormData) => {
        try {
            const album = await createAlbum({
                name: data.name,
                authorId: data.authorId,
                description: data.description,
                image: data.image,
                songIds: data.songIds,
            }).unwrap();

            navigate(`/albums/${album.id}`);
        } catch (error) {
            console.error(
                "Failed to create album:",
                error
            );
        }
    };

    return (
        <div className="min-h-screen bg-gray-950 px-6 py-10 text-gray-100">
            <div className="mx-auto max-w-6xl">

                <div className="mb-10">
                    <span className="text-sm font-semibold uppercase tracking-widest text-cyan-400">
                        Albums
                    </span>

                    <h1 className="mt-2 text-4xl font-bold tracking-tight">
                        Create album
                    </h1>

                    <p className="mt-3 max-w-2xl text-gray-500">
                        Create a new album and add your artwork,
                        author and tracks.
                    </p>
                </div>

                <form
                    onSubmit={handleSubmit(onSubmit)}
                    className="
                        grid
                        grid-cols-1
                        gap-12
                        lg:grid-cols-[380px_1fr]
                    "
                >
                    {/* IMAGE */}
                    <div>
                        <label className="mb-3 block text-sm font-semibold text-gray-200">
                            Album artwork
                        </label>

                        <div
                            className="
                                relative
                                aspect-square
                                overflow-hidden
                                rounded-2xl
                                border
                                border-gray-800
                                bg-gray-900
                                shadow-xl
                            "
                        >
                            {image ? (
                                <img
                                    src={URL.createObjectURL(image)}
                                    alt="Album artwork preview"
                                    className="h-full w-full object-cover"
                                />
                            ) : (
                                <div
                                    className="
                                        flex
                                        h-full
                                        w-full
                                        flex-col
                                        items-center
                                        justify-center
                                        bg-gradient-to-br
                                        from-gray-900
                                        via-gray-950
                                        to-cyan-950/40
                                    "
                                >
                                    <span className="text-8xl text-cyan-400">
                                        ♪
                                    </span>

                                    <span className="mt-5 text-sm text-gray-500">
                                        No artwork selected
                                    </span>
                                </div>
                            )}

                            <div
                                className="
                                    absolute
                                    inset-x-0
                                    bottom-0
                                    bg-gradient-to-t
                                    from-black/80
                                    to-transparent
                                    p-5
                                    pt-16
                                "
                            >
                                <label
                                    htmlFor="album-image"
                                    className="
                                        block
                                        cursor-pointer
                                        rounded-lg
                                        bg-cyan-400
                                        px-5
                                        py-3
                                        text-center
                                        text-sm
                                        font-bold
                                        text-gray-950
                                        transition
                                        hover:bg-cyan-300
                                    "
                                >
                                    {image
                                        ? "Change artwork"
                                        : "Choose artwork"}
                                </label>
                            </div>

                            <input
                                id="album-image"
                                type="file"
                                accept="image/*"
                                className="hidden"
                                onChange={(event) => {
                                    const file =
                                        event.target.files?.[0];

                                    if (file) {
                                        setValue(
                                            "image",
                                            file,
                                            {
                                                shouldValidate:
                                                    true,
                                            }
                                        );
                                    }
                                }}
                            />
                        </div>

                        {errors.image && (
                            <p className="mt-2 text-sm text-red-400">
                                {errors.image.message}
                            </p>
                        )}
                    </div>

                    {/* FIELDS */}
                    <div className="flex flex-col gap-7">

                        {/* NAME */}
                        <div>
                            <label
                                htmlFor="name"
                                className="mb-2 block text-sm font-semibold"
                            >
                                Album name
                                <span className="ml-1 text-cyan-400">
                                    *
                                </span>
                            </label>

                            <input
                                id="name"
                                type="text"
                                placeholder="Enter album name"
                                {...register("name")}
                                className="
                                    w-full
                                    rounded-xl
                                    border
                                    border-gray-800
                                    bg-gray-900
                                    px-4
                                    py-3.5
                                    text-gray-100
                                    outline-none
                                    transition
                                    focus:border-cyan-400
                                "
                            />

                            {errors.name && (
                                <p className="mt-2 text-sm text-red-400">
                                    {errors.name.message}
                                </p>
                            )}
                        </div>

                        {/* AUTHOR */}
                        <div>
                            <label
                                htmlFor="authorId"
                                className="mb-2 block text-sm font-semibold"
                            >
                                Author
                                <span className="ml-1 text-cyan-400">
                                    *
                                </span>
                            </label>

                            <select
                                id="authorId"
                                {...register("authorId", {
                                    valueAsNumber: true,
                                })}
                                disabled={usersLoading}
                                className="
                                    w-full
                                    rounded-xl
                                    border
                                    border-gray-800
                                    bg-gray-900
                                    px-4
                                    py-3.5
                                    outline-none
                                    transition
                                    focus:border-cyan-400
                                    disabled:opacity-50
                                "
                            >
                                <option value={0}>
                                    {usersLoading
                                        ? "Loading users..."
                                        : "Select author"}
                                </option>

                                {users.map((user) => (
                                    <option
                                        key={user.id}
                                        value={user.id}
                                    >
                                        {user.userName}
                                    </option>
                                ))}
                            </select>

                            {usersError && (
                                <p className="mt-2 text-sm text-red-400">
                                    Failed to load users.
                                </p>
                            )}

                            {errors.authorId && (
                                <p className="mt-2 text-sm text-red-400">
                                    {errors.authorId.message}
                                </p>
                            )}
                        </div>

                        {/* DESCRIPTION */}
                        <div>
                            <label
                                htmlFor="description"
                                className="mb-2 block text-sm font-semibold"
                            >
                                Description
                            </label>

                            <textarea
                                id="description"
                                rows={6}
                                placeholder="Tell listeners about this album..."
                                {...register("description")}
                                className="
                                    w-full
                                    resize-none
                                    rounded-xl
                                    border
                                    border-gray-800
                                    bg-gray-900
                                    px-4
                                    py-3.5
                                    outline-none
                                    transition
                                    focus:border-cyan-400
                                "
                            />

                            {errors.description && (
                                <p className="mt-2 text-sm text-red-400">
                                    {errors.description.message}
                                </p>
                            )}
                        </div>

                        {/* SONGS */}
                        <div>
                            <div className="mb-3 flex items-center justify-between">
                                <div>
                                    <label className="block text-sm font-semibold">
                                        Tracks
                                    </label>

                                    <p className="mt-1 text-xs text-gray-600">
                                        Select songs for this album.
                                    </p>
                                </div>

                                <span className="text-xs text-gray-500">
                                    {selectedSongIds.length} selected
                                </span>
                            </div>

                            <div className="max-h-80 overflow-y-auto rounded-xl border border-gray-800 bg-gray-900">
                                {songsLoading ? (
                                    <div className="p-6 text-center text-sm text-gray-500">
                                        Loading songs...
                                    </div>
                                ) : songsError ? (
                                    <div className="p-6 text-center text-sm text-red-400">
                                        Failed to load songs.
                                    </div>
                                ) : songs.length === 0 ? (
                                    <div className="p-6 text-center text-sm text-gray-500">
                                        No songs available.
                                    </div>
                                ) : (
                                    songs.map((song) => {
                                        const selected =
                                            selectedSongIds.includes(
                                                song.id
                                            );

                                        return (
                                            <button
                                                key={song.id}
                                                type="button"
                                                onClick={() =>
                                                    toggleSong(
                                                        song.id
                                                    )
                                                }
                                                className={`
                                                    flex
                                                    w-full
                                                    items-center
                                                    gap-4
                                                    border-b
                                                    border-gray-800
                                                    px-4
                                                    py-3
                                                    text-left
                                                    transition
                                                    last:border-b-0
                                                    ${
                                                    selected
                                                        ? "bg-cyan-400/10"
                                                        : "hover:bg-gray-800"
                                                }
                                                `}
                                            >
                                                <span
                                                    className={`
                                                        flex
                                                        h-5
                                                        w-5
                                                        shrink-0
                                                        items-center
                                                        justify-center
                                                        rounded
                                                        border
                                                        text-xs
                                                        ${
                                                        selected
                                                            ? "border-cyan-400 bg-cyan-400 text-gray-950"
                                                            : "border-gray-700"
                                                    }
                                                    `}
                                                >
                                                    {selected
                                                        ? "✓"
                                                        : ""}
                                                </span>

                                                <div className="min-w-0">
                                                    <p className="truncate font-medium">
                                                        {song.name}
                                                    </p>

                                                    <p className="truncate text-xs text-gray-500">
                                                        {song.artist.userName}
                                                    </p>
                                                </div>
                                            </button>
                                        );
                                    })
                                )}
                            </div>
                        </div>

                        {/* API ERROR */}
                        {createError && (
                            <div className="rounded-xl border border-red-900 bg-red-950/30 px-4 py-3 text-sm text-red-400">
                                Failed to create album.
                            </div>
                        )}

                        {/* ACTIONS */}
                        <div
                            className="
                                flex
                                flex-col-reverse
                                gap-4
                                border-t
                                border-gray-800
                                pt-7
                                sm:flex-row
                                sm:justify-end
                            "
                        >
                            <button
                                type="button"
                                onClick={() =>
                                    navigate("/albums")
                                }
                                disabled={isCreating}
                                className="
                                    rounded-xl
                                    border
                                    border-gray-800
                                    px-7
                                    py-3
                                    font-semibold
                                    text-gray-400
                                    transition
                                    hover:border-gray-600
                                    hover:text-gray-100
                                "
                            >
                                Cancel
                            </button>

                            <button
                                type="submit"
                                disabled={
                                    isCreating ||
                                    usersLoading ||
                                    songsLoading
                                }
                                className="
                                    rounded-xl
                                    bg-cyan-400
                                    px-8
                                    py-3
                                    font-bold
                                    text-gray-950
                                    transition
                                    hover:bg-cyan-300
                                    disabled:cursor-not-allowed
                                    disabled:opacity-50
                                "
                            >
                                {isCreating
                                    ? "Creating..."
                                    : "Create album"}
                            </button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default AlbumCreateForm;