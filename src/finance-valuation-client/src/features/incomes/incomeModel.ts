export interface IncomeDto{
    id: number;
    name: string;
    amount: number;
    date: Date;
    isMainIncome: boolean;
}

export interface IncomesResponseDto {
    incomes: IncomeDto[];
}