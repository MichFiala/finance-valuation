import { Button, Divider, Grid, Typography } from "@mui/material";
import { useState, useEffect } from "react";
import { CardModule } from "../../shared/CardModule";
import SavingsIcon from "@mui/icons-material/Savings";
import { SpendingsDto, SpendingsFrequency } from "./spendingsModel";
import { createSpending, fetchSpendings, updateSpending } from "./spendingsApi";
import {
  spendingMonthlyColor,
  spendingMonthlyTextColor,
  spendingQuarterlyColor,
  spendingQuarterlyTextColor,
  spendingYearlyColor,
  spendingYearlyTextColor,
} from "./spendingStylesSettings";

export const SpendingsModule = (
  { enableEditing }: { enableEditing: boolean } = { enableEditing: false }
) => {
  const [spendings, setSpendings] = useState<SpendingsDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [reloadCounter, setReloadCounter] = useState(0);

  useEffect(() => {
    fetchSpendings()
      .then((data) => {
        setSpendings(data.spendings);
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

  const handleCreateOrUpdate = async (
    id: number | undefined,
    name: string,
    amount: number,
    type: string,
    isMandatory: boolean
  ) => {
    if(id !== undefined){
      await updateSpending(id, name, amount, type as SpendingsFrequency, isMandatory);
    }
    else {
      await createSpending(name, amount, type as SpendingsFrequency, isMandatory);
    }

    setReloadCounter(reloadCounter + 1);
  };

  const handleCreateEmptySpending = (frequency: SpendingsFrequency) => {
    const emptySpending = {
      frequency: frequency,
      amount: 0,
      name: "",
    } as SpendingsDto;
    setSpendings([...spendings, emptySpending]);
  };
  return (
    <>
      <Typography variant="h6" component="h6">
        Monthly
      </Typography>
      <Divider />
      <Grid container spacing={2}>
        {spendings
          .filter((spending) => spending.frequency === "Monthly")
          .sort((a, b) => a.isMandatory === b.isMandatory ? 0 : a.isMandatory ? -1 : 1)
          .map((spending) => (
            <Grid key={`Spending-${spending.id}`} size={3}>
              <CardModule
                entry={spending}
                handleUpdate={handleCreateOrUpdate}
                color={spendingMonthlyColor}
                textColor={spendingMonthlyTextColor}
                icon={<SavingsIcon />}
                enableEditing={enableEditing}
              />
            </Grid>
          ))}
        <Grid size={3} textAlign={'left'} alignContent={'start'}>
          <Button
            size="large"
            style={{ backgroundColor: spendingMonthlyColor, color: spendingMonthlyTextColor }}
            onClick={() =>
              handleCreateEmptySpending("Monthly" as SpendingsFrequency)
            }
          >
            Add new
          </Button>
        </Grid>
      </Grid>

      <Typography variant="h6" component="h6">
        Quaterly
      </Typography>
      <Divider />
      <Grid container spacing={2}>
        {spendings
          .filter((spending) => spending.frequency === "Quaterly")
          .sort((a, b) => a.isMandatory === b.isMandatory ? 0 : a.isMandatory ? -1 : 1)
          .map((spending) => (
            <Grid key={`Spending-${spending.id}`} size={3}>
              <CardModule
                entry={spending}
                handleUpdate={handleCreateOrUpdate}
                color={spendingQuarterlyColor}
                textColor={spendingQuarterlyTextColor}
                icon={<SavingsIcon />}
                enableEditing={enableEditing}
              />
            </Grid>
          ))}
        <Grid size={3} textAlign={'left'} alignContent={'start'}>
          <Button
            size="large"
            style={{ backgroundColor: spendingQuarterlyColor, color: spendingQuarterlyTextColor }}
            onClick={() =>
              handleCreateEmptySpending("Quaterly" as SpendingsFrequency)
            }
          >
            Add new
          </Button>
        </Grid>
      </Grid>
      <Typography variant="h6" component="h6">
        Yearly
      </Typography>
      <Divider />
      <Grid container spacing={2}>
        {spendings
          .filter((spending) => spending.frequency === "Yearly")
          .sort((a, b) => a.isMandatory === b.isMandatory ? 0 : a.isMandatory ? -1 : 1)          
          .map((spending) => (
            <Grid key={`Spending-${spending.id}`} size={3}>
              <CardModule
                entry={spending}
                handleUpdate={handleCreateOrUpdate}
                color={spendingYearlyColor}
                textColor={spendingYearlyTextColor}
                icon={<SavingsIcon />}
                enableEditing={enableEditing}
              />
            </Grid>
          ))}
          <Grid size={3} textAlign={'left'} alignContent={'start'}>
          <Button
            size="large"
            style={{ backgroundColor: spendingYearlyColor, color: spendingYearlyTextColor }}
            onClick={() =>
              handleCreateEmptySpending("Yearly" as SpendingsFrequency)
            }
          >
            Add new
          </Button>
        </Grid>
      </Grid>
    </>
  );
};
