import { SavingsResponseDto } from "./savingsModel";

export async function fetchSavings() {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/savings`);
  if (!response.ok) {
    throw new Error('Failed to fetch savings');
  }
  return response.json() as Promise<SavingsResponseDto>;
}

export async function updateSaving(id: number, name: string, amount: number) {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/savings/${id}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ name, amount }),
  });
  
  if (!response.ok) {
    throw new Error('Failed to update saving');
  }
}
