import { CategoryPurpose, TransactionKind } from "../types";

export const purposeLabels: Record<CategoryPurpose, string> = {
  EXPENSE: "Despesa",
  INCOME: "Receita",
  BOTH: "Ambas"
};

export const kindLabels: Record<TransactionKind, string> = {
  EXPENSE: "Despesa",
  INCOME: "Receita"
};
