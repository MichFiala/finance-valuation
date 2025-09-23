export async function fetchSummary() {
  const apiUrl =  process.env.REACT_APP_API_URL || 'https://localhost:7089';
  const response = await fetch(`${apiUrl}/summary`,{
    method: "GET",
    credentials: "include",
  });
  if (!response.ok) {
    throw new Error('Failed to fetch summary');
  }
  return response.json() as Promise<SummaryResponseDto>;
}

export async function fetchSavingsLongevity() {
  const apiUrl =  process.env.REACT_APP_API_URL || 'https://localhost:7089';
  const response = await fetch(`${apiUrl}/savings-longevity`,{
    method: "GET",
    credentials: "include",
  });
  if (!response.ok) {
    throw new Error('Failed to fetch savings longevity');
  }
  return response.json() as Promise<SavingsLongevityResponse>;
}

export interface SummaryResponseDto {
  totalInvestments: number;
  totalSavings: number;
  totalDebts: number;
  totalMonthlySpendings: number;
};

export interface SavingsLongevityResponse {
  months: number,
  till: Date,
  grade: SavingsLongevityGrade
}

export enum SavingsLongevityGrade {
    Critical = "Critical",
    Insufficient = "Insufficient",
    NeedsImprovement = "NeedsImprovement",
    Recommended = "Recommended",
    Strong = "Strong",
    Excellent = "Excellent",
}
