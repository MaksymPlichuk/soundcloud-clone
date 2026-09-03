export interface AuthResponse {
  message: string;
  payload: {
    token: string;
    user: { id: number; userName: string; image: string | null };
  };
}