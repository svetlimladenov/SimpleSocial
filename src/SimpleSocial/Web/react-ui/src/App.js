import { BrowserRouter as Router } from "react-router-dom";
import ThemeContext, { themes } from "./context/ThemeContext";
import "./App.css";
import Footer from "./components/Footer";
import Header from "./components/Header";
import Pages from "./Pages";

const App = () => {
  return (
    <ThemeContext.Provider value={themes.dark}>
      <Router>
        <Header />
        <Pages />
      </Router>
      <Footer />
    </ThemeContext.Provider>
  );
};

export default App;
