import { SavingsLongevityGrade } from "./summaryApi";

export const SavingsLongevityColors: Record<SavingsLongevityGrade, string> = {
  [SavingsLongevityGrade.Critical]: "#C62828",
  [SavingsLongevityGrade.Insufficient]: "#E57373",
  [SavingsLongevityGrade.NeedsImprovement]: "#FFCDD2",

  [SavingsLongevityGrade.Recommended]: "#A5D6A7", 
  [SavingsLongevityGrade.Strong]: "#66BB6A",
  [SavingsLongevityGrade.Excellent]: "#33a039ff",
};
