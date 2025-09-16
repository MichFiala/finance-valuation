export interface SavingsDto{
    id: number;
    name: string;
    amount: number;
    targetAmount: number | undefined;
}

export interface SavingsResponseDto {
    savings: SavingsDto[];
}