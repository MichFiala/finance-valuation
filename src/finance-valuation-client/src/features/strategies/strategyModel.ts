export enum StrategyConfigurationType {
  Saving = 'Saving',
  Spending = 'Spending',
  Investment = 'Investment',
  Debt = 'Debt',
}

export interface StrategyDto {
  name: string;
  strategyConfigurations: StrategyConfigurationDto[];
}

export interface StrategyConfigurationDto {
  id: string;
  name: string;
  type: StrategyConfigurationType;
  referenceId: number;
  monthlyContributionAmount?: number;
  monthlyContributionPercentage?: number;
}