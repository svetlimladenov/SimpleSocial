import React from "react";

export const themes = {
  dark: {
    primary: "#18191a",
    secondary: "#24252a",
    text: "#fff",
  },
  light: {
    primary: "#f0f2f5",
    secondary: "#fff",
    text: "#65676b",
  },
};

const ThemeContext = React.createContext(themes.dark);

export default ThemeContext;
