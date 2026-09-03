import { type Control, Controller, type FieldPath, type FieldValues } from "react-hook-form";

type BaseProps<T extends FieldValues> = {
    control: Control<T>;
    name: FieldPath<T>;
    label: React.ReactNode;
};

type FormInputProps<T extends FieldValues> = BaseProps<T> & {
    placeholder?: string;
    type?: React.HTMLInputTypeAttribute;
};

export function FormInput<T extends FieldValues>({ control, name, label, placeholder, type = "text" }: FormInputProps<T>) {
    return (
        <Controller
            name={name}
            control={control}
            render={({ field, fieldState: { error } }) => (
                <div>
                    <label htmlFor={name} className="mb-2 block text-sm font-semibold text-gray-200">{label}</label>
                    <input
                        {...field}
                        id={name}
                        type={type}
                        placeholder={placeholder}
                        className={`w-full rounded-xl border bg-gray-900 px-4 py-3.5 text-gray-100 outline-none transition focus:border-cyan-400 ${
                            error ? "border-red-400" : "border-gray-800"
                        }`}
                    />
                    {error && <p className="mt-2 text-sm text-red-400">{error.message}</p>}
                </div>
            )}
        />
    );
}

type FormTextareaProps<T extends FieldValues> = BaseProps<T> & {
    placeholder?: string;
    rows?: number;
};

export function FormTextarea<T extends FieldValues>({ control, name, label, placeholder, rows = 6 }: FormTextareaProps<T>) {
    return (
        <Controller
            name={name}
            control={control}
            render={({ field, fieldState: { error } }) => (
                <div>
                    <label htmlFor={name} className="mb-2 block text-sm font-semibold text-gray-200">{label}</label>
                    <textarea
                        {...field}
                        id={name}
                        rows={rows}
                        placeholder={placeholder}
                        className={`w-full resize-none rounded-xl border bg-gray-900 px-4 py-3.5 text-gray-100 outline-none transition focus:border-cyan-400 ${
                            error ? "border-red-400" : "border-gray-800"
                        }`}
                    />
                    {error && <p className="mt-2 text-sm text-red-400">{error.message}</p>}
                </div>
            )}
        />
    );
}

type FormSelectProps<T extends FieldValues> = BaseProps<T> & {
    options: { value: string | number; label: string }[];
    disabled?: boolean;
    placeholder?: string;
};

export function FormSelect<T extends FieldValues>({ control, name, label, options, disabled, placeholder }: FormSelectProps<T>) {
    return (
        <Controller
            name={name}
            control={control}
            render={({ field, fieldState: { error } }) => (
                <div>
                    <label htmlFor={name} className="mb-2 block text-sm font-semibold text-gray-200">{label}</label>
                    <select
                        {...field}
                        id={name}
                        disabled={disabled}
                        onChange={(e) => {
                            const val = e.target.value;
                            // Конвертуємо в число, якщо значення числове (потрібно для Zod authorId)
                            field.onChange(!isNaN(Number(val)) ? Number(val) : val);
                        }}
                        className={`w-full rounded-xl border bg-gray-900 px-4 py-3.5 text-gray-100 outline-none transition focus:border-cyan-400 disabled:opacity-50 ${
                            error ? "border-red-400" : "border-gray-800"
                        }`}
                    >
                        <option value={0}>{placeholder}</option>
                        {options.map((opt) => (
                            <option key={opt.value} value={opt.value}>{opt.label}</option>
                        ))}
                    </select>
                    {error && <p className="mt-2 text-sm text-red-400">{error.message}</p>}
                </div>
            )}
        />
    );
}

export function ImageFormInput<T extends FieldValues>({ control, name, label }: BaseProps<T>) {
    return (
        <Controller
            name={name}
            control={control}
            render={({ field: { onChange, value }, fieldState: { error } }) => (
                <div>
                    <label className="mb-3 block text-sm font-semibold text-gray-200">{label}</label>
                    <div className={`relative aspect-square overflow-hidden rounded-2xl border bg-gray-900 shadow-xl ${error ? "border-red-400" : "border-gray-800"}`}>
                        {value ? (
                            <img src={URL.createObjectURL(value as File)} alt="Album artwork preview" className="h-full w-full object-cover" />
                        ) : (
                            <div className="flex h-full w-full flex-col items-center justify-center bg-gradient-to-br from-gray-900 via-gray-950 to-cyan-950/40">
                                <span className="text-8xl text-cyan-400">♪</span>
                                <span className="mt-5 text-sm text-gray-500">No artwork selected</span>
                            </div>
                        )}
                        <div className="absolute inset-x-0 bottom-0 bg-gradient-to-t from-black/80 to-transparent p-5 pt-16">
                            <label htmlFor={name as string} className="block cursor-pointer rounded-lg bg-cyan-400 px-5 py-3 text-center text-sm font-bold text-gray-950 transition hover:bg-cyan-300">
                                {value ? "Change artwork" : "Choose artwork"}
                            </label>
                        </div>
                        <input
                            id={name as string}
                            type="file"
                            accept="image/*"
                            className="hidden"
                            onChange={(e) => {
                                const file = e.target.files?.[0];
                                if (file) onChange(file);
                            }}
                        />
                    </div>
                    {error && <p className="mt-2 text-sm text-red-400">{error.message}</p>}
                </div>
            )}
        />
    );
}