import { InvestmentsResponseDto } from "./investmentModel";

export async function fetchInvestments() {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/investments`);
  if (!response.ok) {
    throw new Error('Failed to fetch investments');
  }
  return response.json() as Promise<InvestmentsResponseDto>;
}

export async function updateInvestment(id: number, name: string, amount: number) {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/investments/${id}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ name, amount }),
  });
  
  if (!response.ok) {
    throw new Error('Failed to update investment');
  }
}
