import {useEffect, useRef} from "react";
import {usePlayerStore} from "../store/store.ts";
import APP_ENV from "../env";

const PauseIcon = () => {
    return (
        <div className={"text-center text-white font-bold"}>❚❚</div>
    );
}
const PlayIcon = () => {
    return (
        <div className={"text-center text-white font-bold"}>▶</div>
    );
}

const PlayButton = () => {
    const {currentTrack, isPlaying, togglePlay} = usePlayerStore();

    return (
        <button
            onClick={togglePlay}
            //disabled={!currentTrack} вимкнув для презентації
            className="w-10 h-10 flex items-center justify-center rounded-full bg-black"
        >
            {isPlaying ? <PauseIcon /> : <PlayIcon />}
        </button>
    )
}

type ProgressBarProps = {
    audioRef: React.RefObject<HTMLAudioElement | null>;
};

const ProgressBar = ({ audioRef }: ProgressBarProps) => {
    const {currentTime, setCurrentTime, duration} = usePlayerStore();

    const changeBar = (e : React.ChangeEvent<HTMLInputElement>) => {
        const newTime = Number(e.target.value);
        if (audioRef.current) {
            audioRef.current.currentTime = newTime;
        }
        setCurrentTime(newTime);
    }
    return (
            <input
            type="range"
            min="0"
            max={duration ?? 0}
            value={currentTime}
            onChange={changeBar}
            className="w-full bg-emerald-800"/>
    );

}

const Player = () => {
    const audioRef = useRef<HTMLAudioElement | null>(null);
    const {currentTrack, isPlaying, setCurrentTime, setDuration} = usePlayerStore();

    useEffect(() => {
        if (!audioRef.current) return;
        if (isPlaying) {
            audioRef.current.play();
        } else {
            audioRef.current.pause();
        }
    }, [isPlaying]);

    useEffect(() => {
        if (!audioRef.current) return;
        audioRef.current.load();
        if (isPlaying) {
            audioRef.current.play();
        }
    }, [currentTrack]);

    return (
        <div className={"fixed bottom-0 w-full justify-center z-50 px-4"}>
            <audio
                ref={audioRef}
                src={`${APP_ENV.API_URL}/songs/${currentTrack}`}
                onTimeUpdate={() => setCurrentTime(audioRef.current?.currentTime ?? 0)}
                onLoadedMetadata={() => setDuration(audioRef.current?.duration ?? 0)}
            />
            <div className={"flex flex-row gap-5 bg-cyan-500 px-4 rounded-lg"}>
                <PlayButton/>
                <ProgressBar audioRef={audioRef} />
            </div>
        </div>
    );
}
export default Player;