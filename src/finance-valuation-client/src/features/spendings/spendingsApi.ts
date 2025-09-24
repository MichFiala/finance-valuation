import { create, deleteEntry, fetchEntries, update } from "../../shared/crudApi";
import { SpendingsFrequency, SpendingsResponseDto } from "./spendingsModel";

const Endpoint = "spendings";

export function fetchSpendings() {
  return fetchEntries(Endpoint) as Promise<SpendingsResponseDto>;
}

export function createSpending(name: string, amount: number, frequency: SpendingsFrequency,  isMandatory: boolean) {
  return create(Endpoint, { name, amount, frequency, isMandatory });
}

export function updateSpending(id: number, name: string, amount: number, frequency: SpendingsFrequency, isMandatory: boolean) {
  return update(Endpoint, id, { name, amount, frequency, isMandatory });
}

export function deleteSpending(id: number) {
  return deleteEntry(Endpoint, id);
}

