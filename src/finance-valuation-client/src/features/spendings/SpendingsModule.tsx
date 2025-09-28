import { Button, Divider, Grid, Typography } from "@mui/material";
import { useState, useEffect } from "react";
import SavingsIcon from "@mui/icons-material/Savings";
import { SpendingsDto, SpendingsFrequency } from "./spendingsModel";
import {
  SpendingsEndpoint,
} from "./spendingsApi";
import {
  spendingMonthlyColor,
  spendingMonthlyTextColor,
  spendingQuarterlyColor,
  spendingQuarterlyTextColor,
  spendingYearlyColor,
  spendingYearlyTextColor,
} from "./spendingStylesSettings";
import AddIcon from "@mui/icons-material/Add";
import { createOrUpdate, fetchEntries } from "../../shared/crudApi";
import { SpendingsCardModule } from "./SpendingsCardModule";

export const SpendingsModule = (
  { enableEditing }: { enableEditing: boolean } = { enableEditing: false }
) => {
  const [spendings, setSpendings] = useState<SpendingsDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [reloadCounter, setReloadCounter] = useState(0);

  useEffect(() => {
    fetchEntries(SpendingsEndpoint)
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

  const handleCreateOrUpdate = async (entry: any) => {
    await createOrUpdate(SpendingsEndpoint, entry);

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

  const handleDelete = async (entry: any) => {
    if ((entry as SpendingsDto).id === null) {
      setSpendings([...spendings.filter((s) => s.id !== entry)]);
      return;
    }

    setReloadCounter(reloadCounter + 1);
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      <Typography variant="h6" component="h6" sx={[(theme) => ({color: theme.palette.text.primary})]}>
        Monthly
      </Typography>
      <Divider sx={[(theme) => ({color: theme.palette.text.primary})]}/>
      <Grid container spacing={2}>
        {spendings
          .filter((spending) => spending.frequency === "Monthly")
          .sort((a, b) =>
            a.isMandatory === b.isMandatory ? 0 : a.isMandatory ? -1 : 1
          )
          .map((spending) => (
            <Grid key={`Spending-${spending.id}`} size={{ xs: 6, sm: 6, md: 4, lg: 4, xl: 4 }}>
              <SpendingsCardModule
                entryDto={spending}
                handleCreateOrUpdate={handleCreateOrUpdate}
                handleDelete={handleDelete}
                color={spendingMonthlyColor}
                textColor={spendingMonthlyTextColor}
                icon={<SavingsIcon />}
                enableEditing={enableEditing}
              />
            </Grid>
          ))}
        <Grid textAlign={"left"} alignContent={"start"} size={{ xs: 6, sm: 6, md: 4, lg: 4, xl: 4 }}>
          <Button
            size="large"
            style={{
              backgroundColor: spendingMonthlyColor,
              color: spendingMonthlyTextColor,
            }}
            onClick={() =>
              handleCreateEmptySpending("Monthly" as SpendingsFrequency)
            }
          >
            <AddIcon/>
          </Button>
        </Grid>
      </Grid>

      <Typography variant="h6" component="h6" sx={[(theme) => ({color: theme.palette.text.primary})]}>
        Quaterly
      </Typography>
      <Divider sx={[(theme) => ({color: theme.palette.text.primary})]}/>
      <Grid container spacing={2}>
        {spendings
          .filter((spending) => spending.frequency === "Quaterly")
          .sort((a, b) =>
            a.isMandatory === b.isMandatory ? 0 : a.isMandatory ? -1 : 1
          )
          .map((spending) => (
            <Grid key={`Spending-${spending.id}`} size={{ xs: 12, sm: 6, md: 4, lg: 4, xl: 4 }}>
              <SpendingsCardModule
                entryDto={spending}
                handleCreateOrUpdate={handleCreateOrUpdate}
                handleDelete={handleDelete}
                color={spendingQuarterlyColor}
                textColor={spendingQuarterlyTextColor}
                icon={<SavingsIcon />}
                enableEditing={enableEditing}
              />
            </Grid>
          ))}
        <Grid textAlign={"left"} alignContent={"start"} size={{ xs: 12, sm: 6, md: 4, lg: 4, xl: 4 }}>
          <Button
            size="large"
            style={{
              backgroundColor: spendingQuarterlyColor,
              color: spendingQuarterlyTextColor,
            }}
            onClick={() =>
              handleCreateEmptySpending("Quaterly" as SpendingsFrequency)
            }
          >
            <AddIcon/>
          </Button>
        </Grid>
      </Grid>
      <Typography variant="h6" component="h6" sx={[(theme) => ({color: theme.palette.text.primary})]}>
        Yearly
      </Typography>
      <Divider sx={[(theme) => ({color: theme.palette.text.primary})]}/>
      <Grid container spacing={2}>
        {spendings
          .filter((spending) => spending.frequency === "Yearly")
          .sort((a, b) =>
            a.isMandatory === b.isMandatory ? 0 : a.isMandatory ? -1 : 1
          )
          .map((spending) => (
            <Grid key={`Spending-${spending.id}`} size={{ xs: 6, sm: 6, md: 4, lg: 4, xl: 4 }}>
              <SpendingsCardModule
                entryDto={spending}
                handleCreateOrUpdate={handleCreateOrUpdate}
                handleDelete={handleDelete}
                color={spendingYearlyColor}
                textColor={spendingYearlyTextColor}
                icon={<SavingsIcon />}
                enableEditing={enableEditing}
              />
            </Grid>
          ))}
        {enableEditing && (
          <Grid textAlign={"left"} alignContent={"start"} size={{ xs: 6, sm: 6, md: 4, lg: 4, xl: 4 }}>
            <Button
              size="large"
              style={{
                backgroundColor: spendingYearlyColor,
                color: spendingYearlyTextColor,
              }}
              onClick={() =>
                handleCreateEmptySpending("Yearly" as SpendingsFrequency)
              }
            >
              <AddIcon/>
            </Button>
          </Grid>
        )}
      </Grid>
    </>
  );
};
