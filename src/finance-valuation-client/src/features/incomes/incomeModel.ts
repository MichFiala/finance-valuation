export interface IncomeDto{
    id: number;
    name: string;
    amount: number;
    date: string;
}

export interface IncomesResponseDto {
    incomes: IncomeDto[];
}