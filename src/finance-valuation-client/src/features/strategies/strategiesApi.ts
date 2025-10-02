import { CalculatedStrategyResponseDto } from "./strategyCalculatedModel";
import { StrategyDto } from "./strategyModel";

export const StrategiesEndpoint = "strategies";

export async function fetchStrategy(id: number) {
  console.log('API URL:', import.meta.env.VITE_APP_API_URL);
  const apiUrl =  import.meta.env.VITE_APP_API_URL || '';
  const response = await fetch(`${apiUrl}/${StrategiesEndpoint}/${id}`,
    {
    method: "GET",
    credentials: "include",
  });
  if (!response.ok) {
    throw new Error('Failed to fetch strategy');
  }
  return response.json() as Promise<StrategyDto>;
}

export async function fetchCalculatedStrategy(strategyId: number, incomeId: number) {
  const apiUrl =  import.meta.env.VITE_APP_API_URL || '';
  const response = await fetch(`${apiUrl}/${StrategiesEndpoint}/${strategyId}/calculate?incomeId=${incomeId}`,{
    method: "GET",
    credentials: "include",
  });
  if (!response.ok) {
    throw new Error('Failed to fetch strategy');
  }
  return response.json() as Promise<CalculatedStrategyResponseDto>;
}