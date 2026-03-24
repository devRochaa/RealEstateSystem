import SimpleTable from "../../components/common/SimpleTable";
import PageContainer from "../../components/layout/PageContainer";
import { kindLabels, purposeLabels } from "../../shared/constants/labels";
import { TransactionKind } from "../../shared/types";
import { useHomeExpensesData } from "../../shared/hooks/useHomeExpensesData";
import { formatCurrency } from "../../shared/utils/formatters";

type TransactionsPageProps = {
  data: ReturnType<typeof useHomeExpensesData>;
};

const inputClassName =
  "w-full rounded-md border border-slate-300 bg-white px-3 py-2.5 text-sm text-slate-950 outline-none transition focus:border-blue-600";

const primaryButtonClassName =
  "rounded-md bg-blue-600 px-4 py-2.5 text-sm font-medium text-white transition hover:bg-blue-700";

export default function TransactionsPage({ data }: TransactionsPageProps) {
  return (
    <PageContainer
      title="Transações"
      description="Cadastro e listagem de receitas e despesas."
      loading={data.loading}
      successMessage={data.successMessage}
      errorMessage={data.errorMessage}
    >
      <section className="rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
        <form className="grid grid-cols-[1.8fr_0.9fr_1.3fr_1fr_1.3fr_auto] items-end gap-4" onSubmit={data.handleTransactionSubmit}>
          <label className="space-y-2 text-sm font-medium text-slate-700">
            <span>Descrição</span>
            <input
              className={inputClassName}
              value={data.transactionForm.description}
              onChange={(event) =>
                data.setTransactionForm((current) => ({ ...current, description: event.target.value }))
              }
              maxLength={400}
              required
            />
          </label>

          <label className="space-y-2 text-sm font-medium text-slate-700">
            <span>Valor</span>
            <input
              className={inputClassName}
              type="number"
              min="0.01"
              step="0.01"
              value={data.transactionForm.amount}
              onChange={(event) =>
                data.setTransactionForm((current) => ({ ...current, amount: event.target.value }))
              }
              required
            />
          </label>

          <label className="space-y-2 text-sm font-medium text-slate-700">
            <span>Pessoa</span>
            <select
              className={inputClassName}
              value={data.transactionForm.personId}
              onChange={(event) =>
                data.setTransactionForm((current) => ({
                  ...current,
                  personId: event.target.value,
                  categoryId: ""
                }))
              }
              required
            >
              <option value="">Selecione</option>
              {data.people.map((person) => (
                <option key={person.id} value={person.id}>
                  {person.name} ({person.age} anos)
                </option>
              ))}
            </select>
          </label>

          <label className="space-y-2 text-sm font-medium text-slate-700">
            <span>Tipo</span>
            <select
              className={inputClassName}
              value={data.transactionForm.kind}
              onChange={(event) =>
                data.setTransactionForm((current) => ({
                  ...current,
                  kind: event.target.value as TransactionKind,
                  categoryId: ""
                }))
              }
            >
              <option value="EXPENSE">Despesa</option>
              <option value="INCOME" disabled={data.selectedPerson?.age !== undefined && data.selectedPerson.age < 18}>
                Receita
              </option>
            </select>
          </label>

          <label className="space-y-2 text-sm font-medium text-slate-700">
            <span>Categoria</span>
            <select
              className={inputClassName}
              value={data.transactionForm.categoryId}
              onChange={(event) =>
                data.setTransactionForm((current) => ({ ...current, categoryId: event.target.value }))
              }
              required
            >
              <option value="">Selecione</option>
              {data.compatibleCategories.map((category) => (
                <option key={category.id} value={category.id}>
                  {category.description} ({purposeLabels[category.purpose]})
                </option>
              ))}
            </select>
          </label>

          <div>
            <button type="submit" className={primaryButtonClassName}>
              Cadastrar transação
            </button>
          </div>
        </form>
      </section>

      <section className="mt-8 rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
        <SimpleTable
          columns={["Descrição", "Valor", "Tipo", "Categoria", "Pessoa"]}
          emptyMessage="Nenhuma transação cadastrada."
          hasRows={data.transactions.length > 0}
        >
          {data.transactions.map((transaction) => (
            <tr key={transaction.id} className="border-b border-slate-100">
              <td className="py-3 pr-4">{transaction.description}</td>
              <td className="py-3 pr-4">{formatCurrency(transaction.amount)}</td>
              <td className="py-3 pr-4">{kindLabels[transaction.kind]}</td>
              <td className="py-3 pr-4">{transaction.categoryDescription}</td>
              <td className="py-3 pr-4">{transaction.personName}</td>
            </tr>
          ))}
        </SimpleTable>
      </section>
    </PageContainer>
  );
}
