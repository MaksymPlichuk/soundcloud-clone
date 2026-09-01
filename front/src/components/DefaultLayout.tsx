import {Outlet} from "react-router";
import Navbar from "./Navbar.tsx";
import Footer from "./Footer.tsx";
import Player from "./Player.tsx";

const DefaultLayout = () => {
    return (
        <>
            <Navbar/>
            <div className='main justify content-center align-items-center h-full mt-10 pb-24'>
                <Outlet></Outlet>
            </div>
            <Footer/>
            <Player />
        </>
    );
}
export default DefaultLayout;