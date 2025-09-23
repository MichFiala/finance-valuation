import { useEffect, useState } from "react";
import {
  StrategyConfigurationDto,
  StrategyDto,
} from "./strategyModel";
import {
  DataGrid,
  GridColDef,
  GridRow,
  GridRowProps,
} from "@mui/x-data-grid";
import {
  DndContext,
  PointerSensor,
  closestCenter,
  useSensor,
  useSensors,
  DragEndEvent,
} from "@dnd-kit/core";
import {
  SortableContext,
  verticalListSortingStrategy,
  useSortable,
  arrayMove,
} from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import DragIndicatorIcon from "@mui/icons-material/DragIndicator";
import { getStrategyConfigurationRowClassName, sx } from "./rowStyleSettings";

const columns: GridColDef<StrategyConfigurationDto>[] = [
  {
    field: "drag",
    headerName: "",
    width: 50,
    sortable: false,
    renderCell: () => (
      <DragIndicatorIcon sx={{ cursor: "grab", color: "gray" }} />
    ),
  },
  {
    field: "id",
    headerName: "ID",
    width: 90,
    sortable: false,
    flex: 1,
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

function DraggableRow(props: GridRowProps) {
  const { rowId } = props;
  const {
    attributes,
    listeners,
    setNodeRef,
    transform,
    transition,
    isDragging,
  } = useSortable({ id: rowId });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
    opacity: isDragging ? 0.5 : 1,
    cursor: "grab",
  };

  return (
    <GridRow
      ref={setNodeRef}
      style={style}
      {...props}
      {...attributes}
      {...listeners}
    />
  );
}


export default function StrategySettingsComponent({strategyResponse}: {strategyResponse: StrategyDto}) {
  const [rows, setRows] = useState<StrategyConfigurationDto[]>([]);

  const sensors = useSensors(useSensor(PointerSensor));

  const handleDragEnd = (event: DragEndEvent) => {
    const { active, over } = event;
    if (!over || active.id === over.id) return;

    setRows((prev) => {
      const oldIndex = prev.findIndex((r) => r.id === active.id);
      const newIndex = prev.findIndex((r) => r.id === over.id);
      return arrayMove(prev, oldIndex, newIndex);
    });
  };

  useEffect(() => {
      setRows(strategyResponse.strategyConfigurations || []);
  }, [strategyResponse]);

  return (
        <DndContext
          sensors={sensors}
          collisionDetection={closestCenter}
          onDragEnd={handleDragEnd}
        >
          <SortableContext
            items={rows.map((r) => r.id)}
            strategy={verticalListSortingStrategy}
          >
            <div style={{ display: "flex", flexDirection: "column" }}>
              <DataGrid
                rows={rows}
                columns={columns}
                disableRowSelectionOnClick
                hideFooter={true}
                slots={{
                  row: DraggableRow,
                }}
                getRowClassName={getStrategyConfigurationRowClassName}
                sx={sx}
              />
            </div>
          </SortableContext>
        </DndContext>
  );
}