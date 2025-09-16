import { DebtsResponseDto } from "./debtsResponseModel";

export async function fetchDebts() {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/debts`);
  if (!response.ok) {
    throw new Error('Failed to fetch debts');
  }
  return response.json() as Promise<DebtsResponseDto>;
}

export async function updateDebt(id: number, name: string, amount: number) {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/debts/${id}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ name, amount }),
  });
  
  if (!response.ok) {
    throw new Error('Failed to update debt');
  }
}
