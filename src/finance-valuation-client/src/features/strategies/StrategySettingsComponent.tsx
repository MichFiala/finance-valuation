import {
  StrategyConfigurationDto,
  StrategyDto,
} from "./strategyModel";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { getStrategyConfigurationRowClassName, sx } from "./rowStyleSettings";
import { Stack } from "@mui/material";

export default function StrategySettingsComponent({
  strategy,
}: {
  strategy: StrategyDto;
}) {

  const columns: GridColDef<StrategyConfigurationDto>[] = [
    {
      field: "id",
      headerName: "Id",
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

  return (
    <Stack spacing={1}>
      <DataGrid
        rows={strategy.strategyConfigurations}
        columns={columns}
        disableRowSelectionOnClick
        hideFooter={true}
        getRowClassName={getStrategyConfigurationRowClassName}
        sx={sx}
      />
    </Stack>
  );
}
