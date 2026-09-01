import MainPage from "./pages/MainPage.tsx";
import {Route, Routes} from "react-router";
import DefaultLayout from "./components/DefaultLayout.tsx";
import ErrorPage from "./pages/ErrorPage/ErrorPage.tsx";

function App() {

  return (
    <>
      <Routes>
        <Route path="/" element={<DefaultLayout/>}>
          <Route index element={<MainPage/>}/>

            <Route path={"*"} element={<ErrorPage/>}></Route>
        </Route>
      </Routes>
    </>
  )
}

export default App
