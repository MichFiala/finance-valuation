import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { createTheme, ThemeProvider } from "@mui/material";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
const theme = createTheme({
  colorSchemes: {
    dark: {
      palette: {
        background: {
          default: "#0E1113"
        },
        primary: {
          main: "#0E1113",
        },
        secondary: {
          main: "#0f12151a",
        },
        text: {
          primary: "#fff",
          secondary: "#000",
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
          secondary: "#fff",
        },
      },
    },
  },
});


root.render(
  <React.StrictMode>
    <ThemeProvider theme={theme} defaultMode="dark">
      <App />
    </ThemeProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
