import { useEffect, useState } from "react";
import { IncomeDto } from "./incomeModel";
import {
  deleteIncome,
  fetchIncomes,
  IncomesEndpoint,
  toUtcDateOnlyString,
} from "./incomesApi";
import {
  DataGrid,
  GridActionsCellItem,
  GridColDef,
  GridRowId,
} from "@mui/x-data-grid";
import { Button, Stack } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import { create, update } from "../../shared/crudApi";

export default function IncomesPage() {
  const [incomes, setIncomes] = useState<IncomeDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [reloadCounter, setReloadCounter] = useState(0);

  useEffect(() => {
    fetchIncomes()
      .then((response) => {
        response.incomes.forEach(
          (income) => (income.date = new Date(income.date!))
        );
        setIncomes(response.incomes);
      })
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, [reloadCounter]);

  const columns: GridColDef<IncomeDto>[] = [
    {
      field: "date",
      headerName: "Income Date",
      type: "date",
      editable: true,
    },
    {
      field: "name",
      headerName: "Name",
      editable: true,
      flex: 1,
    },
    {
      field: "amount",
      headerName: "Amount",
      type: "number",
      valueFormatter: (value: number) =>
        value?.toLocaleString("cs-CZ", {
          style: "currency",
          currency: "CZK",
          minimumFractionDigits: 0,
        }),
      editable: true,
      flex: 1,
    },
    {
      field: "isMainIncome",
      headerName: "Is Main Income",
      type: "boolean",
      editable: true,
      width: 150,
    },
    {
      field: "actions",
      type: "actions",
      headerName: "Actions",
      width: 100,
      getActions: (params) => [
        <GridActionsCellItem
          icon={<DeleteIcon style={{ color: "red" }} />}
          label="Delete"
          onClick={() => handleDeleteRow(params.id)}
          color="inherit"
        />,
      ],
    },
  ];
  const handleDeleteRow = (gridRowId: GridRowId) => {
    const id = gridRowId as number;
    if (id > 0) {
      deleteIncome(id).then(() => setReloadCounter(reloadCounter + 1));
    } else {
      setIncomes([...incomes.filter((i) => i.id !== id)]);
    }
  };
  const handleCreateOrUpdateRow = async (
    newRow: IncomeDto,
    oldRow: IncomeDto
  ) => {
    if (newRow.id < 0) {
      await create(IncomesEndpoint, {
        name: newRow.name,
        amount: newRow.amount,
        date: toUtcDateOnlyString(newRow.date),
        isMainIncome: newRow.isMainIncome,
      });
    } else {
      await update(IncomesEndpoint, newRow.id, {
        name: newRow.name,
        amount: newRow.amount,
        date: toUtcDateOnlyString(newRow.date),
        isMainIncome: newRow.isMainIncome,
      });
    }
    setReloadCounter(reloadCounter + 1);
    return newRow;
  };

  const handleCreateEmptyIncome = () => {
    const minId =
      incomes.length > 0 ? Math.min(...incomes.map((income) => income.id)) : 0;
    const emptyIncome = {
      id: minId > 0 ? -1 : minId - 1,
      name: "New Income",
      amount: 0,
      date: new Date(),
      isMainIncome: true,
    } as IncomeDto;

    setIncomes([...incomes, emptyIncome]);
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      <Stack direction={"column"} spacing={2}>
        <Stack
          direction={"row-reverse"}
          width={"100%"}
          spacing={2}
          justifyContent={"flex-start"}
        >
          <Button
            onClick={handleCreateEmptyIncome}
            style={{ backgroundColor: "#425E6A", color: "white" }}
          >
            <AddIcon />
          </Button>
        </Stack>
        <DataGrid
          rows={incomes}
          columns={columns}
          hideFooter={true}
          processRowUpdate={handleCreateOrUpdateRow}
          onProcessRowUpdateError={(error) => console.log(error)}
        />
      </Stack>
    </>
  );
}
