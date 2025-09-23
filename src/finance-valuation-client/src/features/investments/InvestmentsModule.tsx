import { useEffect, useState } from "react";
import { InvestmentDto } from "./investmentModel";
import {
  createInvestment,
  deleteInvestment,
  fetchInvestments,
  updateInvestment,
} from "./investmentsApi";
import { Button, Grid } from "@mui/material";
import {
  investmentColor,
  investmentTextColor,
} from "./investmentStylesSettings";
import { CardModule } from "../../shared/CardModule";
import TrendingUpIcon from "@mui/icons-material/TrendingUp";
import AddIcon from '@mui/icons-material/Add';

export const InvestmentsModule = (
  { enableEditing }: { enableEditing: boolean } = { enableEditing: false }
) => {
  const [investments, setInvestments] = useState<InvestmentDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [reloadCounter, setReloadCounter] = useState(0);

  useEffect(() => {
    fetchInvestments()
      .then((data) => {
        setInvestments(data.investments);
      })
      .catch((err) => {
        setError(err.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [reloadCounter]);

  const handleCreateOrUpdate = async (
    id: number | undefined,
    name: string,
    amount: number
  ) => {
    if (id !== undefined) {
      await updateInvestment(id, name, amount);
    } else {
      await createInvestment(name, amount);
    }

    setReloadCounter(reloadCounter + 1);
  };

  const handleDelete = async (entry: any) => {
    const investment = entry as InvestmentDto;
    if (investment.id === null) {
      setInvestments([...investments.filter((e) => e.id !== entry)]);
      return;
    }
    await deleteInvestment(investment.id);
    setReloadCounter(reloadCounter + 1);
  };

  const handleCreateEmpty = () => {
    const empty = {
      amount: 0,
      name: "",
    } as InvestmentDto;
    setInvestments([...investments, empty]);
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      {investments.sort((a, b) => b.amount - a.amount).map((investment) => (
        <Grid key={`Investment-${investment.id}`} size={3}>
          <CardModule
            entry={investment}
            handleCreateOrUpdate={handleCreateOrUpdate}
            handleDelete={handleDelete}
            color={investmentColor}
            textColor={investmentTextColor}
            icon={<TrendingUpIcon />}
            enableEditing={enableEditing}
          />
        </Grid>
      ))}
      {enableEditing && (
        <Grid size={3} textAlign={"left"} alignContent={"start"}>
          <Button
            size="large"
            style={{
              backgroundColor: investmentColor,
              color: investmentTextColor,
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
