export enum SpendingsFrequency {
    Monthly = 'Monthly',
    Quaterly = 'Quaterly',
    Yearly = 'Yearly'
}

export interface SpendingsDto{
    id: number;
    name: string;
    amount: number;
    frequency: SpendingsFrequency;
    isMandatory: boolean;
}

export interface SpendingsResponseDto {
    spendings: SpendingsDto[];
}