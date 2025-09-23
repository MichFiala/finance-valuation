import { create, deleteEntry, fetchEntries, update } from "../../shared/crudApi";
import { SavingsResponseDto } from "./savingsModel";

const Endpoint = "savings";

export async function fetchSavings() {
  return fetchEntries(Endpoint) as Promise<SavingsResponseDto>;
}

export async function createSaving(name: string, amount: number) {
  create(Endpoint, { name, amount });
}

export async function updateSaving(id: number, name: string, amount: number) {
  update(Endpoint, id, { name, amount });
}

export async function deleteSaving(id: number) {
  deleteEntry(Endpoint, id);
}
