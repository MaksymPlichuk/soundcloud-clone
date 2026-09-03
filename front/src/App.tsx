import MainPage from "./pages/MainPage.tsx";
import { Route, Routes } from "react-router-dom";
import DefaultLayout from "./components/DefaultLayout.tsx";
import ErrorPage from "./pages/errorPage/ErrorPage.tsx";

import AlbumList from "./pages/albumPages/AlbumList.tsx";
import AlbumCreateForm from "./pages/albumPages/AlbumCreateForm.tsx";
import AlbumUpdateForm from "./pages/albumPages/AlbumUpdateForm.tsx";
import AlbumPage from "./pages/albumPages/AlbumPage.tsx";
import LoginPage from "./pages/auth/LoginPage.tsx";

function App() {
    return (
        <Routes>
            <Route path="/" element={<DefaultLayout />}>
                <Route index element={<MainPage />} />

                <Route path="/album">
                    <Route index element={<AlbumList/>}/>
                    <Route path="create" element={<AlbumCreateForm/>}/>
                    {/*<Route path=":id/edit" element={<AlbumUpdateForm/>}/>*/}
                    <Route path=":id" element={<AlbumPage />}/>
                </Route>
                <Route path="/login" element={<LoginPage/>}/>
                {/*<Route path="/register" element={<RegisterPage/>}/>*/}

                <Route path="*" element={<ErrorPage />}/>
            </Route>
        </Routes>
    );
}

export default App;