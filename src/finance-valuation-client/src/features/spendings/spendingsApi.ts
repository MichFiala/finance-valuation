import { SpendingsFrequency, SpendingsResponseDto } from "./spendingsModel";

export async function fetchSpendings() {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/spendings`);
  if (!response.ok) {
    throw new Error('Failed to fetch spendings');
  }
  return response.json() as Promise<SpendingsResponseDto>;
}

export async function createSpending(name: string, amount: number, frequency: SpendingsFrequency,  isMandatory: boolean) {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/spendings`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ name, amount, frequency, isMandatory }),
  });
  
  if (!response.ok) {
    throw new Error('Failed to update spending');
  }
}

export async function updateSpending(id: number, name: string, amount: number, frequency: SpendingsFrequency, isMandatory: boolean) {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/spendings/${id}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ name, amount, frequency, isMandatory }),
  });
  
  if (!response.ok) {
    throw new Error('Failed to update spending');
  }
}