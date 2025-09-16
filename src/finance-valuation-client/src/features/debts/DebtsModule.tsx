import { useEffect, useState } from "react";
import { fetchDebts, updateDebt } from "./debtApi";
import { DebtsResponseDto } from "./debtsResponseModel";
import { Grid } from "@mui/material";
import { debtColor, debtTextColor } from "./debtStylesSettings";
import { CardModule } from "../../shared/CardModule";
import HomeWorkIcon from '@mui/icons-material/HomeWork';

export const DebtsModule = ({ enableEditing }: { enableEditing: boolean }) => {
  const [debtsResponse, setDebtsResponse] = useState<DebtsResponseDto | null>(
    null
  );
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [reloadCounter, setReloadCounter] = useState(0);

  useEffect(() => {
    fetchDebts()
      .then(setDebtsResponse)
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [reloadCounter]);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  async function handleUpdate(
    id: number,
    name: string,
    amount: number
  ): Promise<void> {
    await updateDebt(id, name, amount);

    setReloadCounter(reloadCounter + 1);
  }

  return (
    <>
      {debtsResponse?.debts.map((debt) => (
        <Grid key={`Debt-${debt.id}`} size={3}>
          <CardModule
            entry={debt}
            handleUpdate={handleUpdate}
            color={debtColor}
            textColor={debtTextColor}
            icon={debt.debtType === 'Mortgage' && <HomeWorkIcon />}
            enableEditing={enableEditing}
          />
        </Grid>
      ))}
    </>
  );
};
