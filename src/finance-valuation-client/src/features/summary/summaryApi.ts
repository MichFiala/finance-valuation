export async function fetchSummary() {
  const apiUrl =  import.meta.env.VITE_APP_API_URL || '';
  const response = await fetch(`${apiUrl}/summary`,{
    method: "GET",
    credentials: "include",
  });
  if (!response.ok) {
    throw new Error('Failed to fetch summary');
  }
  return response.json() as Promise<SummaryResponseDto>;
}

export interface SummaryResponseDto {
  totalInvestments: number;
  totalSavings: number;
  totalDebts: number;
  totalMonthlySpendings: number;
};