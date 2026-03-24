export type CategoryPurpose = "EXPENSE" | "INCOME" | "BOTH";
export type TransactionKind = "EXPENSE" | "INCOME";

export interface Person {
  id: string;
  name: string;
  age: number;
}

export interface Category {
  id: string;
  description: string;
  purpose: CategoryPurpose;
}

export interface Transaction {
  id: string;
  description: string;
  amount: number;
  kind: TransactionKind;
  categoryId: string;
  categoryDescription: string;
  personId: string;
  personName: string;
}

export interface TotalsSummary {
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

export interface PersonBalance {
  id: string;
  name: string;
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

export interface PersonTotals {
  people: PersonBalance[];
  totals: TotalsSummary;
}

export interface CategoryBalance {
  id: string;
  description: string;
  purpose: CategoryPurpose;
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

export interface CategoryTotals {
  categories: CategoryBalance[];
  totals: TotalsSummary;
}
