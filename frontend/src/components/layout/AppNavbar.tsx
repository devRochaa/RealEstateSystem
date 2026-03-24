import type { AppRoute } from "../../App";

type AppNavbarProps = {
  currentRoute: AppRoute;
};

const navigationItems: Array<{ label: string; route: AppRoute }> = [
  { label: "Início", route: "home" },
  { label: "Pessoas", route: "people" },
  { label: "Categorias", route: "categories" },
  { label: "Transações", route: "transactions" }
];

export default function AppNavbar({ currentRoute }: AppNavbarProps) {
  return (
    <header className="border-b border-slate-200 bg-white">
      <div className="mx-auto flex h-16 w-[1400px] items-center justify-between px-8">
        <a href="#home" className="text-lg font-semibold tracking-tight text-slate-950">
          HomeExpenses
        </a>

        <nav className="flex items-center gap-2">
          {navigationItems.map((item) => {
            const isActive = item.route === currentRoute;

            return (
              <a
                key={item.route}
                href={`#${item.route}`}
                className={`rounded-md px-4 py-2 text-sm font-medium transition ${
                  isActive
                    ? "bg-blue-600 text-white"
                    : "text-slate-600 hover:bg-slate-100 hover:text-slate-950"
                }`}
              >
                {item.label}
              </a>
            );
          })}
        </nav>
      </div>
    </header>
  );
}
