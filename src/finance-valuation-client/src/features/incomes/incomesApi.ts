import { deleteEntry, fetchEntries } from "../../shared/crudApi";
import { IncomesResponseDto } from "./incomeModel";

export const IncomesEndpoint = "incomes";

export function fetchIncomes() {
  return fetchEntries(IncomesEndpoint) as Promise<IncomesResponseDto>;
}

export function toUtcDateOnlyString(date: Date): string {
  return `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, "0")}-${String(date.getDate()).padStart(2, "0")}`;
}

export function deleteIncome(id: number) {
  return deleteEntry(IncomesEndpoint, id);
}

