export enum StrategyConfigurationType {
  Saving = 'Saving',
  Spending = 'Spending',
  Investment = 'Investment',
  Debt = 'Debt',
}

export interface StrategyDto {
  id: number,
  name: string;
  strategyConfigurations: StrategyConfigurationDto[];
}

export interface StrategyConfigurationDto {
  id: number;
  name: string;
  type: StrategyConfigurationType;
  referenceId: number;
  monthlyContributionAmount?: number;
  monthlyContributionPercentage?: number;
}