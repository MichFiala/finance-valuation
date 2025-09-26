import { ReactNode, useState } from "react";
import {
  Checkbox,
  MenuItem,
  Select,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { DomainCardModuleTemplate } from "../../shared/DomainCardModuleTemplate";
import { SpendingsDto, SpendingsFrequency } from "./spendingsModel";

export const SpendingsCardModule = ({
  entryDto,
  handleCreateOrUpdate,
  handleDelete,
  color,
  textColor,
  icon,
  enableEditing,
}: {
  entryDto: SpendingsDto;
  handleCreateOrUpdate: (entry: SpendingsDto) => void;
  handleDelete: (entry: SpendingsDto) => void;
  color: string;
  textColor: string;
  icon: ReactNode;
  enableEditing: boolean;
}) => {
  const [entry, setEntry] = useState(entryDto);
  const [isEditing, setIsEditing] = useState(false);

  const handleEditClick = () => {
    if (isEditing) {
      setIsEditing(false);
      return;
    }
    setIsEditing(true);
  };

  const handleSaveClick = (entry: SpendingsDto) => {
    handleCreateOrUpdate(entry);
    setIsEditing(false);
  };
  return (
    <DomainCardModuleTemplate
      color={color}
      textColor={textColor}
      enableEditing={enableEditing}
      isEditing={isEditing}
      handleEditClick={handleEditClick}
      handleSaveClick={() => handleSaveClick(entry)}
      handleDeleteClick={() => handleDelete(entry)}
      header={
        <>
          {icon}
          {isEditing && (
            <TextField
              value={entry.name}
              onChange={(e) => setEntry({ ...entry, name: e.target.value })}
              variant="standard"
              sx={{
                mb: 1.5,
                p: 1,
                marginBottom: 0,
              }}
            />
          )}
          {!isEditing && (
            <Typography
              sx={{
                mb: 1.5,
                p: 1,
                marginBottom: 0,
              }}
            >
              {entry.name}
            </Typography>
          )}
        </>
      }
    >
      <Stack
        direction={"column"}
        alignItems={"center"}
        justifyContent={"center"}
      >
        <Stack direction={"row"} alignContent={"center"}>
          {isEditing && (
            <>
              <Select
                label="Frequency"
                value={entry.frequency}
                onChange={(e) =>
                  setEntry({ ...entry, frequency: e.target.value })
                }
              >
                {Object.values(SpendingsFrequency)?.map((frequency) => (
                  <MenuItem key={frequency} value={frequency}>
                    {frequency}
                  </MenuItem>
                ))}
              </Select>
              <TextField
                placeholder="Amount"
                value={entry.amount}
                onChange={(e) =>
                  setEntry({
                    ...entry,
                    amount: isNaN(parseFloat(e.target.value))
                      ? 0
                      : parseFloat(e.target.value),
                  })
                }
                variant="standard"
                sx={{
                  mb: 1.5,
                  p: 1,
                  marginBottom: 0,
                }}
              />
              <Checkbox
                size="small"
                defaultChecked={entry.isMandatory}
                checked={entry.isMandatory}
                disabled={!isEditing}
                onChange={() =>
                  setEntry({ ...entry, isMandatory: !entry.isMandatory })
                }
                style={{ paddingTop: 0 }}
                sx={{
                  color: "text.primary",
                  "&.Mui-checked": {
                    color: "text.primary",
                  },
                }}
              />
            </>
          )}
          {!isEditing && (
            <>
              <Typography
                sx={{
                  color: "text.primary",
                  mb: 1.5,
                  marginBottom: 0,
                  marginRight: 0.5,
                }}
              >
                {entry.frequency}:
              </Typography>
              <Typography sx={{ mb: 1.5 }}>
                <b>
                  {entry.amount.toLocaleString("cs-CZ", {
                    style: "currency",
                    currency: "CZK",
                  })}
                </b>
              </Typography>
              <Checkbox
                size="small"
                checked={entry.isMandatory}
                disabled
                style={{ paddingTop: 0 }}
                sx={{
                  color: "text.primary",
                  "&.Mui-checked": {
                    color: "text.primary",
                  },
                }}
              />
            </>
          )}
        </Stack>
      </Stack>
    </DomainCardModuleTemplate>
  );
};
