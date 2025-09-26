import { ReactNode, useState } from "react";
import { Stack, TextField, Typography } from "@mui/material";
import { DomainCardModuleTemplate } from "../../shared/DomainCardModuleTemplate";
import { SavingsDto } from "./savingsModel";

export const SavingsCardModule = ({
  entryDto,
  handleCreateOrUpdate,
  handleDelete,
  color,
  textColor,
  icon,
  enableEditing,
}: {
  entryDto: SavingsDto;
  handleCreateOrUpdate: (entry: SavingsDto) => void;
  handleDelete: (entry: SavingsDto) => void;
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

  const handleSaveClick = (entry: SavingsDto) => {
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
        {isEditing && (
          <>
            <TextField
              label="Amount"
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
            <TextField
              placeholder="Target Amount"
              value={entry.targetAmount}
              onChange={(e) =>
                setEntry({
                  ...entry,
                  targetAmount: isNaN(parseFloat(e.target.value))
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
          </>
        )}
        {!isEditing && (
          <Stack direction={"row"} alignContent={"center"}>
            <Typography sx={{ mb: 1.5 }}>
              <b>
                {entry.amount.toLocaleString("cs-CZ", {
                  style: "currency",
                  currency: "CZK",
                })}
              </b>
            </Typography>
            {entry.targetAmount && (
              <Typography sx={{ mb: 1.5 }}>
                /{" "}
                {entry.targetAmount.toLocaleString("cs-CZ", {
                  style: "currency",
                  currency: "CZK",
                })}
              </Typography>
            )}
          </Stack>
        )}
      </Stack>
    </DomainCardModuleTemplate>
  );
};
