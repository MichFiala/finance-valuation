import { fetchEntries, update, deleteEntry, create } from "../../shared/crudApi";
import { InvestmentsResponseDto } from "./investmentModel";

const Endpoint = "investments";

export async function fetchInvestments() {
  return fetchEntries(Endpoint) as Promise<InvestmentsResponseDto>;
}

export function createInvestment(name: string, amount: number) {
  return create(Endpoint, { name, amount });
}

export function updateInvestment(id: number, name: string, amount: number) {
  return update(Endpoint, id, { name, amount });
}

export function deleteInvestment(id: number) {
  return deleteEntry(Endpoint, id);
}

