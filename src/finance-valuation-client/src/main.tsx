import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { createTheme, ThemeProvider } from "@mui/material";
import "./i18n/config.ts";

const locale = navigator.language;

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
const theme = createTheme({
  colorSchemes: {
    dark: {
      palette: {
        background: {
          default: "#0E1113",
        },
        primary: {
          main: "#0E1113",
        },
        secondary: {
          main: "#0f12151a",
        },
        text: {
          primary: "#fff",
          secondary: "#fff",
        },
      },
    },
    light: {
      palette: {
        primary: {
          main: "#f0f0f0",
        },
        secondary: {
          main: "#f0f0f0",
        },
        text: {
          primary: "#000",
          secondary: "#000",
        },
      },
    },
  },
});

root.render(
  <React.StrictMode>
    <React.Suspense fallback={<div>Loading...</div>}>
      <ThemeProvider theme={theme} defaultMode="dark">
        <App />
      </ThemeProvider>
    </React.Suspense>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
