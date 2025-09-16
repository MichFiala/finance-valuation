import {
  Grid,
} from "@mui/material";
import { useState, useEffect } from "react";
import { fetchSavings, updateSaving } from "./savingsApi";
import { SavingsDto } from "./savingsModel";
import { savingsColor, savingsTextColor } from "./savingStylesSettings";
import { CardModule } from "../../shared/CardModule";
import SavingsIcon from "@mui/icons-material/Savings";


export const SavingsModule = (
  { enableEditing }: { enableEditing: boolean } = { enableEditing: false }
) => {
  const [savings, setSavings] = useState<SavingsDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [reloadCounter, setReloadCounter] = useState(0);

  useEffect(() => {
    fetchSavings()
      .then((data) => {
        setSavings(data.savings);
      })
      .catch((err) => {
        setError(err.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [reloadCounter]);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  const handleUpdate = async (id: number, name:string, amount: number) => {
    await updateSaving(id, name, amount);
    
    setReloadCounter(reloadCounter + 1);
  };
  return (
    <>
      {savings.map((saving) => (
        <Grid key={`Saving-${saving.id}`} size={3}>
          <CardModule
            entry={saving}
            handleUpdate={handleUpdate}
            color={savingsColor}
            textColor={savingsTextColor}
            icon={<SavingsIcon />}
            enableEditing={enableEditing}
          />
        </Grid>
      ))}
    </>
  );
};
