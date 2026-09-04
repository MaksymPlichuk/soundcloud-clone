import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, Controller } from "react-hook-form";
import { z } from "zod";
import { useNavigate } from "react-router-dom";

import { useCreateSongMutation } from "../../api/songsApi";
import { useGetAlbumsQuery } from "../../api/albumApi";

import { FormInput, ImageFormInput } from "../../components/FormInputs.tsx";

const songSchema = z.object({
    name: z.string().min(1, "Введіть назву пісні").max(200, "Назва не може бути довшою за 200 символів"),
    length: z.coerce.number().min(1, "Введіть тривалість пісні"),
    artistId: z.coerce.number().min(1, "Введіть ID виконавця"),
    image: z.instanceof(File).optional(),
    songFile: z.instanceof(File, { message: "Файл пісні обов'язковий" }),
    albumIds: z.array(z.number()),
    commentIds: z.array(z.number()).default([]),
});

type SongFormData = z.infer<typeof songSchema>;

const SongCreateForm = () => {
    const navigate = useNavigate();

    const { control, handleSubmit, setValue, watch, formState: { errors } } = useForm<SongFormData>({
        resolver: zodResolver(songSchema),
        defaultValues: { name: "", length: 0, artistId: 0, albumIds: [], commentIds: [] },
    });

    const selectedAlbumIds = watch("albumIds");

    const { data: albums = [], isLoading: albumsLoading, isError: albumsError } = useGetAlbumsQuery();
    const [createSong, { isLoading: isCreating, error: createError }] = useCreateSongMutation();

    const toggleAlbum = (albumId: number) => {
        const current = selectedAlbumIds ?? [];
        setValue("albumIds", current.includes(albumId) ? current.filter(id => id !== albumId) : [...current, albumId], { shouldValidate: true });
    };

    const onSubmit = async (data: SongFormData) => {
        try {
            const formData = new FormData();
            formData.append("name", data.name);
            formData.append("length", data.length.toString());
            formData.append("artistId", data.artistId.toString());

            if (data.image) {
                formData.append("image", data.image);
            }
            formData.append("songFile", data.songFile);

            data.albumIds.forEach(id => {
                formData.append("albumIds", id.toString());
            });
            data.commentIds.forEach(id => {
                formData.append("commentIds", id.toString());
            });
            
            const song = await createSong(formData as any).unwrap();
            navigate(`/songs/${song.id}`);
        } catch (error) {
            console.error("Failed to create song:", error);
        }
    };

    return (
        <div className="min-h-screen bg-gray-950 px-6 py-10 text-gray-100">
            <div className="mx-auto max-w-6xl">
                <div className="mb-10">
                    <span className="text-sm font-semibold uppercase tracking-widest text-cyan-400">Songs</span>
                    <h1 className="mt-2 text-4xl font-bold tracking-tight">Create song</h1>
                    <p className="mt-3 max-w-2xl text-gray-500">Upload an audio file, add artwork, and specify the artist ID.</p>
                </div>

                <form onSubmit={handleSubmit(onSubmit)} className="grid grid-cols-1 gap-12 lg:grid-cols-[380px_1fr]">
                    {/* IMAGE */}
                    <ImageFormInput control={control} name="image" label="Song artwork" />

                    {/* FIELDS */}
                    <div className="flex flex-col gap-7">
                        <FormInput
                            control={control}
                            name="name"
                            label={<>Song name<span className="ml-1 text-cyan-400">*</span></>}
                            placeholder="Enter song name"
                        />

                        <FormInput
                            control={control}
                            name="length"
                            type="number"
                            label={<>Length (seconds)<span className="ml-1 text-cyan-400">*</span></>}
                            placeholder="Enter duration in seconds"
                        />

                        {/* ARTIST ID INPUT */}
                        <FormInput
                            control={control}
                            name="artistId"
                            type="number"
                            label={<>Artist ID<span className="ml-1 text-cyan-400">*</span></>}
                            placeholder="Enter artist ID"
                        />

                        {/* AUDIO FILE UPLOAD */}
                        <div>
                            <label className="mb-1 block text-sm font-medium">
                                Audio File<span className="ml-1 text-cyan-400">*</span>
                            </label>
                            <Controller
                                name="songFile"
                                control={control}
                                render={({ field: { onChange } }) => (
                                    <input
                                        type="file"
                                        accept="audio/*"
                                        onChange={(e) => onChange(e.target.files?.[0])}
                                        className="w-full rounded-xl border border-gray-800 bg-gray-900 px-4 py-3 text-sm text-gray-100 focus:border-cyan-400 focus:outline-none"
                                    />
                                )}
                            />
                            {errors.songFile && <p className="mt-2 text-sm text-red-400">{errors.songFile.message}</p>}
                        </div>

                        {/* ALBUMS */}
                        <div>
                            <div className="mb-3 flex items-center justify-between">
                                <div>
                                    <label className="block text-sm font-semibold">Albums</label>
                                    <p className="mt-1 text-xs text-gray-600">Select albums for this song (optional).</p>
                                </div>
                                <span className="text-xs text-gray-500">{selectedAlbumIds.length} selected</span>
                            </div>

                            <div className="max-h-80 overflow-y-auto rounded-xl border border-gray-800 bg-gray-900">
                                {albumsLoading ? (
                                    <div className="p-6 text-center text-sm text-gray-500">Loading albums...</div>
                                ) : albumsError ? (
                                    <div className="p-6 text-center text-sm text-red-400">Failed to load albums.</div>
                                ) : albums.length === 0 ? (
                                    <div className="p-6 text-center text-sm text-gray-500">No albums available.</div>
                                ) : (
                                    albums.map((album: any) => {
                                        const selected = selectedAlbumIds.includes(album.id);
                                        return (
                                            <button
                                                key={album.id}
                                                type="button"
                                                onClick={() => toggleAlbum(album.id)}
                                                className={`flex w-full items-center gap-4 border-b border-gray-800 px-4 py-3 text-left transition last:border-b-0 ${selected ? "bg-cyan-400/10" : "hover:bg-gray-800"}`}
                                            >
                                                <span className={`flex h-5 w-5 shrink-0 items-center justify-center rounded border text-xs ${selected ? "border-cyan-400 bg-cyan-400 text-gray-950" : "border-gray-700"}`}>
                                                    {selected ? "✓" : ""}
                                                </span>
                                                <div className="min-w-0">
                                                    <p className="truncate font-medium">{album.name}</p>
                                                </div>
                                            </button>
                                        );
                                    })
                                )}
                            </div>
                        </div>

                        {createError && <div className="rounded-xl border border-red-900 bg-red-950/30 px-4 py-3 text-sm text-red-400">Failed to create song.</div>}

                        <div className="flex flex-col-reverse gap-4 border-t border-gray-800 pt-7 sm:flex-row sm:justify-end">
                            <button type="button" onClick={() => navigate("/songs")} disabled={isCreating} className="rounded-xl border border-gray-800 px-7 py-3 font-semibold text-gray-400 transition hover:border-gray-600 hover:text-gray-100">
                                Cancel
                            </button>
                            <button type="submit" disabled={isCreating || albumsLoading} className="rounded-xl bg-cyan-400 px-8 py-3 font-bold text-gray-950 transition hover:bg-cyan-300 disabled:cursor-not-allowed disabled:opacity-50">
                                {isCreating ? "Creating..." : "Create song"}
                            </button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default SongCreateForm;