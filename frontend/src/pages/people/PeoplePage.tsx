import SimpleTable from "../../components/common/SimpleTable";
import PageContainer from "../../components/layout/PageContainer";
import { useHomeExpensesData } from "../../shared/hooks/useHomeExpensesData";

type PeoplePageProps = {
  data: ReturnType<typeof useHomeExpensesData>;
};

const inputClassName =
  "w-full rounded-md border border-slate-300 bg-white px-3 py-2.5 text-sm text-slate-950 outline-none transition focus:border-blue-600";

const primaryButtonClassName =
  "rounded-md bg-blue-600 px-4 py-2.5 text-sm font-medium text-white transition hover:bg-blue-700";

const secondaryButtonClassName =
  "rounded-md border border-slate-300 px-4 py-2.5 text-sm font-medium text-slate-700 transition hover:border-slate-400 hover:text-slate-950";

const dangerButtonClassName =
  "rounded-md border border-slate-300 px-3 py-2 text-sm font-medium text-slate-700 transition hover:border-slate-400 hover:text-slate-950";

export default function PeoplePage({ data }: PeoplePageProps) {
  return (
    <PageContainer
      title="Pessoas"
      description="Cadastro, edição e exclusão de pessoas."
      loading={data.loading}
      successMessage={data.successMessage}
      errorMessage={data.errorMessage}
    >
      <section className="rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
        <form className="grid grid-cols-[1.8fr_0.8fr_auto] items-end gap-4" onSubmit={data.handlePersonSubmit}>
          <label className="space-y-2 text-sm font-medium text-slate-700">
            <span>Nome</span>
            <input
              className={inputClassName}
              value={data.personForm.name}
              onChange={(event) => data.setPersonForm((current) => ({ ...current, name: event.target.value }))}
              maxLength={200}
              required
            />
          </label>

          <label className="space-y-2 text-sm font-medium text-slate-700">
            <span>Idade</span>
            <input
              className={inputClassName}
              type="number"
              min={0}
              value={data.personForm.age}
              onChange={(event) => data.setPersonForm((current) => ({ ...current, age: event.target.value }))}
              required
            />
          </label>

          <div className="flex items-center gap-3">
            <button type="submit" className={primaryButtonClassName}>
              {data.editingPersonId ? "Salvar edição" : "Cadastrar pessoa"}
            </button>
            {data.editingPersonId && (
              <button type="button" className={secondaryButtonClassName} onClick={data.resetPersonForm}>
                Cancelar
              </button>
            )}
          </div>
        </form>
      </section>

      <section className="mt-8 rounded-xl border border-slate-200 bg-white p-6 shadow-sm">
        <SimpleTable
          columns={["Nome", "Idade", "Ações"]}
          emptyMessage="Nenhuma pessoa cadastrada."
          hasRows={data.people.length > 0}
        >
          {data.people.map((person) => (
            <tr key={person.id} className="border-b border-slate-100">
              <td className="py-3 pr-4">{person.name}</td>
              <td className="py-3 pr-4">{person.age}</td>
              <td className="py-3 pr-4">
                <div className="flex gap-3">
                  <button type="button" className={secondaryButtonClassName} onClick={() => data.startPersonEdit(person)}>
                    Editar
                  </button>
                  <button type="button" className={dangerButtonClassName} onClick={() => data.handlePersonDelete(person.id)}>
                    Excluir
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </SimpleTable>
      </section>
    </PageContainer>
  );
}
