type StatusBannerProps = {
  tone: "info" | "success" | "error";
  message: string;
};

const toneClasses: Record<StatusBannerProps["tone"], string> = {
  info: "text-slate-600",
  success: "text-blue-700",
  error: "text-slate-950"
};

export default function StatusBanner({ tone, message }: StatusBannerProps) {
  return (
    <div className={`text-sm font-medium ${toneClasses[tone]}`}>
      {message}
    </div>
  );
}
