import {jwtDecode} from "jwt-decode";
import type {UserForInfo} from "../types/User/UserForInfo.ts";

interface JwtPayload {
    exp: number;
    id: number;
    userName: string;
    image?: string | null;
}

export function isTokenExpired(token: string | null): boolean {
    if (!token) return true;
    try {
        const decoded = jwtDecode<JwtPayload>(token);
        console.log(decoded.exp * 1000, Date.now());
        return decoded.exp * 1000 < Date.now();
    } catch {
        return true;
    }
}

export function jwtGetUser(token: string) {
    if (!token) return null;
    try {
        const decoded = jwtDecode<JwtPayload>(token);
        console.log(decoded);
        return {id: decoded.id, userName: decoded.userName, image: decoded.image} as UserForInfo;
    } catch {
        return null;
    }
}