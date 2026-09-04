import { useState } from "react";
import {useRegisterMutation} from "../../api/authApi.ts";


const RegisterPage = () => {
    const [register] = useRegisterMutation();

    const [form, setForm] = useState({ email: "", password: "", userName: "" });

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            console.log(form)
            const response = await register(form).unwrap();
            console.log(response);
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="flex flex-col gap-4 max-w-sm mx-auto mt-10">
            <input
                type="username"
                placeholder="Username"
                value={form.userName}
                onChange={e => setForm({ ...form, userName: e.target.value })}
                className="border p-2 rounded"
                required
            />
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
                Register
            </button>
        </form>
    );
};

export default RegisterPage;