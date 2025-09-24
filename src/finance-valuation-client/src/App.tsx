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
import {
  Avatar,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Stack,
  Switch,
  Tooltip,
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

function App() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedPage, setSelectedPage] = useState("dashboard");
  const [user, setUser] = useState<User | null>(null);
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

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <Grid container className="App" height="100vh" width="100vw" spacing={1}>
      <Grid
        container
        size={2}
        justifyContent="stretch"
        style={{ backgroundColor: "#f0f0f0", padding: "20px" }}
      >
        <List>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage("dashboard")}>
              <ListItemIcon>
                <HomeIcon />
              </ListItemIcon>
              <ListItemText primary="Dashboard" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage("debts")}>
              <ListItemIcon>
                <AccountBalanceIcon />
              </ListItemIcon>
              <ListItemText primary="Debts" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage("savings")}>
              <ListItemIcon>
                <SavingsIcon />
              </ListItemIcon>
              <ListItemText primary="Savings" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage("incomes")}>
              <ListItemIcon>
                <AccountBalanceWalletIcon />
              </ListItemIcon>
              <ListItemText primary="Incomes" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage("investments")}>
              <ListItemIcon>
                <TrendingUpIcon />
              </ListItemIcon>
              <ListItemText primary="Investments" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage("spendings")}>
              <ListItemIcon>
                <TrendingDownIcon />
              </ListItemIcon>
              <ListItemText primary="Spendings" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage("strategies")}>
              <ListItemIcon>
                <AccountTreeIcon />
              </ListItemIcon>
              <ListItemText primary="Strategies" />
            </ListItemButton>
          </ListItem>
        </List>
      </Grid>
      <Grid
        container
        size={10}
        style={{ backgroundColor: "#f0f0f0", padding: "20px" }}
      >
        <Stack width={"100%"} spacing={2}>
          {user && (
            <Stack
              direction={"row-reverse"}
              width={"100%"}
              spacing={2}
              justifyContent={"flex-start"}
            >
              <Tooltip title={user?.userName}>
                {user?.image ? (
                  <Avatar alt="Upload new avatar" src={user.image} />
                ) : (
                  <AccountCircleIcon fontSize={"large"} />
                )}
              </Tooltip>
              <Switch defaultChecked />
            </Stack>
          )}
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
  );
}

export default App;
