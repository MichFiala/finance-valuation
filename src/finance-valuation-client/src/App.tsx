import './App.css';
import DebtsPage from './features/debts/DebtsPage';
import Grid from '@mui/material/Grid';
import HomeIcon from '@mui/icons-material/Home';
import AccountBalanceWalletIcon from '@mui/icons-material/AccountBalanceWallet';
import TrendingUpIcon from '@mui/icons-material/TrendingUp';
import TrendingDownIcon from '@mui/icons-material/TrendingDown';
import AccountBalanceIcon from '@mui/icons-material/AccountBalance';
import SavingsIcon from '@mui/icons-material/Savings';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import { Avatar, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Stack, Switch } from '@mui/material';
import DashboardPage from './features/dashboard/DashboardPage';
import { useState } from 'react';
import StrategiesPage from './features/strategies/StrategiesPage';
import SavingsPage from './features/savings/SavingsPage';
import InvestmentsPage from './features/investments/InvestmentsPage';
import IncomesPage from './features/incomes/IncomesPage';
import SpendingsPage from './features/spendings/SpendingsPage';


function App() {
  const [selectedPage, setSelectedPage] = useState('dashboard');

  return (
    <Grid container className="App" height="100vh" width="100vw" spacing={1}>
      <Grid container size={2} justifyContent="stretch" style={{ backgroundColor: '#f0f0f0', padding: '20px' }}>
        <List>
           <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage('dashboard')}>
              <ListItemIcon>
                <HomeIcon />
              </ListItemIcon>
              <ListItemText primary="Dashboard" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage('debts')}>
              <ListItemIcon>
                <AccountBalanceIcon />
              </ListItemIcon>
              <ListItemText primary="Debts" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage('savings')}>
              <ListItemIcon>
                <SavingsIcon />
              </ListItemIcon>
              <ListItemText primary="Savings" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage('incomes')}>
              <ListItemIcon>
                <AccountBalanceWalletIcon />
              </ListItemIcon>
              <ListItemText primary="Incomes" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage('investments')}>
              <ListItemIcon>
                <TrendingUpIcon />
              </ListItemIcon>
              <ListItemText primary="Investments" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage('spendings')}>
              <ListItemIcon>
                <TrendingDownIcon />
              </ListItemIcon>
              <ListItemText primary="Spendings" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton onClick={() => setSelectedPage('strategies')}>
              <ListItemIcon>
                <AccountTreeIcon />
              </ListItemIcon>
              <ListItemText primary="Strategies" />
            </ListItemButton>
          </ListItem>
        </List>
      </Grid>
      <Grid container size={10} style={{ backgroundColor: '#f0f0f0', padding: '20px' }}>
        <Stack width={"100%"}spacing={2}>
          <Stack direction={"row-reverse"} width={"100%"} spacing={2} justifyContent={"flex-start"}>
            <Avatar alt="Upload new avatar"/>
            <Switch  defaultChecked />
          </Stack>
          {selectedPage === 'dashboard' && <DashboardPage />}
          {selectedPage === 'debts' && <DebtsPage />}
          {selectedPage === 'savings' && <SavingsPage />}
          {selectedPage === 'incomes' && <IncomesPage />}
          {selectedPage === 'investments' && <InvestmentsPage />}
          {selectedPage === 'spendings' && <SpendingsPage />}
          {selectedPage === 'strategies' && <StrategiesPage />}
        </Stack>
      </Grid>
    </Grid>
  );
}

export default App;
