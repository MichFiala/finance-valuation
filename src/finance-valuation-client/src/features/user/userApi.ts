import { User } from "./userModel";

export async function fetchMe() {
  const apiUrl =  process.env.REACT_APP_API_URL || '';
  const returnUrl = process.env.REACT_APP_RETURN_URL;
  const response = await fetch(`${apiUrl}/users/me`, {
    method: "GET",
    credentials: "include",
  });
  if(response.redirected){
    window.location.href = `${apiUrl}/login/google?returnUrl=${returnUrl}`;

    return null;
  }
    
  if (!response.ok) {
    throw new Error('Failed to fetch me');
  }
  return response.json() as Promise<User>;
}