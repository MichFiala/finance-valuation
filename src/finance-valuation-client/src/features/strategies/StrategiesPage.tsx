import { useEffect, useState } from "react";
import {
  alpha,
  Card,
  CardActions,
  CardContent,
  CardHeader,
  Grid,
  IconButton,
} from "@mui/material";
import { StrategyDto } from "./strategyModel";
import { fetchStrategy, StrategiesEndpoint } from "./strategiesApi";
import { fetchEntries } from "../../shared/crudApi";
import AddIcon from "@mui/icons-material/Add";
import { Stack } from "@mui/material";
import CreateOrUpdateStrategyComponent from "./CreateOrUpdateStrategyComponent";
import SettingsIcon from "@mui/icons-material/Settings";
import CalculatedStrategyComponent from "./CalculatedStrategyComponent";
import AltRouteIcon from "@mui/icons-material/AltRoute";
import ContentCopyIcon from "@mui/icons-material/ContentCopy";
const StrategyColor = "#2397a7ff";

export default function StrategiesPage() {
  const [strategies, setStrategies] = useState<StrategyDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [showSettings, setShowSettings] = useState(false);
  const [selectedStrategy, setSelectedStrategy] = useState<StrategyDto | null>(
    null
  );
  const [open, setOpen] = useState<boolean>(false);
  const [calculatedOpen, setCalculatedOpen] = useState<boolean>(false);
  const [reloadCounter, setReloadCounter] = useState(0);

  const fetchData = async () => {
    setLoading(true);
    const [strategiesResponse] = await Promise.all([
      fetchEntries(StrategiesEndpoint),
    ]);

    setStrategies(strategiesResponse.strategies);
  };

  useEffect(() => {
    fetchData()
      .catch((err) => {
        setError(err.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [reloadCounter]);

  const handleEditClick = (strategy: StrategyDto) => {
    setSelectedStrategy(strategy);
    setOpen(true);
  };
  const handleDialogClose = (refresh: boolean) => {
    setOpen(false);
    setSelectedStrategy(null);
    if (refresh) setReloadCounter(reloadCounter + 1);
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      <Grid container spacing={2}>
        {strategies.map((strategy) => (
          <Grid size={{ xs: 12, sm: 6, md: 2, lg: 2, xl: 2 }}>
            <Card
              key={strategy.id}
              sx={[
                (theme) => ({
                  backgroundColor: theme.palette.secondary.main,
                  color: theme.palette.text.primary,
                  border: 1,
                  borderColor: StrategyColor,
                }),
              ]}
            >
              <CardHeader
                title={strategy.name}
                style={{
                  background: StrategyColor,
                  color: "white",
                }}
              ></CardHeader>
              <CardContent></CardContent>
              <CardActions
                sx={[
                  (theme) => ({
                    mt: "auto",
                    justifyContent: "center",
                  }),
                ]}
                style={{
                  backgroundColor: alpha(StrategyColor, 0.7),
                  paddingTop: "auto",
                }}
              >
                <Stack
                  direction={"row-reverse"}
                  width={"100%"}
                  justifyContent={"flex-start"}
                >
                  <IconButton
                    size="small"
                    onClick={() => handleEditClick(strategy)}
                  >
                    <SettingsIcon
                      sx={[
                        (theme) => ({
                          color: theme.palette.text.primary,
                        }),
                      ]}
                    />
                  </IconButton>
                  <IconButton
                    onClick={() => {
                      setCalculatedOpen(true);
                      setSelectedStrategy(strategy);
                    }}
                  >
                    <AltRouteIcon />
                  </IconButton>
                  <IconButton
                    onClick={() => {
                      fetchStrategy(strategy.id).then((data) => {
                        const { id, ...rest } = data;

                        navigator.clipboard.writeText(JSON.stringify(rest));
                      });
                    }}
                  >
                    <ContentCopyIcon />
                  </IconButton>
                </Stack>
              </CardActions>
            </Card>
          </Grid>
        ))}
        <Grid
          textAlign={"left"}
          alignContent={"start"}
          size={{ xs: 12, sm: 6, md: 3, lg: 3, xl: 3 }}
        >
          <IconButton
            size="large"
            sx={[
              (theme) => ({
                color: "black",
                backgroundColor: StrategyColor,
                border: "1px solid",
                borderRadius: "8px",
              }),
            ]}
            onClick={() => {
              setOpen(true);
            }}
          >
            <AddIcon />
          </IconButton>
        </Grid>
      </Grid>
      <CreateOrUpdateStrategyComponent
        existingStrategyId={selectedStrategy?.id}
        handleDialogClose={handleDialogClose}
        open={open}
      />
      <CalculatedStrategyComponent
        key={selectedStrategy?.id}
        strategyId={selectedStrategy?.id!}
        open={calculatedOpen}
        handleDialogClose={() => {
          setCalculatedOpen(false);
          setSelectedStrategy(null);
        }}
      />
    </>
  );
}
