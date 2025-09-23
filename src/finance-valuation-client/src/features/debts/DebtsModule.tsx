import { useEffect, useState } from "react";
import { createDebt, deleteDebt, fetchDebts, updateDebt } from "./debtApi";
import { Button, Grid } from "@mui/material";
import { debtColor, debtTextColor } from "./debtStylesSettings";
import { CardModule } from "../../shared/CardModule";
import HomeWorkIcon from "@mui/icons-material/HomeWork";
import { DebtDto } from "./debtModel";
import AddIcon from '@mui/icons-material/Add';

export const DebtsModule = ({ enableEditing }: { enableEditing: boolean }) => {
  const [debts, setDebts] = useState<DebtDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [reloadCounter, setReloadCounter] = useState(0);

  useEffect(() => {
    fetchDebts()
      .then((data) => setDebts(data.debts))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [reloadCounter]);

  const handleCreateOrUpdate = async (
    id: number | undefined,
    name: string,
    amount: number
  ) => {
    if (id !== undefined) {
      await updateDebt(id, name, amount);
    } else {
      await createDebt(name, amount);
    }

    setReloadCounter(reloadCounter + 1);
  };

  const handleDelete = async (entry: any) => {
    const debt = entry as DebtDto;
    if (debt.id === null) {
      setDebts([...debts.filter((e) => e.id !== entry)]);
      return;
    }
    await deleteDebt(debt.id);
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
      {debts.sort((a, b) => b.amount - a.amount).map((debt) => (
        <Grid key={`Debt-${debt.id}`} size={3}>
          <CardModule
            entry={debt}
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
        <Grid size={3} textAlign={"left"} alignContent={"start"}>
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
