import { Box, Button, Grid, Stack, Typography } from "@mui/material";
import GoogleIcon from "@mui/icons-material/Google";
import { redirectGoogleLogin } from "./userApi";
import GoogleSignInButton from "./GoogleSignInButton";

export function LoginPage() {
  return (
    <Box
      sx={[
        (theme) => ({
          display: "flex",
          bgcolor: "background.default",
          minWidth: "100vw",
          minHeight: "100vh",
          textAlign: "center",
          alignItems: "center",
          alignContent: "center",
          color: theme.palette.text.primary,
        }),
      ]}
    >
      <Grid
        width={"100%"}
        container
        sx={{
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Grid
          size={2}
          sx={[
            (theme) => ({
              borderRadius: 2,
              minHeight: 200,
            }),
          ]}
        >
          <Stack>
            <Typography
              sx={[
                (theme) => ({
                  color: theme.palette.text.primary,
                }),
              ]}
            >
              Login with:
            </Typography>
            <GoogleSignInButton onClick={redirectGoogleLogin} disabled={false} />
          </Stack>
        </Grid>
      </Grid>
    </Box>
  );
}
