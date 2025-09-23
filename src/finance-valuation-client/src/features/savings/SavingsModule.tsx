import { Button, Grid } from "@mui/material";
import { useState, useEffect } from "react";
import { createSaving, deleteSaving, fetchSavings, updateSaving } from "./savingsApi";
import { SavingsDto } from "./savingsModel";
import { savingsColor, savingsTextColor } from "./savingStylesSettings";
import { CardModule } from "../../shared/CardModule";
import SavingsIcon from "@mui/icons-material/Savings";
import AddIcon from '@mui/icons-material/Add';

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

  const handleCreateOrUpdate = async (id: number | undefined, name: string, amount: number) => {
      if (id !== undefined) {
        await updateSaving(
          id,
          name,
          amount
        );
      } else {
        await createSaving(
          name,
          amount
        );
      }
  
      setReloadCounter(reloadCounter + 1);
    };
  

  const handleDelete = async (entry: any) => {
    const saving = entry as SavingsDto;
    if(saving.id === null){
      setSavings([...savings.filter(e => e.id !== entry)]);
      return;
    }
    await deleteSaving(saving.id);
    setReloadCounter(reloadCounter + 1);
  }

  const handleCreateEmpty = () => {
    const empty = {
      amount: 0,
      name: ""
    } as SavingsDto;
    setSavings([...savings, empty]);
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      {savings.sort((a, b) => b.amount - a.amount).map((saving) => (
        <Grid key={`Saving-${saving.id}`} size={3}>
          <CardModule
            entry={saving}
            handleCreateOrUpdate={handleCreateOrUpdate}
            handleDelete={handleDelete}
            color={savingsColor}
            textColor={savingsTextColor}
            icon={<SavingsIcon />}
            enableEditing={enableEditing}
          />
        </Grid>
      ))}
      {enableEditing && (
        <Grid size={3} textAlign={"left"} alignContent={"start"}>
          <Button
            size="large"
            style={{
              backgroundColor: savingsColor,
              color: savingsTextColor,
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
