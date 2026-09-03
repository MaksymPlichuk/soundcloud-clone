import {useLoginMutation} from "../../api/authApi.ts";
import {useAppDispatch} from "../../store";

const LoginPage = () => {
    const [login] = useLoginMutation();
    const dispatch = useAppDispatch();

    const loginFunc = () => {
        const handleSubmit = async (data: LoginDto) => {
            try {
                const response = await login(data).unwrap();
                dispatch(setCredentials(response.payload));
            } catch (err) {
                console.error(err);
            }
    }

    return (
        <>
        </>
    );
}
export default LoginPage;