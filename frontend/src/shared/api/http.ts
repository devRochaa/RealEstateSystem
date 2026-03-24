const apiUrl = (import.meta.env.VITE_API_URL ?? "").replace(/\/+$/, "");

export async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const url = `${apiUrl}${path}`;

  const response = await fetch(url, {
    ...init,
    headers: {
      "Content-Type": "application/json",
      ...(init?.headers ?? {})
    }
  });

  if (!response.ok) {
    let message = "Não foi possível concluir a requisição.";

    try {
      const errorBody = (await response.json()) as { message?: string };
      if (errorBody.message) {
        message = errorBody.message;
      }
    } catch {
      message = response.statusText || message;
    }

    throw new Error(message);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return (await response.json()) as T;
}
