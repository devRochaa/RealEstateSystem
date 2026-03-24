import SummaryCard from "../../components/dashboard/SummaryCard";
import PageContainer from "../../components/layout/PageContainer";
import SimpleTable from "../../components/common/SimpleTable";
import { formatCurrency } from "../../shared/utils/formatters";
import { useHomeExpensesData } from "../../shared/hooks/useHomeExpensesData";
import { purposeLabels } from "../../shared/constants/labels";

type HomePageProps = {
  data: ReturnType<typeof useHomeExpensesData>;
};

export default function HomePage({ data }: HomePageProps) {
  return (
    <PageContainer
      title="Visão geral"
      description="Resumo dos totais e visão rápida do que já foi cadastrado."
      loading={data.loading}
      successMessage={data.successMessage}
      errorMessage={data.errorMessage}
    >
      <section className="grid grid-cols-2 gap-8">
        <div className="rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
          <SummaryCard
            title="Totais por pessoa"
            income={data.personTotals.totals.totalIncome}
            expense={data.personTotals.totals.totalExpense}
            balance={data.personTotals.totals.balance}
          />
        </div>

        <div className="rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
          <SummaryCard
            title="Totais por categoria"
            income={data.categoryTotals.totals.totalIncome}
            expense={data.categoryTotals.totals.totalExpense}
            balance={data.categoryTotals.totals.balance}
          />
        </div>
      </section>

      <section className="mt-10 grid grid-cols-2 gap-8">
        <div className="rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
          <h2 className="mb-4 text-lg font-semibold text-slate-950">Totais por pessoa</h2>
          <SimpleTable
            columns={["Pessoa", "Receitas", "Despesas", "Saldo"]}
            emptyMessage="Sem dados para exibir."
            hasRows={data.personTotals.people.length > 0}
          >
            {data.personTotals.people.map((item) => (
              <tr key={item.id} className="border-b border-slate-100">
                <td className="py-3 pr-4">{item.name}</td>
                <td className="py-3 pr-4">{formatCurrency(item.totalIncome)}</td>
                <td className="py-3 pr-4">{formatCurrency(item.totalExpense)}</td>
                <td className="py-3">{formatCurrency(item.balance)}</td>
              </tr>
            ))}
          </SimpleTable>
        </div>

        <div className="rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
          <h2 className="mb-4 text-lg font-semibold text-slate-950">Totais por categoria</h2>
          <SimpleTable
            columns={["Categoria", "Finalidade", "Receitas", "Despesas", "Saldo"]}
            emptyMessage="Sem dados para exibir."
            hasRows={data.categoryTotals.categories.length > 0}
          >
            {data.categoryTotals.categories.map((item) => (
              <tr key={item.id} className="border-b border-slate-100">
                <td className="py-3 pr-4">{item.description}</td>
                <td className="py-3 pr-4">{purposeLabels[item.purpose]}</td>
                <td className="py-3 pr-4">{formatCurrency(item.totalIncome)}</td>
                <td className="py-3 pr-4">{formatCurrency(item.totalExpense)}</td>
                <td className="py-3">{formatCurrency(item.balance)}</td>
              </tr>
            ))}
          </SimpleTable>
        </div>
      </section>
    </PageContainer>
  );
}
