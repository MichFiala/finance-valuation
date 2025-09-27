// src/i18n/config.ts

// Core i18next library.
import i18n from "i18next";
// Bindings for React: allow components to
// re-render when language changes.
import HttpApi from "i18next-http-backend";
import { initReactI18next } from "react-i18next";

 export const supportedLngs = {
   en: "English",
   cs: "Čeština",
 };

i18n.use(HttpApi)
  .use(initReactI18next)
  .init({
    lng: "en",
    fallbackLng: "en",
    debug: true,
    supportedLngs: Object.keys(supportedLngs),
    interpolation: {
      escapeValue: false,
    }
  });

export default i18n;