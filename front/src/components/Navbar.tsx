import { Link } from "react-router-dom";

const Navbar = () => {
    return (
        <>
            <nav className="flex items-center justify-between bg-gray-950 px-6 max-h-20 border-b border-gray-800 text-gray-100">
                <Link to="/">
                    <div className="my-3">
                        <img
                            className="h-12 w-12 rounded-full object-cover border border-cyan-500/30"
                            src={"placeholder"}
                            alt="Logo"
                        />
                    </div>
                </Link>

                <div className="hidden md:flex items-center space-x-6 text-sm font-medium">
                    <Link to="liked" className="hover:text-cyan-400 transition-colors">
                        Library
                    </Link>
                    <Link to="album" className="hover:text-cyan-400 transition-colors">
                        Albums
                    </Link>
                    <Link to="song" className="hover:text-cyan-400 transition-colors">
                        Songs
                    </Link>
                    <Link to="song/create" className="hover:text-cyan-400 transition-colors">
                        Upload
                    </Link>
                </div>

                <div className="flex items-center space-x-4 w-1/3">
                    <input
                        placeholder={"Search..."}
                        className="
                            w-full px-4 py-2 rounded-xl text-sm
                            bg-gray-900 text-gray-100
                            border border-gray-800
                            placeholder-gray-500
                            focus:outline-none focus:border-cyan-500
                            transition-all duration-200
                        "
                    />
                </div>

                <div className="flex items-center space-x-3">
                    <Link to="placeholder" className="flex items-center justify-center">
                        <img
                            src={"https://i.pinimg.com/236x/15/0f/a8/150fa8800b0a0d5633abc1d1c4db3d87.jpg?nii=t"}
                            alt={"Profile"}
                            className="rounded-full ring-1 ring-cyan-500/50 w-10 h-10 object-cover"
                        />
                    </Link>
                    <Link
                        to="placeholder"
                        className="px-4 py-2 text-sm rounded-xl border border-gray-700 hover:border-cyan-500 transition-colors"
                    >
                        Login
                    </Link>
                    <Link
                        to="placeholder"
                        className="px-4 py-2 text-sm rounded-xl bg-cyan-500 text-gray-950 font-medium hover:bg-cyan-400 transition-colors"
                    >
                        Register
                    </Link>
                </div>
            </nav>
        </>
    );
};

export default Navbar;