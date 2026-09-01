import {Outlet} from "react-router";
import Navbar from "./Navbar.tsx";
import Footer from "./Footer.tsx";

const DefaultLayout = () => {
    return (
        <>
            <Navbar/>
            <div className='main justify content-center align-items-center h-full mt-10 pb-24'>
                <Outlet></Outlet>
            </div>
            <Footer/>
            <audio className={"fixed bottom-0 w-full justify-center z-50 px-4"} controls>
                <source src={"https://localhost:7293/songs/Dr%20Dre,%20Snoop%20Dogg%20%E2%80%93%20Still%20DRE.mp3"} type={"audio/mpeg"}></source>
            </audio>
        </>
    );
}
export default DefaultLayout;