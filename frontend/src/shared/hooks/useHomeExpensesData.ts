import { FormEvent, useEffect, useState } from "react";
import { request } from "../api/http";
import {
  Category,
  CategoryPurpose,
  CategoryTotals,
  Person,
  PersonTotals,
  Transaction,
  TransactionKind
} from "../types";
import { getErrorMessage } from "../utils/formatters";

export type PersonForm = {
  name: string;
  age: string;
};

export type CategoryForm = {
  description: string;
  purpose: CategoryPurpose;
};

export type TransactionForm = {
  description: string;
  amount: string;
  kind: TransactionKind;
  categoryId: string;
  personId: string;
};

const emptyPersonForm: PersonForm = { name: "", age: "" };
const emptyCategoryForm: CategoryForm = { description: "", purpose: "EXPENSE" };
const emptyTransactionForm: TransactionForm = {
  description: "",
  amount: "",
  kind: "EXPENSE",
  categoryId: "",
  personId: ""
};

const emptyPersonTotals: PersonTotals = {
  people: [],
  totals: { totalIncome: 0, totalExpense: 0, balance: 0 }
};

const emptyCategoryTotals: CategoryTotals = {
  categories: [],
  totals: { totalIncome: 0, totalExpense: 0, balance: 0 }
};

export function useHomeExpensesData() {
  const [people, setPeople] = useState<Person[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [personTotals, setPersonTotals] = useState<PersonTotals>(emptyPersonTotals);
  const [categoryTotals, setCategoryTotals] = useState<CategoryTotals>(emptyCategoryTotals);

  const [personForm, setPersonForm] = useState<PersonForm>(emptyPersonForm);
  const [categoryForm, setCategoryForm] = useState<CategoryForm>(emptyCategoryForm);
  const [transactionForm, setTransactionForm] = useState<TransactionForm>(emptyTransactionForm);

  const [editingPersonId, setEditingPersonId] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  const selectedPerson = people.find((person) => person.id === transactionForm.personId);
  const compatibleCategories = categories.filter((category) => {
    if (category.purpose === "BOTH") {
      return true;
    }

    return category.purpose === transactionForm.kind;
  });

  useEffect(() => {
    void loadAll();
  }, []);

  useEffect(() => {
    if (selectedPerson && selectedPerson.age < 18 && transactionForm.kind === "INCOME") {
      setTransactionForm((current) => ({ ...current, kind: "EXPENSE", categoryId: "" }));
    }
  }, [selectedPerson, transactionForm.kind]);

  async function loadAll() {
    setLoading(true);
    setErrorMessage("");

    try {
      const [peopleData, categoriesData, transactionsData, peopleTotalsData, categoriesTotalsData] =
        await Promise.all([
          request<Person[]>("/persons"),
          request<Category[]>("/categories"),
          request<Transaction[]>("/transactions"),
          request<PersonTotals>("/persons/totals"),
          request<CategoryTotals>("/categories/totals")
        ]);

      setPeople(peopleData);
      setCategories(categoriesData);
      setTransactions(transactionsData);
      setPersonTotals(peopleTotalsData);
      setCategoryTotals(categoriesTotalsData);
    } catch (error) {
      setErrorMessage(getErrorMessage(error));
    } finally {
      setLoading(false);
    }
  }

  function clearMessages() {
    setErrorMessage("");
    setSuccessMessage("");
  }

  function resetPersonForm() {
    setEditingPersonId(null);
    setPersonForm(emptyPersonForm);
  }

  async function handlePersonSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    clearMessages();

    const payload = {
      name: personForm.name.trim(),
      age: Number(personForm.age)
    };

    try {
      if (editingPersonId) {
        await request<Person>(`/persons/${editingPersonId}`, {
          method: "PUT",
          body: JSON.stringify(payload)
        });
        setSuccessMessage("Pessoa atualizada com sucesso.");
      } else {
        await request<Person>("/persons", {
          method: "POST",
          body: JSON.stringify(payload)
        });
        setSuccessMessage("Pessoa criada com sucesso.");
      }

      resetPersonForm();
      await loadAll();
    } catch (error) {
      setErrorMessage(getErrorMessage(error));
    }
  }

  function startPersonEdit(person: Person) {
    clearMessages();
    setEditingPersonId(person.id);
    setPersonForm({
      name: person.name,
      age: String(person.age)
    });
  }

  async function handlePersonDelete(personId: string) {
    const confirmed = window.confirm("Excluir esta pessoa também removerá todas as transações dela. Continuar?");

    if (!confirmed) {
      return;
    }

    clearMessages();

    try {
      await request<void>(`/persons/${personId}`, {
        method: "DELETE"
      });

      if (editingPersonId === personId) {
        resetPersonForm();
      }

      setSuccessMessage("Pessoa removida com sucesso.");
      await loadAll();
    } catch (error) {
      setErrorMessage(getErrorMessage(error));
    }
  }

  async function handleCategorySubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    clearMessages();

    try {
      await request<Category>("/categories", {
        method: "POST",
        body: JSON.stringify({
          description: categoryForm.description.trim(),
          purpose: categoryForm.purpose
        })
      });

      setCategoryForm(emptyCategoryForm);
      setSuccessMessage("Categoria criada com sucesso.");
      await loadAll();
    } catch (error) {
      setErrorMessage(getErrorMessage(error));
    }
  }

  async function handleTransactionSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    clearMessages();

    try {
      await request<Transaction>("/transactions", {
        method: "POST",
        body: JSON.stringify({
          description: transactionForm.description.trim(),
          amount: Number(transactionForm.amount),
          kind: transactionForm.kind,
          categoryId: transactionForm.categoryId,
          personId: transactionForm.personId
        })
      });

      setTransactionForm((current) => ({
        ...emptyTransactionForm,
        kind: current.kind
      }));

      setSuccessMessage("Transação criada com sucesso.");
      await loadAll();
    } catch (error) {
      setErrorMessage(getErrorMessage(error));
    }
  }

  return {
    people,
    categories,
    transactions,
    personTotals,
    categoryTotals,
    personForm,
    setPersonForm,
    categoryForm,
    setCategoryForm,
    transactionForm,
    setTransactionForm,
    editingPersonId,
    selectedPerson,
    compatibleCategories,
    loading,
    errorMessage,
    successMessage,
    clearMessages,
    resetPersonForm,
    loadAll,
    startPersonEdit,
    handlePersonSubmit,
    handlePersonDelete,
    handleCategorySubmit,
    handleTransactionSubmit
  };
}
