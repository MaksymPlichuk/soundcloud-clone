import {useEffect, useRef} from "react";
import {usePlayerStore} from "../store/store.ts";
import APP_ENV from "../env";

const PlayButton = () => {
    const audioRef = useRef<HTMLAudioElement | null>(null);
    const {pause, isPlaying, togglePlay} = usePlayerStore();
//todo
    if (isPlaying == true) {
        pause();
    }
    else {togglePlay()}

    return (
        <div
            onClick={togglePlay}
            //disabled={!currentTrack}
            className="w-10 h-10 flex items-center justify-center rounded-full bg-black"
        >
            {/*{isPlaying ? <Pause size={18} /> : <Play size={18} fill="black" />}*/}
        </div>
    )}

const Player = () => {
    const audioRef = useRef<HTMLAudioElement | null>(null);
    const {currentTrack, isPlaying, togglePlay} = usePlayerStore();

    useEffect(() => {
        if (!audioRef.current) return;
        if (isPlaying) { audioRef.current.play();}
    }, [])
//todo
    return (
        <>
                                                                    {/*ref={audioRef} для кастомного*/}
            <audio className={"fixed bottom-0 w-full justify-center z-50 px-4"} controls>
                <source src={`${APP_ENV.API_URL}/songs/Dr%20Dre,%20Snoop%20Dogg%20%E2%80%93%20Still%20DRE.mp3`} type={"audio/mpeg"}></source>
            </audio>
                <PlayButton />
        </>
    );
}
export default Player;