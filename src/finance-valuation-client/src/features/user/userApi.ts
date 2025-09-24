import { User } from "./userModel";

export async function fetchMe() {
  const apiUrl =  process.env.REACT_APP_API_URL || '';
  const response = await fetch(`${apiUrl}/users/me`, {
    method: "GET",
    credentials: "include",
  });
  if(response.redirected){
    window.location.href =
      "https://localhost:7089/login/google?returnUrl=http://localhost:3000";

    return null;
  }
    
  if (!response.ok) {
    throw new Error('Failed to fetch me');
  }
  return response.json() as Promise<User>;
}