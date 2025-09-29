import { Card, CardContent, CardActions, alpha } from "@mui/material";
import SettingsIcon from "@mui/icons-material/Settings";
import SaveAltIcon from "@mui/icons-material/SaveAlt";
import { Stack, Button } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import CloseIcon from "@mui/icons-material/Close";

export const DomainCardModuleTemplate = ({
  color,
  textColor,
  enableEditing,
  isEditing,
  header,
  children,
  handleEditClick,
  handleSaveClick,
  handleDeleteClick,
}: {
  handleSaveClick: () => void;
  handleDeleteClick: () => void;
  handleEditClick: () => void;
  color: string;
  textColor: string;
  enableEditing: boolean;
  isEditing: boolean;
  header?: React.ReactNode;
  children?: React.ReactNode;
}) => {
  return (
    <Card
      sx={[
        (theme) => ({
          backgroundColor: theme.palette.secondary.main,
          color: theme.palette.text.primary,
          border: "1px solid",
          borderRadius: "8px",
          borderColor: color,
        }),
      ]}
    >
      <Stack
        direction={"row"}
        alignContent={"center"}
        alignItems={"center"}
        justifyContent={"center"}
        sx={{
          color: textColor,
          background: color,
        }}
      >
        {header}
      </Stack>
      <CardContent>{children}</CardContent>
      {enableEditing && (
        <CardActions
          sx={[
            (theme) => ({
              mt: "auto",
              justifyContent: "center",
            }),
          ]}
          style={{
            backgroundColor: alpha(color, 0.7),
            paddingTop: "auto",
          }}
        >
          <Stack
            direction={"row-reverse"}
            width={"100%"}
            justifyContent={"flex-start"}
          >
            <Button size="small" onClick={() => handleEditClick()}>
              {!isEditing && (
                <SettingsIcon
                  sx={[
                    (theme) => ({
                      color: theme.palette.text.primary,
                    }),
                  ]}
                />
              )}
              {isEditing && (
                <CloseIcon
                  sx={[
                    (theme) => ({
                      color: theme.palette.text.primary,
                    }),
                  ]}
                />
              )}
            </Button>
            {isEditing ? (
              <>
                <Button
                  size="small"
                  onClick={() => handleSaveClick()}
                  style={{ color: "green" }}
                >
                  <SaveAltIcon />
                </Button>
                <Button
                  size="small"
                  onClick={() => handleDeleteClick()}
                  style={{ color: "red" }}
                >
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
