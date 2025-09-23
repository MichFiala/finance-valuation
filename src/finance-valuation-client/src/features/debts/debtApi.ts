import { create, deleteEntry, fetchEntries, update } from "../../shared/crudApi";
import { DebtsResponseDto } from "./debtsResponseModel";

const Endpoint = "debts";

export async function fetchDebts(){
  return fetchEntries(Endpoint) as Promise<DebtsResponseDto>;
}

export async function createDebt(name: string, amount: number) {
  create(Endpoint, { name, amount });
}

export async function updateDebt(id: number, name: string, amount: number) {
  update(Endpoint, id, { name, amount });
}

export async function deleteDebt(id: number) {
  deleteEntry(Endpoint, id);
}

