import MainPage from "./pages/MainPage.tsx";
import {Route, Routes} from "react-router";
import DefaultLayout from "./components/DefaultLayout.tsx";
import ErrorPage from "./pages/ErrorPage/ErrorPage.tsx";
import AlbumList from "./pages/albumPages/AlbumList.tsx";
import AlbumCreateForm from "./pages/albumPages/AlbumCreateForm.tsx";
import AlbumUpdateForm from "./pages/albumPages/AlbumUpdateForm.tsx";
import AlbumPage from "./pages/albumPages/AlbumPage.tsx";

function App() {

  return (
    <>
      <Routes>
        <Route path="/" element={<DefaultLayout/>}>
          <Route index element={<MainPage/>}/>

            <Route path={"/album"}>
                <Route index element={<AlbumList/>}/>
                <Route path='create' element={<AlbumCreateForm/>}/>
                <Route path='update/:id' element={<AlbumUpdateForm/>}/>
                <Route path='description/:id' element={<AlbumPage/>}/>
            </Route>

            <Route path={"*"} element={<ErrorPage/>}></Route>
        </Route>
      </Routes>
    </>
  )
}

export default App
