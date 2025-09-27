import { Grid, Snackbar, SnackbarCloseReason } from "@mui/material";
import { DebtsModule } from "./DebtsModule";
import { useState } from "react";

export default function DebtsPage() {
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState("");

  const handleOpenSnackBar = (message: string) => {
    setSnackbarMessage(message)
    setOpenSnackbar(true);
  }

  const handleClose = (
    event: React.SyntheticEvent | Event,
    reason?: SnackbarCloseReason,
  ) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpenSnackbar(false);
  };

  return (
    <>
      <Grid container direction={"row"} spacing={2}>
        <DebtsModule enableEditing={true} handleOpenSnackBar={handleOpenSnackBar}/>
      </Grid>
      <Snackbar
        open={openSnackbar}
        autoHideDuration={2000}
        onClose={handleClose}
        message={snackbarMessage}
      />
    </>
  );
}
