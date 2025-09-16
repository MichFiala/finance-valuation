import { useEffect, useState } from "react";
import { InvestmentDto } from "./investmentModel";
import { fetchInvestments, updateInvestment } from "./investmentsApi";
import { Grid } from "@mui/material";
import { investmentColor, investmentTextColor } from "./investmentStylesSettings";
import { CardModule } from "../../shared/CardModule";
import TrendingUpIcon from '@mui/icons-material/TrendingUp';



export const InvestmentsModule = ({ enableEditing }: { enableEditing: boolean } = { enableEditing: false }) => {
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

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  const handleUpdate = async (id: number, name:string, amount: number) => {
    await updateInvestment(id, name, amount);
    
    setReloadCounter(reloadCounter + 1);
  };
  return (
    <>
      {investments.map((investment) => (
        <Grid key={`Investment-${investment.id}`} size={3}>
          <CardModule
            entry={investment}
            handleUpdate={handleUpdate}
            color={investmentColor}
            textColor={investmentTextColor}
            icon={<TrendingUpIcon />}
            enableEditing={enableEditing}
          />
        </Grid>
      ))}
    </>
  );
};

