import {
  Divider,
  Grid,
} from "@mui/material";
import { DebtsModule } from "../debts/DebtsModule";
import { InvestmentsModule } from "../investments/InvestmentsModule";
import { SavingsModule } from "../savings/SavingsModule";
import { SummaryModule } from "../summary/SummaryModule";
import { SavingsLongevityModule } from "../savings-longevity/SavingsLongevityModule";

export default function DashboardPage() {
  return (
    <Grid container width={"100%"} spacing={2}>
      <Grid size={12}>
        <SummaryModule />
      </Grid>
      <Grid size={12}>
        <Divider />
      </Grid>
      <Grid container size={12} marginTop={2}>
        <DebtsModule key={"Debts"} enableEditing={false} />
        <InvestmentsModule key={"Investments"} enableEditing={false} />
        <SavingsModule key={"Savings"} enableEditing={false} />
      </Grid>
      <Grid size={12}>
        <Divider />
      </Grid>
      <Grid size={{ xs: 6, sm: 6, md: 4, lg: 4, xl: 4 }}>
        <SavingsLongevityModule />
      </Grid>
    </Grid>
  );
}
