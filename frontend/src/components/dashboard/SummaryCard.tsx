import { formatCurrency } from "../../shared/utils/formatters";

type SummaryCardProps = {
  title: string;
  income: number;
  expense: number;
  balance: number;
};

export default function SummaryCard({
  title,
  income,
  expense,
  balance
}: SummaryCardProps) {
  return (
    <article className="space-y-3">
      <h2 className="text-lg font-semibold text-slate-950">{title}</h2>
      <dl className="flex items-start gap-10 text-sm text-slate-600">
        <div className="space-y-1">
          <dt>Receitas</dt>
          <dd className="text-base font-semibold text-slate-950">{formatCurrency(income)}</dd>
        </div>
        <div className="space-y-1">
          <dt>Despesas</dt>
          <dd className="text-base font-semibold text-slate-950">{formatCurrency(expense)}</dd>
        </div>
        <div className="space-y-1">
          <dt>Saldo</dt>
          <dd className="text-base font-semibold text-slate-950">{formatCurrency(balance)}</dd>
        </div>
      </dl>
    </article>
  );
}
