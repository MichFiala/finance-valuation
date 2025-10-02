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
  DialogProps,
  DialogTitle,
  IconButton,
  MenuItem,
  Select,
  Stack,
  TextField,
  Tooltip,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { savingsColor } from "../savings/savingStylesSettings";
import { investmentColor } from "../investments/investmentStylesSettings";
import { spendingColor } from "../spendings/spendingStylesSettings";
import { debtColor } from "../debts/debtStylesSettings";
import ArrowDropUpIcon from "@mui/icons-material/ArrowDropUp";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";
import { DebtDto } from "../debts/debtModel";
import {
  createOrUpdate,
  deleteEntry,
  fetchEntries,
} from "../../shared/crudApi";
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
import ContentPasteGoIcon from "@mui/icons-material/ContentPasteGo";
import CreateIcon from "@mui/icons-material/Create";
import CheckIcon from "@mui/icons-material/Check";
export default function CreateOrUpdateStrategyComponent({
  existingStrategyId,
  open,
  handleDialogClose,
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
  const [jsonValue, setJsonValue] = useState<string | null>(null);
  const [openJsonDialog, setOpenJsonDialog] = useState<boolean>(false);
  
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

  const handleReferenceChange = (
    rowId: number,
    value: number,
    configurationType: StrategyConfigurationType
  ) => {
    let name: string;
    let monthlyContributionAmount: number | undefined;

    switch (configurationType) {
      case StrategyConfigurationType.Debt:
        const debt = debts.find((e) => e.id === value)!;
        name = debt.name;
        monthlyContributionAmount = debt.payment ?? undefined;
        break;
      case StrategyConfigurationType.Saving:
        const saving = savings.find((e) => e.id === value)!;
        name = saving.name;
        break;
      case StrategyConfigurationType.Investment:
        const investment = investments.find((e) => e.id === value)!;
        name = investment.name;
        break;
      case StrategyConfigurationType.Spending:
        const spending = spendings.find((e) => e.id === value)!;
        name = spending.name;
        monthlyContributionAmount = spending.amount;
        break;
    }
    setStrategy({
      ...strategy,
      strategyConfigurations: strategy.strategyConfigurations.map((r) =>
        r.id === rowId
          ? {
              ...r,
              referenceId: value,
              monthlyContributionAmount: monthlyContributionAmount,
              name: name,
            }
          : r
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
      editable: false,
      sortable: false,
      flex: 1,
    },
    {
      field: "referenceId",
      flex: 1,

      headerName: "Reference",
      renderCell: (params) => (
        <Select
          fullWidth
          value={params.row.referenceId || ""}
          label={params.row.type}
          onChange={(e) =>
            handleReferenceChange(
              params.row.id,
              e.target.value,
              params.row.type
            )
          }
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
      editable: true,
      sortable: false,
      valueFormatter: (value: number) =>
        value?.toLocaleString("cs-CZ", {
          style: "percent",
          minimumFractionDigits: 0,
        }),
      flex: 1,
    },
    {
      field: "action",
      headerName: "",
      renderCell: (props) => (
        <IconButton onClick={() => handleRowDelete(props.row.id)}>
          <DeleteIcon />
        </IconButton>
      ),
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
  };

  const handleRowDelete = (id: number) => {
    setStrategy({
      ...strategy,
      strategyConfigurations: [
        ...strategy.strategyConfigurations.filter(
          (configuration) => configuration.id !== id
        ),
      ],
    });
  };

  const handleSaveClick = () => {
    createOrUpdate(StrategiesEndpoint, { ...strategy });

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
    <>
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
        {existingStrategyId ? (
          <DialogTitle> Update {strategy.name} </DialogTitle>
        ) : (
          <DialogTitle> Create new</DialogTitle>
        )}
        <DialogContent>
          <Stack spacing={1}>
            <Stack direction={"row-reverse"} spacing={2}>
              <Tooltip title="Load from JSON">
                <IconButton
                  onClick={() => setOpenJsonDialog(true)}
                  sx={[
                    (theme) => ({
                      color: theme.palette.text.primary,
                      backgroundColor: theme.palette.primary.main,
                    }),
                  ]}
                >
                  <ContentPasteGoIcon />
                </IconButton>
              </Tooltip>
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
                onClick={() =>
                  handleAddClick(StrategyConfigurationType.Spending)
                }
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
                setStrategy((prev) => ({
                  ...prev,
                  strategyConfigurations: prev.strategyConfigurations.map(
                    (row) => (row.id === newRow.id ? newRow : row)
                  ),
                }));
                return newRow;
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
      <Dialog open={openJsonDialog} maxWidth={"xl"} fullWidth={true}>
        <DialogContent>
            <TextField 
              fullWidth={true}
              id="outlined-multiline-static"
              label="JSON"
              multiline
              rows={20}
              onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                setJsonValue(event.target.value);
              }}
            />
        </DialogContent>
        <DialogActions>
          <IconButton
            onClick={() => {
              if (!jsonValue) return;

              const strategyFromJson: StrategyDto = JSON.parse(jsonValue);

              setStrategy(strategyFromJson);
              setOpenJsonDialog(false);
            }}
          >
            <CheckIcon />
          </IconButton>
          <IconButton onClick={() => setOpenJsonDialog(false)}>
            <CloseIcon />
          </IconButton>
        </DialogActions>
      </Dialog>
    </>
  );
}
