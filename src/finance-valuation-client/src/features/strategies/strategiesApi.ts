import { CalculatedStrategyResponseDto } from "./strategyCalculatedModel";
import { StrategyDto } from "./strategyModel";

export async function fetchStrategy() {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/strategies/1`);
  if (!response.ok) {
    throw new Error('Failed to fetch strategy');
  }
  return response.json() as Promise<StrategyDto>;
}

export async function fetchCalculatedStrategy(incomeId: number) {
  const apiUrl =  process.env.REACT_APP_API_URL || 'http://localhost:5153';
  const response = await fetch(`${apiUrl}/strategies/1/calculate?incomeId=${incomeId}`);
  if (!response.ok) {
    throw new Error('Failed to fetch strategy');
  }
  return response.json() as Promise<CalculatedStrategyResponseDto>;
}