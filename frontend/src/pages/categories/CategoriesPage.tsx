import SimpleTable from "../../components/common/SimpleTable";
import PageContainer from "../../components/layout/PageContainer";
import { purposeLabels } from "../../shared/constants/labels";
import { CategoryPurpose } from "../../shared/types";
import { useHomeExpensesData } from "../../shared/hooks/useHomeExpensesData";

type CategoriesPageProps = {
  data: ReturnType<typeof useHomeExpensesData>;
};

const inputClassName =
  "w-full rounded-md border border-slate-300 bg-white px-3 py-2.5 text-sm text-slate-950 outline-none transition focus:border-blue-600";

const primaryButtonClassName =
  "rounded-md bg-blue-600 px-4 py-2.5 text-sm font-medium text-white transition hover:bg-blue-700";

export default function CategoriesPage({ data }: CategoriesPageProps) {
  return (
    <PageContainer
      title="Categorias"
      description="Cadastro de categorias para receitas, despesas ou ambas."
      loading={data.loading}
      successMessage={data.successMessage}
      errorMessage={data.errorMessage}
    >
      <section className="rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
        <form className="grid grid-cols-[1.8fr_1fr_auto] items-end gap-4" onSubmit={data.handleCategorySubmit}>
          <label className="space-y-2 text-sm font-medium text-slate-700">
            <span>Descrição</span>
            <input
              className={inputClassName}
              value={data.categoryForm.description}
              onChange={(event) =>
                data.setCategoryForm((current) => ({ ...current, description: event.target.value }))
              }
              maxLength={400}
              required
            />
          </label>

          <label className="space-y-2 text-sm font-medium text-slate-700">
            <span>Finalidade</span>
            <select
              className={inputClassName}
              value={data.categoryForm.purpose}
              onChange={(event) =>
                data.setCategoryForm((current) => ({
                  ...current,
                  purpose: event.target.value as CategoryPurpose
                }))
              }
            >
              {Object.entries(purposeLabels).map(([value, label]) => (
                <option key={value} value={value}>
                  {label}
                </option>
              ))}
            </select>
          </label>

          <div>
            <button type="submit" className={primaryButtonClassName}>
              Cadastrar categoria
            </button>
          </div>
        </form>
      </section>

      <section className="mt-8 rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
        <SimpleTable
          columns={["Descrição", "Finalidade"]}
          emptyMessage="Nenhuma categoria cadastrada."
          hasRows={data.categories.length > 0}
        >
          {data.categories.map((category) => (
            <tr key={category.id} className="border-b border-slate-100">
              <td className="py-3 pr-4">{category.description}</td>
              <td className="py-3 pr-4">{purposeLabels[category.purpose]}</td>
            </tr>
          ))}
        </SimpleTable>
      </section>
    </PageContainer>
  );
}
