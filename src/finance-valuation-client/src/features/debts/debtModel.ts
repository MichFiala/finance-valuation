export enum DebtType {
  Mortgage = 'Mortgage'
}

export interface DebtDto {
  id: number;
  name: string;
  debtType: DebtType;
  amount: number;
  interest: number;
  payment: number;
}