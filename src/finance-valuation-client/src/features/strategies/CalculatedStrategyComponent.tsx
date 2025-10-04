import { useEffect, useState } from "react";
import { fetchCalculatedStrategy } from "./strategiesApi";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { IncomesResponseDto } from "../incomes/incomeModel";
import { fetchIncomes } from "../incomes/incomesApi";
import {
  Stack,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Button,
  AppBar,
  Dialog,
  IconButton,
  Toolbar,
  DialogContent,
  DialogActions,
} from "@mui/material";
import {
  CalculatedStrategyResponseDto,
  CalculationStepConfigurationDto,
} from "./strategyCalculatedModel";
import { getCalculationStepRowClassName, sx } from "./rowStyleSettings";
import CloseIcon from "@mui/icons-material/Close";

const columns: GridColDef<CalculationStepConfigurationDto>[] = [
  {
    field: "id",
    headerName: "ID",
    width: 90,
    sortable: false,
    editable: false,
    flex: 1,
  },
  {
    field: "name",
    headerName: "Name",
    width: 150,
    editable: false,
    sortable: false,
    flex: 1,
  },
  {
    field: "type",
    headerName: "Type",
    width: 150,
    editable: false,
    sortable: false,
    flex: 1,
  },
  {
    field: "monthlyActualContributionAmount",
    headerName: "Monthly Contribution (CZK)",
    type: "number",
    width: 180,
    editable: false,
    sortable: false,
    valueFormatter: (value: number) =>
      value?.toLocaleString("cs-CZ", {
        style: "currency",
        currency: "CZK",
        minimumFractionDigits: 0,
      }),
    flex: 1,
  },
  {
    field: "monthlyActualContributionPercentage",
    headerName: "Monthly Contribution (%)",
    type: "number",
    width: 180,
    editable: false,
    sortable: false,
    valueFormatter: (value: number) =>
      value?.toLocaleString("cs-CZ", {
        style: "percent",
        minimumFractionDigits: 0,
      }),
    flex: 1,
  },
];

export default function CalculatedStrategyComponent({
  strategyId,
  open,
  handleDialogClose,
}: {
  strategyId: number;
  open: boolean;
  handleDialogClose: () => void;
}) {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedIncomeId, setSelectedIncomeId] = useState<number | null>(null);
  const [incomesResponse, setIncomesResponse] =
    useState<IncomesResponseDto | null>(null);
  const [strategy, setStrategy] =
    useState<CalculatedStrategyResponseDto | null>(null);
  const [displayCalculatedStrategy, setDisplayCalculatedStrategy] =
    useState(false);

  useEffect(() => {
    fetchIncomes()
      .then((response) => {
        setIncomesResponse(response);
        setSelectedIncomeId(response.incomes[0]?.id || null);
      })
      .catch((err) => {
        setError(err.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  const handleCalculate = async () => {
    fetchCalculatedStrategy(strategyId, selectedIncomeId!)
      .then((response) => {
        setStrategy(response);
        setDisplayCalculatedStrategy(true);
      })
      .catch((err) => {
        setError(err.message);
      });
  };

  const totalContributionAmount =
    strategy?.strategyConfigurationsCalculationSteps.reduce(
      (sum, row) => sum + (row.monthlyActualContributionAmount || 0),
      0
    );
  const totalContributionPercentage =
    strategy?.strategyConfigurationsCalculationSteps.reduce(
      (sum, row) => sum + (row.monthlyActualContributionPercentage || 0),
      0
    );

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      <Dialog
        fullScreen
        open={open}
        // slots={{
        //   transition: Transition,
        // }}
      >
        <AppBar sx={{ position: "relative" }}>
          <Toolbar>
            <IconButton
              edge="start"
              color="inherit"
              onClick={handleDialogClose}
              aria-label="close"
            >
              <CloseIcon />
            </IconButton>
          </Toolbar>
        </AppBar>
        <DialogContent>
          <Stack direction={"column"} spacing={2}>
            <Stack direction={"row"} spacing={2}>
              <FormControl>
                <InputLabel id="income-select-label">Income</InputLabel>
                <Select
                  labelId="income-select-label"
                  id="income-select"
                  value={selectedIncomeId}
                  label="Income"
                  onChange={(e) =>
                    setSelectedIncomeId(e.target.value as number)
                  }
                >
                  {incomesResponse?.incomes.map((income) => (
                    <MenuItem key={income.id} value={income.id}>
                      {income.name} -{" "}
                      {new Date(income.date).toLocaleDateString("cs-CZ", {
                        month: "long",
                      })}{" "}
                      -{" "}
                      {income.amount.toLocaleString("cs-CZ", {
                        style: "currency",
                        currency: "CZK",
                      })}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
              <Button
                onClick={() => handleCalculate()}
                sx={[
                  (theme) => ({
                    color: theme.palette.text.primary,
                    backgroundColor: theme.palette.secondary.main,
                  }),
                ]}
              >
                Calculate
              </Button>
            </Stack>
            {displayCalculatedStrategy && (
              <>
                <DataGrid
                  rows={strategy?.strategyConfigurationsCalculationSteps}
                  columns={columns}
                  disableRowSelectionOnClick={false}
                  hideFooter={true}
                  getRowClassName={getCalculationStepRowClassName}
                  sx={sx}
                />
                <div
                  style={{
                    display: "flex",
                    justifyContent: "flex-end",
                    background: "#f5f5f5",
                    padding: "8px",
                    marginTop: "4px",
                    fontWeight: "bold",
                  }}
                >
                  <span style={{ minWidth: 150, textAlign: "right" }}>
                    Total Contribution (CZK):{" "}
                    {totalContributionAmount!.toLocaleString("cs-CZ", {
                      style: "currency",
                      currency: "CZK",
                      minimumFractionDigits: 0,
                    })}
                  </span>
                  <span
                    style={{
                      minWidth: 180,
                      textAlign: "right",
                      marginLeft: 32,
                    }}
                  >
                    Total Contribution (%):{" "}
                    {totalContributionPercentage!.toLocaleString("cs-CZ", {
                      style: "percent",
                      minimumFractionDigits: 0,
                    })}
                  </span>
                </div>
              </>
            )}
          </Stack>
        </DialogContent>
        <DialogActions>
          <Button
            onClick={() => handleDialogClose()}
            sx={[
              (theme) => ({
                color: theme.palette.text.primary,
              }),
            ]}
          >
            <CloseIcon
              sx={[
                (theme) => ({
                  color: theme.palette.text.primary,
                }),
              ]}
            />
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
}
