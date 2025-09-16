import { useEffect, useState } from "react";
import {
  Button,
  Typography,
} from "@mui/material";
import {
  StrategyDto,
} from "./strategyModel";
import { fetchStrategy } from "./strategiesApi";
import SettingsIcon from "@mui/icons-material/Settings";
import CalculatedStrategyComponent from "./CalculatedStrategyComponent";
import StrategySettingsComponent from "./StrategySettingsComponent";


export default function StrategiesPage() {
  const [strategyResponse, setStrategyResponse] = useState<StrategyDto | null>(
    null
  );
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [showSettings, setShowSettings] = useState(false);

  useEffect(() => {
    fetchStrategy()
      .then((strategyResponse) => {
        setStrategyResponse(strategyResponse);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setLoading(false);
      });
  }, []);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      <Typography variant="h4">
        {strategyResponse!.name}
        <Button onClick={() => setShowSettings((prev) => !prev)}>
          <SettingsIcon />
        </Button>
      </Typography>
      {showSettings && <StrategySettingsComponent strategyResponse={strategyResponse!} />}
      <CalculatedStrategyComponent />
    </>
  );
}
