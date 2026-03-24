import { useEffect, useState } from "react";
import AppNavbar from "./components/layout/AppNavbar";
import CategoriesPage from "./pages/categories/CategoriesPage";
import HomePage from "./pages/home/HomePage";
import PeoplePage from "./pages/people/PeoplePage";
import TransactionsPage from "./pages/transactions/TransactionsPage";
import { useHomeExpensesData } from "./shared/hooks/useHomeExpensesData";

export type AppRoute = "home" | "people" | "categories" | "transactions";

function getRouteFromHash(hash: string): AppRoute {
  const route = hash.replace("#", "");

  if (route === "people" || route === "categories" || route === "transactions") {
    return route;
  }

  return "home";
}

export default function App() {
  const [route, setRoute] = useState<AppRoute>(() => getRouteFromHash(window.location.hash));
  const homeExpenses = useHomeExpensesData();

  useEffect(() => {
    function handleHashChange() {
      setRoute(getRouteFromHash(window.location.hash));
    }

    window.addEventListener("hashchange", handleHashChange);
    return () => window.removeEventListener("hashchange", handleHashChange);
  }, []);

  return (
    <div className="min-h-screen bg-slate-50 text-slate-950">
      <AppNavbar currentRoute={route} />

      {route === "home" && <HomePage data={homeExpenses} />}
      {route === "people" && <PeoplePage data={homeExpenses} />}
      {route === "categories" && <CategoriesPage data={homeExpenses} />}
      {route === "transactions" && <TransactionsPage data={homeExpenses} />}
    </div>
  );
}
