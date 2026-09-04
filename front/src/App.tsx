import MainPage from "./pages/MainPage.tsx";
import { Route, Routes } from "react-router-dom";
import DefaultLayout from "./components/DefaultLayout.tsx";
import ErrorPage from "./pages/errorPage/ErrorPage.tsx";

import AlbumList from "./pages/albumPages/AlbumList.tsx";
import AlbumCreateForm from "./pages/albumPages/AlbumCreateForm.tsx";
import AlbumUpdateForm from "./pages/albumPages/AlbumUpdateForm.tsx";
import AlbumPage from "./pages/albumPages/AlbumPage.tsx";
import LoginPage from "./pages/auth/LoginPage.tsx";
import RegisterPage from "./pages/auth/RegisterPage.tsx";
import SongList from "./pages/songPages/SongList.tsx";
import SongPage from "./pages/songPages/SongPage.tsx";
import SongCreateForm from "./pages/songPages/SongCreateForm.tsx";

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
                <Route path="/song">
                    <Route index element={<SongList/>}/>
                    <Route path="create" element={<SongCreateForm/>}/>
                    {/*<Route path=":id/edit" element={<AlbumUpdateForm/>}/>*/}
                    <Route path=":id" element={<SongPage />}/>
                </Route>
                <Route path="/login" element={<LoginPage/>}/>
                <Route path="/register" element={<RegisterPage/>}/>

                <Route path="*" element={<ErrorPage />}/>
            </Route>
        </Routes>
    );
}

export default App;