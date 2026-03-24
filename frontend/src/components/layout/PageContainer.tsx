import { ReactNode } from "react";
import StatusBanner from "../common/StatusBanner";

type PageContainerProps = {
  title: string;
  description: string;
  loading: boolean;
  successMessage: string;
  errorMessage: string;
  children: ReactNode;
};

export default function PageContainer({
  title,
  description,
  loading,
  successMessage,
  errorMessage,
  children
}: PageContainerProps) {
  return (
    <main className="mx-auto w-[1400px] px-8 py-10">
      <div className="mb-8 flex items-end justify-between border-b border-slate-200 pb-5">
        <div>
          <h1 className="text-3xl font-semibold tracking-tight text-slate-950">{title}</h1>
          <p className="mt-2 text-sm text-slate-500">{description}</p>
        </div>
      </div>

      <div className="mb-6 space-y-2">
        {loading && <StatusBanner tone="info" message="Carregando dados..." />}
        {successMessage && <StatusBanner tone="success" message={successMessage} />}
        {errorMessage && <StatusBanner tone="error" message={errorMessage} />}
      </div>

      {children}
    </main>
  );
}
