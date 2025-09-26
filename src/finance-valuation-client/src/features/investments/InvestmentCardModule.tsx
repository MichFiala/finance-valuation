import { ReactNode, useState } from "react";
import { Stack, TextField, Typography } from "@mui/material";
import { DomainCardModuleTemplate } from "../../shared/DomainCardModuleTemplate";
import { InvestmentDto } from "./investmentModel";


export const InvestmentCardModule = ({
  entryDto,
  handleCreateOrUpdate,
  handleDelete,
  color,
  textColor,
  icon,
  enableEditing,
}: {
  entryDto: InvestmentDto;
  handleCreateOrUpdate: (entry: InvestmentDto) => void;
  handleDelete: (entry: InvestmentDto) => void;
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

  const handleSaveClick = (entry: InvestmentDto) => {
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
          </>
        )}
        {!isEditing && (
          <>
            <Typography sx={{ mb: 1.5 }}>
              <b>
                {entry.amount.toLocaleString("cs-CZ", {
                  style: "currency",
                  currency: "CZK",
                })}
              </b>
            </Typography>
          </>
        )}
      </Stack>
    </DomainCardModuleTemplate>
  );
};