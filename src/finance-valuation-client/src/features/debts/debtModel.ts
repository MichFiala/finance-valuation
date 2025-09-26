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

export interface DebtUpdateDto {
  id: number;
  name: string;
  debtType: DebtType;
  amount: string;
  interest: string;
  payment: string;
}