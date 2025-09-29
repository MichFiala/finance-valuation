import { useEffect, useState } from "react";
import {
  StrategyConfigurationDto,
  StrategyConfigurationType,
  StrategyDto,
} from "./strategyModel";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { arrayMove } from "@dnd-kit/sortable";
import { getStrategyConfigurationRowClassName, sx } from "./rowStyleSettings";
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
  MenuItem,
  Select,
  Stack,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { savingsColor } from "../savings/savingStylesSettings";
import { investmentColor } from "../investments/investmentStylesSettings";
import { spendingColor } from "../spendings/spendingStylesSettings";
import { debtColor } from "../debts/debtStylesSettings";
import ArrowDropUpIcon from "@mui/icons-material/ArrowDropUp";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";
import { DebtDto } from "../debts/debtModel";
import { create, deleteEntry, fetchEntries } from "../../shared/crudApi";
import { DebtsEndpoint } from "../debts/debtApi";
import { SavingsEndpoint } from "../savings/savingsApi";
import { SavingsDto } from "../savings/savingsModel";
import { SpendingsEndpoint } from "../spendings/spendingsApi";
import { SpendingsDto } from "../spendings/spendingsModel";
import { fetchStrategy, StrategiesEndpoint } from "./strategiesApi";
import SaveAltIcon from "@mui/icons-material/SaveAlt";
import CloseIcon from "@mui/icons-material/Close";
import { InvestmentsEndpoint } from "../investments/investmentsApi";
import { InvestmentDto } from "../investments/investmentModel";
import DeleteIcon from "@mui/icons-material/Delete";

export default function CreateOrUpdateStrategyComponent({
  existingStrategyId,
  open,
  handleDialogClose
}: {
  existingStrategyId?: number | null;
  open: boolean;
  handleDialogClose: (refresh: boolean) => void;
}) {
  const [debts, setDebts] = useState<DebtDto[]>([]);
  const [savings, setSavings] = useState<SavingsDto[]>([]);
  const [spendings, setSpendings] = useState<SpendingsDto[]>([]);
  const [investments, setInvestments] = useState<InvestmentDto[]>([]);

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [strategy, setStrategy] = useState<StrategyDto>({} as StrategyDto);
  // const [strategyConfigurations, setStrategyConfigurations] = useState<StrategyConfigurationDto[]>([]);

  const fetchData = async () => {
    setLoading(true);
    const newStrategy = {
      id: -1,
      name: "New Strategy",
      strategyConfigurations: [],
    } as StrategyDto;
    const [
      debtsResponse,
      savingsResponse,
      spendingsRespone,
      investmentsResponse,
      strategyResponse,
    ] = await Promise.all([
      fetchEntries(DebtsEndpoint),
      fetchEntries(SavingsEndpoint),
      fetchEntries(SpendingsEndpoint),
      fetchEntries(InvestmentsEndpoint),
      existingStrategyId ? fetchStrategy(existingStrategyId) : null,
    ]);

    setDebts(debtsResponse.debts);
    setSavings(savingsResponse.savings);
    setSpendings(spendingsRespone.spendings);
    setInvestments(investmentsResponse.investments);
    console.log(strategyResponse);
    strategyResponse ? setStrategy(strategyResponse) : setStrategy(newStrategy);
  };

  useEffect(() => {
    fetchData()
      .catch((err) => {
        setError(err.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [existingStrategyId]);

  const handleReferenceChange = (rowId: number, value: any) => {
    setStrategy({
      ...strategy,
      strategyConfigurations: strategy.strategyConfigurations.map((r) =>
        r.id === rowId ? { ...r, referenceId: value } : r
      ),
    });
  };

  const columns: GridColDef<StrategyConfigurationDto>[] = [
    {
      field: "orderActions",
      headerName: "Order",
      width: 50,
      sortable: false,
      renderCell: (params) => (
        <Stack direction={"column"}>
          <IconButton
            onClick={() => handleMoveRow(params.row.id, -1)}
            size={"small"}
            sx={{ maxWidth: 25, maxHeight: 25 }}
          >
            <ArrowDropUpIcon />
          </IconButton>
          <IconButton
            onClick={() => handleMoveRow(params.row.id, 1)}
            size={"small"}
            sx={{ maxWidth: 25, maxHeight: 25 }}
          >
            <ArrowDropDownIcon />
          </IconButton>
        </Stack>
      ),
    },
    {
      field: "name",
      headerName: "Name",
      width: 150,
      editable: true,
      sortable: false,
      flex: 1,
    },
    {
      field: "referenceId",
      width: 150,
      headerName: "Reference",
      renderCell: (params) => (
        <Select
          fullWidth
          value={params.row.referenceId || ""}
          label={params.row.type}
          onChange={(e) => handleReferenceChange(params.row.id, e.target.value)}
        >
          {params.row.type === StrategyConfigurationType.Debt &&
            debts.map((debt) => (
              <MenuItem value={debt.id}>{debt.name}</MenuItem>
            ))}
          {params.row.type === StrategyConfigurationType.Saving &&
            savings.map((saving) => (
              <MenuItem value={saving.id}>{saving.name}</MenuItem>
            ))}
          {params.row.type === StrategyConfigurationType.Spending &&
            spendings.map((spending) => (
              <MenuItem value={spending.id}>{spending.name}</MenuItem>
            ))}
          {params.row.type === StrategyConfigurationType.Investment &&
            investments.map((investment) => (
              <MenuItem value={investment.id}>{investment.name}</MenuItem>
            ))}
        </Select>
      ),
    },
    {
      field: "type",
      headerName: "Type",
      width: 150,
      editable: true,
      sortable: false,
      flex: 1,
    },
    {
      field: "monthlyContributionAmount",
      headerName: "Monthly Contribution (CZK)",
      type: "number",
      width: 180,
      editable: true,
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
      field: "monthlyContributionPercentage",
      headerName: "Monthly Contribution (%)",
      type: "number",
      width: 180,
      editable: true,
      sortable: false,
      valueFormatter: (value: number) =>
        value?.toLocaleString("cs-CZ", {
          style: "percent",
          minimumFractionDigits: 0,
        }),
      flex: 1,
    },
  ];

  const handleMoveRow = (id: number, direction: number) => {
    const oldIndex = strategy.strategyConfigurations.findIndex(
      (r) => r.id === id
    );
    const changedOrderConfigurations = arrayMove(
      strategy.strategyConfigurations,
      oldIndex,
      oldIndex + direction
    );

    setStrategy({
      ...strategy,
      strategyConfigurations: [...changedOrderConfigurations],
    });
    // setStrategy({...strategy, strategyConfigurations: strategy.strategyConfigurations(prev) => {
    //   const oldIndex = prev.findIndex((r) => r.id === id);
    //   return arrayMove(prev, oldIndex, oldIndex + direction);
    // })};
  };

  const handleSaveClick = () => {
    create(StrategiesEndpoint, { ...strategy });

    handleDialogClose(true);
  };

  const handleDeleteClick = () => {
    if (strategy.id > 0) {
      deleteEntry(StrategiesEndpoint, strategy.id);
      handleDialogClose(true);
    }
  };
  const handleAddClick = (configurationType: StrategyConfigurationType) => {
    const minId =
      strategy.strategyConfigurations.length > 0
        ? Math.min(...strategy.strategyConfigurations.map((row) => row.id))
        : 0;

    setStrategy({
      ...strategy,
      strategyConfigurations: [
        ...strategy.strategyConfigurations,
        {
          id: minId - 1,
          name: `New ${configurationType}`,
          type: configurationType,
        } as StrategyConfigurationDto,
      ],
    });
  };

  return (
    <Dialog
      fullScreen
      open={open}
      onClose={() => ""}
      sx={[
        (theme) => ({
          color: theme.palette.text.primary,
        }),
      ]}
    >
      {existingStrategyId ? <DialogTitle> Update {strategy.name} </DialogTitle> : <DialogTitle> Create new</DialogTitle>}
      <DialogContent>
        <Stack spacing={1}>
          <Stack direction={"row-reverse"} spacing={2}>
            <Button
              onClick={() => handleAddClick(StrategyConfigurationType.Debt)}
              sx={[
                (theme) => ({
                  color: theme.palette.text.primary,
                  backgroundColor: debtColor,
                }),
              ]}
            >
              <AddIcon
                sx={[
                  (theme) => ({
                    color: theme.palette.text.primary,
                  }),
                ]}
              />
            </Button>
            <Button
              onClick={() => handleAddClick(StrategyConfigurationType.Spending)}
              sx={[
                (theme) => ({
                  color: theme.palette.text.primary,
                  backgroundColor: spendingColor,
                }),
              ]}
            >
              <AddIcon
                sx={[
                  (theme) => ({
                    color: theme.palette.text.primary,
                  }),
                ]}
              />
            </Button>
            <Button
              onClick={() =>
                handleAddClick(StrategyConfigurationType.Investment)
              }
              sx={[
                (theme) => ({
                  color: theme.palette.text.primary,
                  backgroundColor: investmentColor,
                }),
              ]}
            >
              <AddIcon
                sx={[
                  (theme) => ({
                    color: theme.palette.text.primary,
                  }),
                ]}
              />
            </Button>
            <Button
              onClick={() => handleAddClick(StrategyConfigurationType.Saving)}
              sx={[
                (theme) => ({
                  color: theme.palette.text.primary,
                  backgroundColor: savingsColor,
                }),
              ]}
            >
              <AddIcon
                sx={[
                  (theme) => ({
                    color: theme.palette.text.primary,
                  }),
                ]}
              />
            </Button>
          </Stack>

          <DataGrid
            rows={strategy.strategyConfigurations}
            columns={columns}
            disableRowSelectionOnClick
            hideFooter={true}
            processRowUpdate={(newRow) => {
              // nahraď starý řádek novým
              setStrategy((prev) => ({
                ...prev,
                strategyConfigurations: prev.strategyConfigurations.map((row) =>
                  row.id === newRow.id ? newRow : row
                ),
              }));
              return newRow; // důležité pro MUI, jinak UI rollbackne
            }}
            getRowClassName={getStrategyConfigurationRowClassName}
            sx={sx}
          />
        </Stack>
      </DialogContent>
      <DialogActions>
        {existingStrategyId && (
          <Button
            size="small"
            onClick={() => handleDeleteClick()}
            style={{ color: "red" }}
          >
            <DeleteIcon />
          </Button>
        )}
        <Button
          onClick={handleSaveClick}
          type="submit"
          form="subscription-form"
          sx={[
            (theme) => ({
              color: "green",
            }),
          ]}
        >
          <SaveAltIcon />
        </Button>
        <Button
          onClick={() => handleDialogClose(false)}
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
  );
}
