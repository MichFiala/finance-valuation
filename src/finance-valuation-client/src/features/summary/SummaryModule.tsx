import {
  Card,
  CardContent,
  Divider,
  Grid,
  Stack,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { fetchSummary, SummaryResponseDto } from "./summaryApi";
import { useTranslation } from "react-i18next";

const investmentsCardStyle: React.CSSProperties = {
  display: "flex",
  flexDirection: "column",
  background:
    "linear-gradient(328deg,rgba(22, 105, 122, 1) 35%, rgba(128, 206, 215, 1) 100%)",
  color: "white",
};

const savingsCardStyle: React.CSSProperties = {
  display: "flex",
  flexDirection: "column",
  background:
    "linear-gradient(328deg,rgba(25, 125, 58, 1) 35%, rgba(153, 209, 156, 1) 100%)",
  color: "white",
};

const debtsCardStyle: React.CSSProperties = {
  display: "flex",
  flexDirection: "column",
  background:
    "linear-gradient(328deg,rgba(186, 28, 28, 1) 35%, rgba(233, 153, 153, 1) 100%)",
  color: "white",
};

const monthlySpendingsCardStyle: React.CSSProperties = {
  display: "flex",
  flexDirection: "column",
  background:
    "linear-gradient(328deg,rgba(255, 146, 72, 1) 46%, rgba(231, 170, 130, 1) 100%)",
  color: "white",
};

export function SummaryModule() {
  const [summaryResponse, setSummaryResponse] =
    useState<SummaryResponseDto | null>(null);

  const { t } = useTranslation();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetchSummary()
      .then((data) => setSummaryResponse(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <Grid container spacing={2}>
      <Grid size={4}>
        <Card sx={{ height: "100%" }} style={investmentsCardStyle}>
          <CardContent style={{ marginTop: "auto" }}>
            <Typography component="div" variant="h5" gutterBottom>
              {summaryResponse?.totalInvestments.toLocaleString("cs-CZ", {
                style: "currency",
                currency: "CZK",
                minimumFractionDigits: 0,
              })}
            </Typography>
            <Typography variant="body1" component="div">
              {t("total_investments")}
            </Typography>
          </CardContent>
        </Card>
      </Grid>
      <Grid size={4}>
        <Card sx={{ height: "100%" }} style={savingsCardStyle}>
          <CardContent style={{ marginTop: "auto" }}>
            <Typography component="div" variant="h5">
              {summaryResponse?.totalSavings.toLocaleString("cs-CZ", {
                style: "currency",
                currency: "CZK",
                minimumFractionDigits: 0,
              })}
            </Typography>
            <Typography variant="body1" component="div">
              {t("total_savings")}
            </Typography>
          </CardContent>
        </Card>
      </Grid>
      <Grid size={4}>
        <Stack spacing={2}>
          <Card style={debtsCardStyle}>
            <CardContent style={{ marginTop: "auto" }}>
              <Typography variant="h5">
                {summaryResponse?.totalDebts.toLocaleString("cs-CZ", {
                  style: "currency",
                  currency: "CZK",
                  minimumFractionDigits: 0,
                })}
              </Typography>
              <Typography variant="body1" component="div">
                {t("total_debts")}
              </Typography>
            </CardContent>
          </Card>
          <Card style={monthlySpendingsCardStyle}>
            <CardContent style={{ marginTop: "auto" }}>
              <Typography variant="h5">
                {summaryResponse?.totalMonthlySpendings.toLocaleString(
                  "cs-CZ",
                  {
                    style: "currency",
                    currency: "CZK",
                    minimumFractionDigits: 0,
                  }
                )}
              </Typography>
              <Typography variant="body1" component="div">
                {t("mandatory_monthly_spendings")}
              </Typography>
            </CardContent>
          </Card>
        </Stack>
      </Grid>
    </Grid>
  );
}
