import { create, deleteEntry, fetchEntries, update } from "../../shared/crudApi";
import { IncomesResponseDto } from "./incomeModel";

const Endpoint = "incomes";

export async function fetchIncomes() {
  return fetchEntries(Endpoint) as Promise<IncomesResponseDto>;
}

function toUtcDateOnlyString(date: Date): string {
  return `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, "0")}-${String(date.getDate()).padStart(2, "0")}`;
}

export async function createIncome(name: string, amount: number, date: Date) {
  create(Endpoint, { name, amount, date: toUtcDateOnlyString(date)});
}

export async function updateIncome(id: number, name: string, amount: number, date:Date) {
  update(Endpoint, id, { name, amount, date: toUtcDateOnlyString(date) });
}

export async function deleteIncome(id: number) {
  deleteEntry(Endpoint, id);
}

