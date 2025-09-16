import {
  Grid,
} from "@mui/material";
import { DebtsModule } from "./DebtsModule";

export default function DebtsPage() {
  return (
    <>
      <Grid container direction={"row"} spacing={2}>
        <DebtsModule enableEditing={true}/>
      </Grid>
    </>
  );
}
