import { ReactNode } from "react";

type SimpleTableProps = {
  columns: string[];
  emptyMessage: string;
  children: ReactNode;
  hasRows: boolean;
  footer?: ReactNode;
};

export default function SimpleTable({
  columns,
  emptyMessage,
  children,
  hasRows,
  footer
}: SimpleTableProps) {
  return (
    <div className="overflow-x-auto">
      <table className="w-full border-collapse text-sm">
        <thead>
          <tr className="border-b border-dashed border-slate-300 text-left text-xs font-semibold uppercase tracking-wide text-slate-500">
            {columns.map((column) => (
              <th key={column} className="py-3 pr-4">
                {column}
              </th>
            ))}
          </tr>
        </thead>
        <tbody className="text-slate-800">
          {hasRows ? (
            children
          ) : (
            <tr>
              <td colSpan={columns.length} className="py-6 text-center text-slate-500">
                {emptyMessage}
              </td>
            </tr>
          )}
        </tbody>
        {footer && (
          <tfoot className="border-t border-dashed border-slate-300 font-semibold text-slate-950">
            {footer}
          </tfoot>
        )}
      </table>
    </div>
  );
}
