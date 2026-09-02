import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

const albumSchema = z.object({
    name: z
        .string()
        .min(1, "Введіть назву альбому")
        .max(200, "Назва не може бути довшою за 200 символів"),

    authorId: z
        .number()
        .int()
        .positive("Виберіть автора"),

    description: z
        .string()
        .max(2000, "Опис не може бути довшим за 2000 символів")
        .optional(),

    image: z
        .instanceof(File)
        .optional(),
});

type AlbumFormData = z.infer<typeof albumSchema>;

type AlbumUpdateFormProps = {
    album: {
        id: number;
        name: string;
        description?: string;
        authorId: number;
        image?: string;
    };
};

const AlbumUpdateForm = ({ album }: AlbumUpdateFormProps) => {
    const {
        register,
        handleSubmit,
        setValue,
        watch,
        formState: { errors },
    } = useForm<AlbumFormData>({
        resolver: zodResolver(albumSchema),
        defaultValues: {
            name: album.name,
            authorId: album.authorId,
            description: album.description ?? "",
        },
    });

    const image = watch("image");

    const onSubmit = (data: AlbumFormData) => {
        const formData = new FormData();

        formData.append("Name", data.name);
        formData.append("AuthorId", data.authorId.toString());

        if (data.description) {
            formData.append("Description", data.description);
        }

        if (data.image) {
            formData.append("Image", data.image);
        }

        console.log("Update album:", album.id);
        console.log(
            "FormData:",
            Object.fromEntries(formData.entries())
        );

        // Тут пізніше:
        // updateAlbum({ id: album.id, formData });
    };

    return (
        <div className="min-h-screen bg-gray-950 px-6 py-10 text-gray-100">
            <div className="mx-auto max-w-6xl">

                {/* HEADER */}
                <div className="mb-10">
                    <h1 className="text-3xl font-bold">
                        Edit album
                    </h1>

                    <p className="mt-2 text-gray-400">
                        Update your album information
                    </p>
                </div>

                <form
                    onSubmit={handleSubmit(onSubmit)}
                    className="grid grid-cols-1 gap-12 lg:grid-cols-[380px_1fr]"
                >

                    {/* IMAGE */}
                    <div>
                        <label className="mb-3 block text-sm font-semibold">
                            Album artwork
                        </label>

                        <div className="relative aspect-square overflow-hidden rounded-xl">
                            {image ? (
                                <img
                                    src={URL.createObjectURL(image)}
                                    alt="Album artwork"
                                    className="h-full w-full object-cover"
                                />
                            ) : album.image ? (
                                <img
                                    src={album.image}
                                    alt={album.name}
                                    className="h-full w-full object-cover"
                                />
                            ) : (
                                <div className="flex h-full w-full flex-col items-center justify-center border border-dashed border-gray-600 bg-gray-900">
                                    <span className="text-5xl text-cyan-400">
                                        🖼
                                    </span>

                                    <span className="mt-4 text-gray-400">
                                        No artwork
                                    </span>
                                </div>
                            )}

                            <label
                                htmlFor="update-album-image"
                                className="
                                    absolute
                                    bottom-4
                                    left-1/2
                                    -translate-x-1/2
                                    cursor-pointer
                                    rounded-lg
                                    bg-gray-950/90
                                    px-5
                                    py-2
                                    text-sm
                                    font-semibold
                                    transition
                                    hover:bg-cyan-400
                                    hover:text-gray-950
                                "
                            >
                                Change artwork
                            </label>

                            <input
                                id="update-album-image"
                                type="file"
                                accept="image/*"
                                className="hidden"
                                onChange={(event) => {
                                    const file =
                                        event.target.files?.[0];

                                    if (file) {
                                        setValue("image", file, {
                                            shouldValidate: true,
                                        });
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

                    {/* FORM */}
                    <div className="flex flex-col gap-7">

                        {/* NAME */}
                        <div>
                            <label
                                htmlFor="name"
                                className="mb-2 block text-sm font-semibold"
                            >
                                Album name *
                            </label>

                            <input
                                id="name"
                                {...register("name")}
                                className="
                                    w-full
                                    border-b
                                    border-gray-700
                                    bg-transparent
                                    px-0
                                    py-3
                                    outline-none
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
                                Author *
                            </label>

                            <select
                                id="authorId"
                                {...register("authorId", {
                                    valueAsNumber: true,
                                })}
                                className="
                                    w-full
                                    rounded-lg
                                    border
                                    border-gray-700
                                    bg-gray-900
                                    px-4
                                    py-3
                                    outline-none
                                    focus:border-cyan-400
                                "
                            >
                                <option value={0}>
                                    Select author
                                </option>

                                <option value={1}>
                                    Mark
                                </option>

                                <option value={2}>
                                    Example Artist
                                </option>
                            </select>

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
                                {...register("description")}
                                rows={6}
                                className="
                                    w-full
                                    resize-none
                                    rounded-lg
                                    border
                                    border-gray-700
                                    bg-gray-900
                                    px-4
                                    py-3
                                    outline-none
                                    focus:border-cyan-400
                                "
                            />

                            {errors.description && (
                                <p className="mt-2 text-sm text-red-400">
                                    {errors.description.message}
                                </p>
                            )}
                        </div>

                        {/* SUBMIT */}
                        <div className="flex justify-end border-t border-gray-800 pt-6">
                            <button
                                type="submit"
                                className="
                                    rounded-lg
                                    bg-cyan-400
                                    px-8
                                    py-3
                                    font-semibold
                                    text-gray-950
                                    transition
                                    hover:bg-cyan-300
                                "
                            >
                                Save changes
                            </button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default AlbumUpdateForm;