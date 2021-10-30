import React from "react";

export const themes = {
  dark: {
    primary: "#18191a",
    secondary: "#24252a",
    text: "#fff",
  },
};

const ThemeContext = React.createContext(themes.dark);

export default ThemeContext;
