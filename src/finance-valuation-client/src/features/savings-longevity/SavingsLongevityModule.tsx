import { Card, CardContent, CardHeader, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import FavoriteIcon from "@mui/icons-material/Favorite";
import { SavingsLongevityColors } from "./savingsLongevityStylesSettings";
import {
  fetchSavingsLongevity,
  SavingsLongevityResponse,
} from "./savingsLongevityApi";
import { useTranslation } from "react-i18next";

export function SavingsLongevityModule() {
  const [savingsLongevityResponse, setSavingsLongevityResponse] =
    useState<SavingsLongevityResponse | null>(null);

  const { t } = useTranslation();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const maxMonthsTreshold = 12 * 100;

  useEffect(() => {
    fetchSavingsLongevity()
      .then((data) => setSavingsLongevityResponse(data))
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      {savingsLongevityResponse && (
        <Card
          sx={[
            (theme) => ({
              backgroundColor: theme.palette.secondary.main,
              color: theme.palette.text.primary,
              border: "1px solid",
              borderRadius: "8px",
              borderColor:
                SavingsLongevityColors[savingsLongevityResponse.grade],
            }),
          ]}
        >
          <CardHeader
            avatar={<FavoriteIcon />}
            title={t("savings_longevity")}
            style={{
              background:
                SavingsLongevityColors[savingsLongevityResponse.grade],
              color: "white",
            }}
          ></CardHeader>
          <CardContent>
            <Typography variant="body1">
              <b>{savingsLongevityResponse?.till.toLocaleString()}</b>
            </Typography>
            <Typography variant="body2">
              {savingsLongevityResponse.months > maxMonthsTreshold ? "∞" : savingsLongevityResponse.months} {t("months")}
            </Typography>
            <Typography variant="body2">
              {savingsLongevityResponse.months > maxMonthsTreshold ? "∞" : (savingsLongevityResponse.months / 12).toPrecision(2)} {t("years")}
            </Typography>
            <Typography variant="body2">
              {t("grade")}: {t(`grade_${savingsLongevityResponse.grade}`)}
            </Typography>
          </CardContent>
        </Card>
      )}
    </>
  );
}
