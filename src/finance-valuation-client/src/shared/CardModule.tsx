import {
  Card,
  CardContent,
  Typography,
  CardActions,
  alpha,
  TextField,
  Checkbox,
} from "@mui/material";
import { ReactNode, useState } from "react";
import SettingsIcon from "@mui/icons-material/Settings";
import SaveAltIcon from "@mui/icons-material/SaveAlt";
import { SavingsDto } from "../features/savings/savingsModel";
import { DebtDto } from "../features/debts/debtModel";
import { InvestmentDto } from "../features/investments/investmentModel";
import {
  SpendingsDto,
  SpendingsFrequency,
} from "../features/spendings/spendingsModel";
import { Stack, Select, MenuItem, Button } from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import CloseIcon from '@mui/icons-material/Close';

export const CardModule = ({
  entry,
  handleCreateOrUpdate,
  handleDelete,
  color,
  textColor,
  icon,
  enableEditing,
}: {
  entry: SavingsDto | InvestmentDto | DebtDto | SpendingsDto;
  handleCreateOrUpdate: (
    id: number | undefined,
    name: string,
    amount: number,
    type: string,
    isMandatory: boolean
  ) => void;
  handleDelete:(entry: SavingsDto | InvestmentDto | DebtDto | SpendingsDto) => void;
  color: string;
  textColor: string;
  icon: ReactNode;
  enableEditing: boolean;
}) => {
  const [name, setName] = useState("");
  const [amount, setAmount] = useState(0);
  const [type, setType] = useState("");
  const [isChecked, setIsChecked] = useState("isMandatory" in entry ? entry.isMandatory : false);

  const [isEditing, setIsEditing] = useState(false);

  const handleEditClick = (
    entry: SavingsDto | InvestmentDto | DebtDto | SpendingsDto
  ) => {
    if (isEditing) {
      setIsEditing(false);
      return;
    }

    setName(entry.name);
    setAmount(entry.amount);
    "frequency" in entry && setType(entry.frequency);
    "isMandatory" in entry && setIsChecked(entry.isMandatory);
    setIsEditing(true);
  };

  const handleSaveClick = (
    entry: SavingsDto | InvestmentDto | DebtDto | SpendingsDto
  ) => {
    handleCreateOrUpdate(entry.id, name, amount, type, isChecked);
    setIsEditing(false);
  };
  return (
    <Card
      style={{
        border: "1px solid #ccc",
        borderRadius: "8px",
        overflow: "hidden",
      }}
    >
      <Stack
        direction={"row"}
        alignContent={"center"}
        alignItems={"center"}
        justifyContent={"center"}
        style={{ backgroundColor: color, color: textColor }}
      >
        {icon}
        {isEditing ? (
          <TextField
            value={name}
            onChange={(e) => setName(e.target.value)}
            variant="standard"
            sx={{
              color: "text.secondary",
              mb: 1.5,
              p: 1,
              marginBottom: 0,
            }}
          />
        ) : (
          <Typography
            sx={{
              color: "text.secondary",
              mb: 1.5,
              p: 1,
              marginBottom: 0,
            }}
          >
            {entry.name}
          </Typography>
        )}
      </Stack>
      <CardContent>
        <Stack
          direction={"column"}
          alignItems={"center"}
          justifyContent={"center"}
        >
          <Stack direction={"row"} alignContent={"center"}>
            {"frequency" in entry &&
              (isEditing ? (
                <Select
                  label="Frequency"
                  value={type}
                  onChange={(e) => setType(e.target.value as string)}
                >
                  {Object.values(SpendingsFrequency)?.map((frequency) => (
                    <MenuItem key={frequency} value={frequency}>
                      {frequency}
                    </MenuItem>
                  ))}
                </Select>
              ) : (
                <Typography
                  sx={{
                    color: "text.secondary",
                    mb: 1.5,
                    marginBottom: 0,
                    marginRight: 0.5,
                  }}
                >
                  {entry.frequency}:
                </Typography>
              ))}
            {isEditing ? (
              <TextField
                value={amount}
                onChange={(e) =>
                  setAmount(
                    isNaN(parseFloat(e.target.value))
                      ? 0
                      : parseFloat(e.target.value)
                  )
                }
                variant="standard"
                sx={{
                  color: "text.secondary",
                  mb: 1.5,
                  p: 1,
                  marginBottom: 0,
                }}
              />
            ) : (
              <Typography sx={{ color: "text.secondary", mb: 1.5 }}>
                <b>
                  {entry.amount.toLocaleString("cs-CZ", {
                    style: "currency",
                    currency: "CZK",
                  })}
                </b>
              </Typography>
            )}
            {"targetAmount" in entry && entry.targetAmount ? (
              <Typography sx={{ color: "text.secondary", mb: 1.5 }}>
                /{" "}
                {entry.targetAmount.toLocaleString("cs-CZ", {
                  style: "currency",
                  currency: "CZK",
                })}
              </Typography>
            ) : null}
            {"isMandatory" in entry &&
              (isEditing ? (
                <Checkbox
                  size="small"
                  defaultChecked={isChecked}
                  checked={isChecked}
                  disabled={!isEditing}
                  onChange={() => setIsChecked(!isChecked)}
                  style={{ paddingTop: 0 }}
                />
              ) : (
                <Checkbox
                  size="small"
                  checked={isChecked}
                  disabled
                  style={{ paddingTop: 0 }}
                />
              ))}
          </Stack>
          {"payment" in entry && (
            <Typography variant="body2">
              Payment:{" "}
              <b>
                {entry.payment.toLocaleString("cs-CZ", {
                  style: "currency",
                  currency: "CZK",
                })}
              </b>
            </Typography>
          )}
          {"interest" in entry && (
            <Typography variant="body2">
              Interest: <b>{entry.interest}</b> %
            </Typography>
          )}
        </Stack>
      </CardContent>
      {enableEditing && (
        <CardActions
          sx={{ mt: "auto", justifyContent: "center" }}
          style={{
            backgroundColor: alpha(color, 0.3),
            paddingTop: "auto",
          }}
        >
          <Stack
            direction={"row-reverse"}
            width={"100%"}
            justifyContent={"flex-start"}
          >
            <Button size="small" onClick={() => handleEditClick(entry)}>
               {isEditing ? <CloseIcon/> : <SettingsIcon />}
            </Button>
            {isEditing ? (
              <>
              <Button size="small" onClick={() => handleSaveClick(entry)} style={{color: "green"}}>
                <SaveAltIcon />
              </Button>
              <Button size="small" onClick={() => handleDelete(entry)} style={{color: "red"}}>
                <DeleteIcon />
              </Button>
              </>
            ) : (
              <></>
            )}
          </Stack>
        </CardActions>
      )}
    </Card>
  );
};
