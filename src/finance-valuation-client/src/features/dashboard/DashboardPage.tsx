import {
  Card,
  CardContent,
  CardHeader,
  Divider,
  Grid,
  Stack,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { fetchSummary, SummaryResponseDto, SavingsLongevityResponse, fetchSavingsLongevity } from "./summaryApi";
import { DebtsModule } from "../debts/DebtsModule";
import { InvestmentsModule } from "../investments/InvestmentsModule";
import { SavingsModule } from "../savings/SavingsModule";
import { SavingsLongevityColors } from "./dashboardStylesSettings";
import FavoriteIcon from '@mui/icons-material/Favorite';

const investmentsCardStyle : React.CSSProperties = {
  display: "flex",
  flexDirection: "column",
  background: "linear-gradient(328deg,rgba(22, 105, 122, 1) 35%, rgba(128, 206, 215, 1) 100%)",
  color: "white"
}

const savingsCardStyle : React.CSSProperties = {
  display: "flex",
  flexDirection: "column",
  background: "linear-gradient(328deg,rgba(25, 125, 58, 1) 35%, rgba(153, 209, 156, 1) 100%)",
  color: "white"
}

const debtsCardStyle : React.CSSProperties = {
  display: "flex",
  flexDirection: "column",
  background: "linear-gradient(328deg,rgba(186, 28, 28, 1) 35%, rgba(233, 153, 153, 1) 100%)",
  color: "white"
}

const monthlySpendingsCardStyle : React.CSSProperties = {
  display: "flex",
  flexDirection: "column",
  background: "linear-gradient(328deg,rgba(255, 146, 72, 1) 46%, rgba(231, 170, 130, 1) 100%)",
  color: "white"
}

export default function DashboardPage() {
  const [summaryResponse, setSummaryResponse] = useState<SummaryResponseDto | null>(null);
  const [savingsLongevityResponse, setsavingsLongevityResponse] = useState<SavingsLongevityResponse | null>(null)
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {

    const fetchData = async() => {
      var summaryPromise = fetchSummary();
      var savingsLongevityPromise = fetchSavingsLongevity();
      
      
      await savingsLongevityPromise;
      setSummaryResponse(await summaryPromise);
      setsavingsLongevityResponse(await savingsLongevityPromise);
    }
    fetchData()
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);
  
  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <Grid container width={"100%"} spacing={2}>
      <Grid size={4}>
        <Card sx={{height: '100%'}} style={investmentsCardStyle}>
            <CardContent style={{ marginTop: "auto" }}>
              <Typography component="div" variant="h5" gutterBottom>
                {summaryResponse?.totalInvestments.toLocaleString('cs-CZ', { style: 'currency', currency: 'CZK', minimumFractionDigits: 0 })}
              </Typography>
              <Typography
                variant="body1"
                component="div"
              >
                Total Investments
              </Typography>
            </CardContent>
        </Card>
      </Grid>
      <Grid size={4}>
        <Card sx={{height: '100%'}} style={savingsCardStyle}>
            <CardContent style={{ marginTop: "auto" }}>
              <Typography component="div" variant="h5">
                {summaryResponse?.totalSavings.toLocaleString('cs-CZ', { style: 'currency', currency: 'CZK', minimumFractionDigits: 0 })}
              </Typography>
              <Typography
                variant="body1"
                component="div"
              >
                Total Savings
              </Typography>
            </CardContent>
        </Card>
      </Grid>
      <Grid size={4}>
        <Stack spacing={2}>
          <Card style={debtsCardStyle}>
            <CardContent style={{ marginTop: "auto" }}>
              <Typography variant="h5">{summaryResponse?.totalDebts.toLocaleString('cs-CZ', { style: 'currency', currency: 'CZK', minimumFractionDigits: 0 })}</Typography>
              <Typography variant="body1" component="div">Total Debts</Typography>
            </CardContent>
          </Card>
          <Card style={monthlySpendingsCardStyle}>
            <CardContent style={{ marginTop: "auto" }}>
              <Typography variant="h5">{summaryResponse?.totalMonthlySpendings.toLocaleString('cs-CZ', { style: 'currency', currency: 'CZK', minimumFractionDigits: 0 })}</Typography>
              <Typography variant="body1" component="div">Mandatory Monthly Spendings</Typography>
            </CardContent>
          </Card>
        </Stack>
      </Grid>
      <Grid size={12}>
        <Divider />
      </Grid>
      <Grid container size={12} marginTop={2}>
        <DebtsModule key={"Debts"} enableEditing={false}/>
        <InvestmentsModule key={"Investments"} enableEditing={false} />
        <SavingsModule key={"Savings"} enableEditing={false} />
      </Grid>
      <Grid size={12}>
        <Divider/>
      </Grid>
      <Grid size={4}>
        {savingsLongevityResponse &&
        <Card>
          <CardHeader avatar={<FavoriteIcon/>} title="Savings Longevity" style={{background: SavingsLongevityColors[savingsLongevityResponse.grade], color: "white"}}></CardHeader>
          <CardContent>
            <Typography variant="body1">
              <b>{savingsLongevityResponse?.till.toLocaleString()}</b>
            </Typography>
            <Typography variant="body2">
              {savingsLongevityResponse.months} Months
            </Typography>
            <Typography variant="body2">
              {(savingsLongevityResponse.months / 12).toPrecision(2)} Years
            </Typography>
            <Typography variant="body2">
              Grade: {savingsLongevityResponse.grade}
            </Typography>
          </CardContent>
        </Card>
        }
      </Grid>
    </Grid>
  );
}
