interface ImportMetaEnv {
  readonly VITE_APP_API_URL: string;
  readonly VITE_APP_RETURN_URL: string;
  // další proměnné můžeš přidat sem
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}