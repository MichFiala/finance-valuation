import { User } from "./userModel";

export async function fetchMe() {
  const apiUrl =  import.meta.env.VITE_APP_API_URL || '';
  const returnUrl = import.meta.env.VITE_APP_RETURN_URL;
  const response = await fetch(`${apiUrl}/users/me`, {
    method: "GET",
    credentials: "include",
    cache: "no-store", // nebo "reload"
  });
  if(response.redirected){
    console.log("Redirect");
    window.location.href = `${apiUrl}/login/google?returnUrl=${returnUrl}`;
  }
    
  if (!response.ok) {
    throw new Error('Failed to fetch me');
  }
  return response.json() as Promise<User>;
}