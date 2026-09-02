import {create} from "zustand"
import type {ISongItem} from "../types/Song/ISongItem.ts";

type PlayerStore = {
    currentTrack: ISongItem | null;
    isPlaying: boolean;
    volume: number;
    isLiked: boolean;

    currentTime: number,
    duration: number,

    play: (song: ISongItem | null) => void;
    pause: () => void;
    togglePlay: () => void;
    //next: () => void; в майбутньому
    //previous: () => void;
    setVolume: (volume: number) => void;

    setCurrentTime: (currentTime: number) => void;
    setDuration: (duration: number) => void;
}

export const usePlayerStore = create<PlayerStore>((set) => ({
    currentTrack: null,
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