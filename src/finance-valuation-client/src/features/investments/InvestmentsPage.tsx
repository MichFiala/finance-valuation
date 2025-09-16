
import { Grid } from "@mui/material";
import { InvestmentsModule } from "./InvestmentsModule";

export default function InvestmentsPage() {
  return (
    <>
      <Grid container direction={"row"} spacing={2}>
        <InvestmentsModule enableEditing={true} />
      </Grid>
    </>
  );
}
