import { fetchEntries, update, deleteEntry, create } from "../../shared/crudApi";
import { InvestmentsResponseDto } from "./investmentModel";

const Endpoint = "investments";

export async function fetchInvestments() {
  return fetchEntries(Endpoint) as Promise<InvestmentsResponseDto>;
}

export async function createInvestment(name: string, amount: number) {
  create(Endpoint, { name, amount });
}

export async function updateInvestment(id: number, name: string, amount: number) {
  update(Endpoint, id, { name, amount });
}

export async function deleteInvestment(id: number) {
  deleteEntry(Endpoint, id);
}

