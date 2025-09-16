import { GridRowClassNameParams } from "@mui/x-data-grid";
import { CalculationStepConfigurationDto } from "./strategyCalculatedModel";
import { StrategyConfigurationDto, StrategyConfigurationType } from "./strategyModel";
import { debtColor, debtTextColor } from "../debts/debtStylesSettings";
import { investmentColor, investmentTextColor } from "../investments/investmentStylesSettings";
import { savingsColor, savingsTextColor } from "../savings/savingStylesSettings";
import { spendingColor, spendingTextColor } from "../spendings/spendingStylesSettings";
import { alpha } from "@mui/material";

const getRowClassName = (type: StrategyConfigurationType) => {
    switch (type) {
        case "Spending": {
            return "spending-row";
        }
        case "Investment":
            return "investment-row";
        case "Saving":
            return "saving-row";
        case "Debt":
            return "debt-row";
        default:
            return "";
    }
};

export const getCalculationStepRowClassName = (
    params: GridRowClassNameParams<CalculationStepConfigurationDto>
) => getRowClassName(params.row.type);

export const getStrategyConfigurationRowClassName = (
    params: GridRowClassNameParams<StrategyConfigurationDto>
) => getRowClassName(params.row.type);

export const sx = {
    "& .spending-row": {
        backgroundColor: alpha(spendingColor, 0.9),
        color: spendingTextColor
    },
    "& .investment-row": {
        backgroundColor: alpha(investmentColor, 0.9),
        color: investmentTextColor,
    },
    "& .saving-row": {
        backgroundColor: alpha(savingsColor, 0.9),
        color: savingsTextColor
    },
    "& .debt-row": {
        backgroundColor: alpha(debtColor, 0.9),
        color: debtTextColor
    },
};