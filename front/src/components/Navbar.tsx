import {Link} from "react-router-dom";

const Navbar = () => {
    return (
        <>
            <nav className="flex items-center bg-white max-h-20">
        <Link to="/">
        <div className="grow-0.5 mb-5 mt-5">
        <img className="h-20 w-20 rounded-full "
    src={"https://thumbs.dreamstime.com/b/lets-shopping-logo-design-template-shop-icon-135610500.jpg"}/>
    </div>
    </Link>
    //todo icon
    <div className="grow-1 justify-between space-x-2 flex">
        <div>
            </div>
        <input
    placeholder={"Never gonna give you up"}
    className="
    w-full px-4 py-2.5 rounded-xl text-sm mx-5
    bg-slate-150 text-slate-900
    border border-gray-200
    focus:ring-indigo-400/20
    transition-all duration-200
    "/>

    <div className="flex items-center justify-center w-full ml-10">
    <img src={"https://i.pinimg.com/236x/15/0f/a8/150fa8800b0a0d5633abc1d1c4db3d87.jpg?nii=t"}
    alt={"no pfp"}
    className="rounded-full ring ring-red-300 w-15 h-15"/>
    <button className="container border border-red-400 m-5 rounded-2xl">Login</button>
        <button className="container border border-red-400 bg-emerald-300 m-5 rounded-2xl">Register
        </button>
        </div>

        </div>
        </nav>
        </>
)
}
export default Navbar;