import { Grid } from "@mui/material";
import { SavingsModule } from "./SavingsModule";

export default function SavingsPage() {
  return (
    <>
      <Grid container direction={"row"} spacing={2}>
        <SavingsModule enableEditing={true} />
      </Grid>
    </>
  );
}
