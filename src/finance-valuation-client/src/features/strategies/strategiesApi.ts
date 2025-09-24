import { CalculatedStrategyResponseDto } from "./strategyCalculatedModel";
import { StrategyDto } from "./strategyModel";

export async function fetchStrategy() {
  const apiUrl =  process.env.REACT_APP_API_URL || '';
  const response = await fetch(`${apiUrl}/strategies/1`,
    {
    method: "GET",
    credentials: "include",
  });
  if (!response.ok) {
    throw new Error('Failed to fetch strategy');
  }
  return response.json() as Promise<StrategyDto>;
}

export async function fetchCalculatedStrategy(incomeId: number) {
  const apiUrl =  process.env.REACT_APP_API_URL || '';
  const response = await fetch(`${apiUrl}/strategies/1/calculate?incomeId=${incomeId}`,{
    method: "GET",
    credentials: "include",
  });
  if (!response.ok) {
    throw new Error('Failed to fetch strategy');
  }
  return response.json() as Promise<CalculatedStrategyResponseDto>;
}