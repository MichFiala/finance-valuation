import { create, deleteEntry, fetchEntries, update } from "../../shared/crudApi";
import { SpendingsFrequency, SpendingsResponseDto } from "./spendingsModel";

const Endpoint = "spendings";

export async function fetchSpendings() {
  return fetchEntries(Endpoint) as Promise<SpendingsResponseDto>;
}

export async function createSpending(name: string, amount: number, frequency: SpendingsFrequency,  isMandatory: boolean) {
  create(Endpoint, { name, amount, frequency, isMandatory });
}

export async function updateSpending(id: number, name: string, amount: number, frequency: SpendingsFrequency, isMandatory: boolean) {
  update(Endpoint, id, { name, amount, frequency, isMandatory });
}

export async function deleteSpending(id: number) {
  deleteEntry(Endpoint, id);
}

