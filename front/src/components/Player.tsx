import { useEffect, useRef } from "react";
import { usePlayerStore } from "../store/store.ts";
import APP_ENV from "../env";

const formatTime = (time: number | undefined) => {
    if (!time || isNaN(time)) return "0:00";
    const minutes = Math.floor(time / 60);
    const seconds = Math.floor(time % 60);
    return `${minutes}:${seconds.toString().padStart(2, "0")}`;
};

const PauseIcon = () => (
    <svg className="h-6 w-6 fill-gray-950" viewBox="0 0 24 24">
        <path d="M6 19h4V5H6v14zm8-14v14h4V5h-4z" />
    </svg>
);

const PlayIcon = () => (
    <svg className="h-6 w-6 fill-gray-950 pl-1" viewBox="0 0 24 24">
        <path d="M8 5v14l11-7z" />
    </svg>
);

const PlayButton = () => {
    const { isPlaying, togglePlay } = usePlayerStore();

    return (
        <button
            onClick={togglePlay}
            // disabled={!currentTrack} // вимкнув для презентації
            className="flex h-12 w-12 shrink-0 items-center justify-center rounded-full bg-cyan-400 transition hover:scale-105 hover:bg-cyan-300"
        >
            {isPlaying ? <PauseIcon /> : <PlayIcon />}
        </button>
    );
};

type ProgressBarProps = {
    audioRef: React.RefObject<HTMLAudioElement | null>;
};

const ProgressBar = ({ audioRef }: ProgressBarProps) => {
    const { currentTime, setCurrentTime, duration } = usePlayerStore();

    const changeBar = (e: React.ChangeEvent<HTMLInputElement>) => {
        const newTime = Number(e.target.value);
        if (audioRef.current) {
            audioRef.current.currentTime = newTime;
        }
        setCurrentTime(newTime);
    };

    return (
        <div className="flex w-full items-center gap-4">
            <span className="w-10 text-right text-xs font-medium text-gray-400">
                {formatTime(currentTime)}
            </span>
            <input
                type="range"
                min="0"
                max={duration || 0}
                value={currentTime || 0}
                onChange={changeBar}
                className="h-1.5 w-full cursor-pointer appearance-none rounded-full bg-gray-800 accent-cyan-400 transition-all hover:h-2"
            />
            <span className="w-10 text-xs font-medium text-gray-400">
                {formatTime(duration)}
            </span>
        </div>
    );
};

const Player = () => {
    const audioRef = useRef<HTMLAudioElement | null>(null);
    const { currentTrack, isPlaying, setCurrentTime, setDuration } = usePlayerStore();

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
        <div className="fixed bottom-0 left-0 right-0 z-50 border-t border-gray-800 bg-gray-950/95 px-6 py-4 backdrop-blur-md">
            <audio
                ref={audioRef}
                src={`${APP_ENV.API_URL}/songs/${currentTrack}`}
                onTimeUpdate={() => setCurrentTime(audioRef.current?.currentTime ?? 0)}
                onLoadedMetadata={() => setDuration(audioRef.current?.duration ?? 0)}
            />

            <div className="mx-auto flex max-w-6xl items-center justify-between gap-6">
                {/* Блок з інфо про пісню (можеш додати сюди обкладинку та назву пізніше) */}
                <div className="hidden w-1/4 sm:block">
                    {/* Placeholder для UI */}
                </div>

                <div className="flex w-full max-w-2xl flex-col items-center gap-3 sm:w-2/4">
                    <PlayButton />
                    <ProgressBar audioRef={audioRef} />
                </div>

                {/* Блок для додаткових кнопок (гучність тощо) */}
                <div className="hidden w-1/4 justify-end sm:flex">
                    {/* Placeholder для UI */}
                </div>
            </div>
        </div>
    );
};

export default Player;