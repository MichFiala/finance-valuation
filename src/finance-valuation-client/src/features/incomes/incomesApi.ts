import { IncomesResponseDto } from "./incomeModel";

export async function fetchIncomes() {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/incomes`);
  if (!response.ok) {
    throw new Error('Failed to fetch incomes');
  }
  return response.json() as Promise<IncomesResponseDto>;
}