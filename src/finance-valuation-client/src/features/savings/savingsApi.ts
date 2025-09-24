import { create, deleteEntry, fetchEntries, update } from "../../shared/crudApi";
import { SavingsResponseDto } from "./savingsModel";

const Endpoint = "savings";

export async function fetchSavings() {
  return await fetchEntries(Endpoint) as Promise<SavingsResponseDto>;
}

export function createSaving(name: string, amount: number) {
  return create(Endpoint, { name, amount });
}

export function updateSaving(id: number, name: string, amount: number) {
  return update(Endpoint, id, { name, amount });
}

export function deleteSaving(id: number) {
  return deleteEntry(Endpoint, id);
}
