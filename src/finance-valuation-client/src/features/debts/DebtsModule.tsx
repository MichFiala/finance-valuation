import { useEffect, useState } from "react";
import { DebtsEndpoint } from "./debtApi";
import { Button, Grid } from "@mui/material";
import { debtColor, debtTextColor } from "./debtStylesSettings";
import HomeWorkIcon from "@mui/icons-material/HomeWork";
import { DebtDto, DebtUpdateDto } from "./debtModel";
import AddIcon from "@mui/icons-material/Add";
import {
  createOrUpdate,
  deleteEntry,
  fetchEntries,
} from "../../shared/crudApi";
import { DebtCardModule } from "./DebtCardModule";

export const DebtsModule = ({ enableEditing }: { enableEditing: boolean }) => {
  const [debts, setDebts] = useState<DebtDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [reloadCounter, setReloadCounter] = useState(0);

  useEffect(() => {
    fetchEntries(DebtsEndpoint)
      .then((data) => setDebts(data.debts))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [reloadCounter]);

  const handleCreateOrUpdate = async (updateDto: DebtUpdateDto) => {
    const entry = {
      ...updateDto,
      amount: parseFloat(updateDto.amount.replace(",", ".")) || 0,
      interest: parseFloat(updateDto.interest!.replace(",", ".")) || 0,
      payment: parseFloat(updateDto.payment!.replace(",", ".")) || 0,
    };

    await createOrUpdate(DebtsEndpoint, entry);

    setReloadCounter(reloadCounter + 1);
  };

  const handleDelete = async (entry: any) => {
    const debt = entry as DebtDto;
    if (debt.id === null) {
      setDebts([...debts.filter((e) => e.id !== entry)]);
      return;
    }
    await deleteEntry(DebtsEndpoint, debt.id);
    setReloadCounter(reloadCounter + 1);
  };

  const handleCreateEmpty = () => {
    const empty = {
      amount: 0,
      name: "",
      debtType: "Mortgage",
    } as DebtDto;
    setDebts([...debts, empty]);
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      {debts
        .sort((a, b) => b.amount - a.amount)
        .map((debt) => (
          <Grid key={`Debt-${debt.id}`} size={{ xs: 6, sm: 6, md: 3, lg: 3, xl: 3 }}>
            <DebtCardModule
              entryDto={debt}
              handleCreateOrUpdate={handleCreateOrUpdate}
              handleDelete={handleDelete}
              color={debtColor}
              textColor={debtTextColor}
              icon={debt.debtType === "Mortgage" && <HomeWorkIcon />}
              enableEditing={enableEditing}
            />
          </Grid>
        ))}
      {enableEditing && (
        <Grid textAlign={"left"} alignContent={"start"} size={{ xs: 6, sm: 6, md: 3, lg: 3, xl: 3 }}>
          <Button
            size="large"
            style={{
              backgroundColor: debtColor,
              color: debtTextColor,
            }}
            onClick={() => handleCreateEmpty()}
          >
            <AddIcon></AddIcon>
          </Button>
        </Grid>
      )}
    </>
  );
};
