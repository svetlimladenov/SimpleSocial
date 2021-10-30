import { BrowserRouter as Router } from "react-router-dom";
import "./App.css";
import Footer from "./components/Footer";
import Header from "./components/Header";
import Pages from "./Pages";

const App = () => {
  return (
    <>
      <Router>
        <Header />
        <Pages />
      </Router>
      <Footer />
    </>
  );
};

export default App;
