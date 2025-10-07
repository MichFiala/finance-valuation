import { StrategyConfigurationType } from "./strategyModel";

export interface CalculatedStrategyResponseDto {
  name: string;
  strategyConfigurationsCalculationSteps: CalculationStepConfigurationDto[];
  strategyConfigurationsCalculationByAccounts: StrategyConfigurationCalculationByAccountDto[];
}

export interface CalculationStepConfigurationDto {
  id: string;
  name: string;
  type: StrategyConfigurationType;
  availableAmount: number;
  monthlyExpectedContributionAmount: number | null;
  monthlyExpectedContributionPercentage: number;
  monthlyActualContributionAmount: number;
  monthlyActualContributionPercentage: number;
}

export interface StrategyConfigurationCalculationByAccountDto {
  id: string;
  accountName: string;
  totalMonthlyActualContributionAmount?: number;
  totalMonthlyActualContributionPercentage?: number;
}