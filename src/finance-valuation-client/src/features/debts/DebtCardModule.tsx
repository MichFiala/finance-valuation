import { ReactNode, useState } from "react";
import { InputAdornment, Stack, TextField, Typography } from "@mui/material";
import { DomainCardModuleTemplate } from "../../shared/DomainCardModuleTemplate";
import { DebtDto, DebtUpdateDto } from "./debtModel";

export const DebtCardModule = ({
  entryDto,
  handleCreateOrUpdate,
  handleDelete,
  color,
  textColor,
  icon,
  enableEditing,
}: {
  entryDto: DebtDto;
  handleCreateOrUpdate: (entry: DebtUpdateDto) => void;
  handleDelete: (entry: DebtDto) => void;
  color: string;
  textColor: string;
  icon: ReactNode;
  enableEditing: boolean;
}) => {
  const [entry, setEntry] = useState<DebtUpdateDto>({
    ...entryDto,
    amount: entryDto.amount.toString(),
    interest: entryDto.interest.toString(),
    payment: entryDto.payment.toString(),
  });
  const [isEditing, setIsEditing] = useState(false);

  const handleEditClick = () => {
    if (isEditing) {
      setIsEditing(false);
      return;
    }
    setIsEditing(true);
  };

  const handleSaveClick = (entry: DebtUpdateDto) => {
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
      handleDeleteClick={() => handleDelete(entryDto)}
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
                  amount: e.target.value,
                })
              }
              variant="standard"
              sx={{
                mb: 1.5,
                p: 1,
                marginBottom: 0,
              }}
              slotProps={{
                input: {
                  startAdornment: (
                    <InputAdornment position="start">CZK</InputAdornment>
                  ),
                },
              }}              
            />
            <TextField
              label="Payment"
              placeholder="Payment"
              value={entry.payment}
              onChange={(e) =>
                setEntry({
                  ...entry,
                  payment: e.target.value,
                })
              }
              variant="standard"
              sx={{
                mb: 1.5,
                p: 1,
                marginBottom: 0,
              }}
              slotProps={{
                input: {
                  startAdornment: (
                    <InputAdornment position="start">CZK</InputAdornment>
                  ),
                },
              }}
            />
            <TextField
              label="Interest"
              placeholder="Interest"
              value={entry.interest}
              onChange={(e) =>
                setEntry({
                  ...entry,
                  interest: e.target.value,
                })
              }
              variant="standard"
              sx={{
                mb: 1.5,
                p: 1,
                marginBottom: 0,
              }}
              slotProps={{
                input: {
                  startAdornment: (
                    <InputAdornment position="start">%</InputAdornment>
                  ),
                },
              }}
            />
          </>
        )}
        {!isEditing && (
          <>
            <Typography sx={{ mb: 1.5 }}>
              <b>
                {entryDto.amount.toLocaleString("cs-CZ", {
                  style: "currency",
                  currency: "CZK",
                })}
              </b>
            </Typography>
            {entryDto.payment && (
              <Typography variant="body2">
                Payment:{" "}
                <b>
                  {entryDto.payment.toLocaleString("cs-CZ", {
                    style: "currency",
                    currency: "CZK",
                  })}
                </b>
              </Typography>
            )}
            {entryDto.interest && (
              <Typography variant="body2">
                Interest: <b>{entryDto.interest}</b> %
              </Typography>
            )}
          </>
        )}
      </Stack>
    </DomainCardModuleTemplate>
  );
};
