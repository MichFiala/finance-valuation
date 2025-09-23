export interface IncomeDto{
    id: number;
    name: string;
    amount: number;
    date: Date;
}

export interface IncomesResponseDto {
    incomes: IncomeDto[];
}