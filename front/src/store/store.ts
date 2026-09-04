import {create} from "zustand"
import type {ISongItem} from "../types/Song/ISongItem.ts";

type PlayerStore = {
    currentTrack: string | "Dr%20Dre,%20Snoop%20Dogg%20%E2%80%93%20Still%20DRE.mp3";
    isPlaying: boolean;
    volume: number;
    isLiked: boolean;

    currentTime: number,
    duration: number,

    play: (song: string | "Dr%20Dre,%20Snoop%20Dogg%20%E2%80%93%20Still%20DRE.mp3") => void;
    pause: () => void;
    togglePlay: () => void;
    //next: () => void; в майбутньому
    //previous: () => void;
    setVolume: (volume: number) => void;

    setCurrentTime: (currentTime: number) => void;
    setDuration: (duration: number) => void;
}

export const usePlayerStore = create<PlayerStore>((set) => ({
    currentTrack: "Dr%20Dre,%20Snoop%20Dogg%20%E2%80%93%20Still%20DRE.mp3",
    isPlaying: false,
    volume: 100,
    isLiked: false,

    currentTime: 0,
    duration: 0,

    setVolume: (volume: number) => {
        set(() => ({volume}));
    },
    play: (song) => set(() => ({currentTrack: song, isPlaying: true})),
    pause: () => set(() => ({isPlaying: false})),
    togglePlay: () => set((state) => ({isPlaying: !state.isPlaying})),

    setCurrentTime: (time) => set(() => ({currentTime: time})),
    setDuration: (duration) => set(() => ({duration})),
}));