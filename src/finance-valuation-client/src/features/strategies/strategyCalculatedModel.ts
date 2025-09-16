import { StrategyConfigurationType } from "./strategyModel";

export interface CalculatedStrategyResponseDto {
  name: string;
  strategyConfigurationsCalculationSteps: CalculationStepConfigurationDto[];
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