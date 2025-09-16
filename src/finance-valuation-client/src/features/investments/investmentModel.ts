export interface InvestmentDto{
    id: number;
    name: string;
    amount: number;
}

export interface InvestmentsResponseDto {
    investments: InvestmentDto[];
}