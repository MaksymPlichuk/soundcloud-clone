import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { useNavigate } from "react-router-dom";

import { useCreateAlbumMutation } from "../../api/albumApi";
import { useGetUsersQuery } from "../../api/userApi";
import { useGetSongsQuery } from "../../api/songsApi";

import { FormInput, FormTextarea, FormSelect, ImageFormInput } from "../../components/FormInputs.tsx";

const albumSchema = z.object({
    name: z.string().min(1, "Введіть назву альбому").max(200, "Назва не може бути довшою за 200 символів"),
    authorId: z.number().int().positive("Виберіть автора"),
    description: z.string().max(2000, "Опис не може бути довшим за 2000 символів").optional(),
    image: z.instanceof(File).optional(),
    songIds: z.array(z.number()),
});

type AlbumFormData = z.infer<typeof albumSchema>;

const AlbumCreateForm = () => {
    const navigate = useNavigate();

    const { control, handleSubmit, setValue, watch } = useForm<AlbumFormData>({
        resolver: zodResolver(albumSchema),
        defaultValues: { name: "", authorId: 0, description: "", songIds: [] },
    });

    const selectedSongIds = watch("songIds");

    const { data: users = [], isLoading: usersLoading, isError: usersError } = useGetUsersQuery();
    const { data: songs = [], isLoading: songsLoading, isError: songsError } = useGetSongsQuery();
    const [createAlbum, { isLoading: isCreating, error: createError }] = useCreateAlbumMutation();

    const toggleSong = (songId: number) => {
        const current = selectedSongIds ?? [];
        setValue("songIds", current.includes(songId) ? current.filter(id => id !== songId) : [...current, songId], { shouldValidate: true });
    };

    const onSubmit = async (data: AlbumFormData) => {
        try {
            const album = await createAlbum(data).unwrap();
            navigate(`/albums/${album.id}`);
        } catch (error) {
            console.error("Failed to create album:", error);
        }
    };

    const authorOptions = users.map(user => ({ value: user.id, label: user.userName }));

    return (
        <div className="min-h-screen bg-gray-950 px-6 py-10 text-gray-100">
            <div className="mx-auto max-w-6xl">
                <div className="mb-10">
                    <span className="text-sm font-semibold uppercase tracking-widest text-cyan-400">Albums</span>
                    <h1 className="mt-2 text-4xl font-bold tracking-tight">Create album</h1>
                    <p className="mt-3 max-w-2xl text-gray-500">Create a new album and add your artwork, author and tracks.</p>
                </div>

                <form onSubmit={handleSubmit(onSubmit)} className="grid grid-cols-1 gap-12 lg:grid-cols-[380px_1fr]">
                    {/* IMAGE */}
                    <ImageFormInput control={control} name="image" label="Album artwork" />

                    {/* FIELDS */}
                    <div className="flex flex-col gap-7">
                        <FormInput
                            control={control}
                            name="name"
                            label={<>Album name<span className="ml-1 text-cyan-400">*</span></>}
                            placeholder="Enter album name"
                        />

                        <div>
                            <FormSelect
                                control={control}
                                name="authorId"
                                label={<>Author<span className="ml-1 text-cyan-400">*</span></>}
                                options={authorOptions}
                                disabled={usersLoading}
                                placeholder={usersLoading ? "Loading users..." : "Select author"}
                            />
                            {usersError && <p className="mt-2 text-sm text-red-400">Failed to load users.</p>}
                        </div>

                        <FormTextarea
                            control={control}
                            name="description"
                            label="Description"
                            placeholder="Tell listeners about this album..."
                        />

                        {/* SONGS */}
                        <div>
                            <div className="mb-3 flex items-center justify-between">
                                <div>
                                    <label className="block text-sm font-semibold">Tracks</label>
                                    <p className="mt-1 text-xs text-gray-600">Select songs for this album.</p>
                                </div>
                                <span className="text-xs text-gray-500">{selectedSongIds.length} selected</span>
                            </div>

                            <div className="max-h-80 overflow-y-auto rounded-xl border border-gray-800 bg-gray-900">
                                {songsLoading ? (
                                    <div className="p-6 text-center text-sm text-gray-500">Loading songs...</div>
                                ) : songsError ? (
                                    <div className="p-6 text-center text-sm text-red-400">Failed to load songs.</div>
                                ) : songs.length === 0 ? (
                                    <div className="p-6 text-center text-sm text-gray-500">No songs available.</div>
                                ) : (
                                    songs.map((song) => {
                                        const selected = selectedSongIds.includes(song.id);
                                        return (
                                            <button
                                                key={song.id}
                                                type="button"
                                                onClick={() => toggleSong(song.id)}
                                                className={`flex w-full items-center gap-4 border-b border-gray-800 px-4 py-3 text-left transition last:border-b-0 ${selected ? "bg-cyan-400/10" : "hover:bg-gray-800"}`}
                                            >
                                                <span className={`flex h-5 w-5 shrink-0 items-center justify-center rounded border text-xs ${selected ? "border-cyan-400 bg-cyan-400 text-gray-950" : "border-gray-700"}`}>
                                                    {selected ? "✓" : ""}
                                                </span>
                                                <div className="min-w-0">
                                                    <p className="truncate font-medium">{song.name}</p>
                                                    <p className="truncate text-xs text-gray-500">{song.artist.userName}</p>
                                                </div>
                                            </button>
                                        );
                                    })
                                )}
                            </div>
                        </div>

                        {createError && <div className="rounded-xl border border-red-900 bg-red-950/30 px-4 py-3 text-sm text-red-400">Failed to create album.</div>}

                        <div className="flex flex-col-reverse gap-4 border-t border-gray-800 pt-7 sm:flex-row sm:justify-end">
                            <button type="button" onClick={() => navigate("/albums")} disabled={isCreating} className="rounded-xl border border-gray-800 px-7 py-3 font-semibold text-gray-400 transition hover:border-gray-600 hover:text-gray-100">
                                Cancel
                            </button>
                            <button type="submit" disabled={isCreating || usersLoading || songsLoading} className="rounded-xl bg-cyan-400 px-8 py-3 font-bold text-gray-950 transition hover:bg-cyan-300 disabled:cursor-not-allowed disabled:opacity-50">
                                {isCreating ? "Creating..." : "Create album"}
                            </button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default AlbumCreateForm;