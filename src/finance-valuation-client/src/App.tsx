import "./App.css";
import DebtsPage from "./features/debts/DebtsPage";
import Grid from "@mui/material/Grid";
import HomeIcon from "@mui/icons-material/Home";
import AccountBalanceWalletIcon from "@mui/icons-material/AccountBalanceWallet";
import TrendingUpIcon from "@mui/icons-material/TrendingUp";
import TrendingDownIcon from "@mui/icons-material/TrendingDown";
import AccountBalanceIcon from "@mui/icons-material/AccountBalance";
import SavingsIcon from "@mui/icons-material/Savings";
import AccountTreeIcon from "@mui/icons-material/AccountTree";
import BedtimeIcon from "@mui/icons-material/Bedtime";
import SunnyIcon from "@mui/icons-material/Sunny";
import {
  Avatar,
  Box,
  CircularProgress,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  MenuItem,
  Select,
  Stack,
  Tooltip,
  Typography,
  useColorScheme,
} from "@mui/material";
import DashboardPage from "./features/dashboard/DashboardPage";
import { useEffect, useState } from "react";
import StrategiesPage from "./features/strategies/StrategiesPage";
import SavingsPage from "./features/savings/SavingsPage";
import InvestmentsPage from "./features/investments/InvestmentsPage";
import IncomesPage from "./features/incomes/IncomesPage";
import SpendingsPage from "./features/spendings/SpendingsPage";
import { fetchMe } from "./features/user/userApi";
import { User } from "./features/user/userModel";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { useTranslation } from "react-i18next";
import i18n, { supportedLngs } from "./i18n/config";

function App() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedPage, setSelectedPage] = useState("dashboard");
  const [user, setUser] = useState<User | null>(null);
  const { mode, setMode } = useColorScheme();
  const { t } = useTranslation();

  useEffect(() => {
    fetchMe()
      .then((response) => {
        setUser(response);
      })
      .catch((err) => {
        setError(err.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  if (error) return <div>Error: {error}</div>;

  return (
    <>
      {loading && (
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
          <Grid width={"100%"}>
            <Typography typography={"h6"}>Loading Application</Typography>
            <CircularProgress
              sx={[
                (theme) => ({
                  color: theme.palette.text.primary,
                  padding: 5,
                }),
              ]}
            />
          </Grid>
        </Box>
      )}
      {!loading && (
        <Box
          sx={{
            display: "flex",
            bgcolor: "background.default",
            minWidth: "100vw",
            minHeight: "100vh",
            textAlign: "center",
            alignContent: "center",
          }}
        >
          (
          <Stack width={"100%"} padding={5} spacing={1}>
            <Grid container>
              <Grid size={1} textAlign={'center'} alignContent={'center'} alignItems={'center'}>
                <Avatar alt="Logo" src="/logo.png" style={{paddingLeft: 15}}/>
              </Grid>
              <Grid size={11}>
                <Stack
                  direction={"row-reverse"}
                  width={"100%"}
                  spacing={1}
                  textAlign={"center"}
                  alignContent={"center"}
                >
                  {user && (
                    <Tooltip title={user?.userName}>
                      {user?.image ? (
                        <Avatar alt="Users avatar" src={user.image} />
                      ) : (
                        <AccountCircleIcon fontSize={"large"} />
                      )}
                    </Tooltip>
                  )}
                  <IconButton
                    onClick={() =>
                      mode === "dark" ? setMode("light") : setMode("dark")
                    }
                    sx={[(theme) => ({ color: theme.palette.text.primary })]}
                  >
                    {mode === "dark" ? <SunnyIcon /> : <BedtimeIcon />}
                  </IconButton>
                  <Select
                    value={i18n.resolvedLanguage}
                    label="Age"
                    onChange={(e) => i18n.changeLanguage(e.target.value)}
                  >
                    {Object.entries(supportedLngs).map(([code, name]) => (
                      <MenuItem value={code} key={code}>
                        {name}
                      </MenuItem>
                    ))}
                  </Select>
                </Stack>
              </Grid>
            </Grid>
            <Grid container className="App">
              <Grid
                container
                size={{ xs: 12, sm: 12, md: 3, lg: 2, xl: 1.5 }}
                justifyContent="stretch"
              >
                <List
                  sx={[
                    (theme) => ({
                      color: theme.palette.text.primary,
                    }),
                  ]}
                >
                  <ListItem disablePadding>
                    <ListItemButton
                      onClick={() => setSelectedPage("dashboard")}
                    >
                      <ListItemIcon>
                        <HomeIcon />
                      </ListItemIcon>
                      <ListItemText primary={t("dashboard")} />
                    </ListItemButton>
                  </ListItem>
                  <ListItem disablePadding>
                    <ListItemButton onClick={() => setSelectedPage("debts")}>
                      <ListItemIcon>
                        <AccountBalanceIcon />
                      </ListItemIcon>
                      <ListItemText primary={t("debts")} />
                    </ListItemButton>
                  </ListItem>
                  <ListItem disablePadding>
                    <ListItemButton onClick={() => setSelectedPage("savings")}>
                      <ListItemIcon>
                        <SavingsIcon />
                      </ListItemIcon>
                      <ListItemText primary={t("savings")} />
                    </ListItemButton>
                  </ListItem>
                  <ListItem disablePadding>
                    <ListItemButton onClick={() => setSelectedPage("incomes")}>
                      <ListItemIcon>
                        <AccountBalanceWalletIcon />
                      </ListItemIcon>
                      <ListItemText primary={t("incomes")} />
                    </ListItemButton>
                  </ListItem>
                  <ListItem disablePadding>
                    <ListItemButton
                      onClick={() => setSelectedPage("investments")}
                    >
                      <ListItemIcon>
                        <TrendingUpIcon />
                      </ListItemIcon>
                      <ListItemText primary={t("investments")} />
                    </ListItemButton>
                  </ListItem>
                  <ListItem disablePadding>
                    <ListItemButton
                      onClick={() => setSelectedPage("spendings")}
                    >
                      <ListItemIcon>
                        <TrendingDownIcon />
                      </ListItemIcon>
                      <ListItemText primary={t("spendings")} />
                    </ListItemButton>
                  </ListItem>
                  <ListItem disablePadding>
                    <ListItemButton
                      onClick={() => setSelectedPage("strategies")}
                    >
                      <ListItemIcon>
                        <AccountTreeIcon />
                      </ListItemIcon>
                      <ListItemText primary={t("strategies")} />
                    </ListItemButton>
                  </ListItem>
                </List>
              </Grid>
              <Grid
                container
                size={{ xs: 12, sm: 12, md: 9, lg: 10, xl: 10.5 }}
                sx={[
                  (theme) => ({
                    color: theme.palette.text.secondary,
                  }),
                ]}
              >
                <Stack width={"100%"} spacing={2}>
                  {selectedPage === "dashboard" && <DashboardPage />}
                  {selectedPage === "debts" && <DebtsPage />}
                  {selectedPage === "savings" && <SavingsPage />}
                  {selectedPage === "incomes" && <IncomesPage />}
                  {selectedPage === "investments" && <InvestmentsPage />}
                  {selectedPage === "spendings" && <SpendingsPage />}
                  {selectedPage === "strategies" && <StrategiesPage />}
                </Stack>
              </Grid>
            </Grid>
          </Stack>
          )
        </Box>
      )}
    </>
  );
}

export default App;
