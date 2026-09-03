import { useState } from "react";
import { useLoginMutation } from "../../api/authApi.ts";
import { useAppDispatch } from "../../store";
import authSlice, {setCredentials} from "../../utils/authSlice.ts";

const LoginPage = () => {
    const [login] = useLoginMutation();
    const dispatch = useAppDispatch();

    const [form, setForm] = useState({ email: "", password: "" });

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            console.log(form)
            const response = await login(form).unwrap();
            console.log(response);
            dispatch(setCredentials(response.payload));
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="flex flex-col gap-4 max-w-sm mx-auto mt-10">
            <input
                type="email"
                placeholder="Email"
                value={form.email}
                onChange={e => setForm({ ...form, email: e.target.value })}
                className="border p-2 rounded"
                required
            />
            <input
                type="password"
                placeholder="Password"
                value={form.password}
                onChange={e => setForm({ ...form, password: e.target.value })}
                className="border p-2 rounded"
                required
            />
            <button type="submit" className="bg-blue-500 text-white p-2 rounded">
                Увійти
            </button>
        </form>
    );
};

export default LoginPage;