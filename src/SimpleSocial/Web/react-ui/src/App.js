import "./App.css";
import Footer from "./common/Footer";
import Wrapper from "./common/Wrapper";
import Navigation from "./navigation/Navigation";

const App = () => {
  return (
    <Wrapper>
      <Navigation />
      <Footer />
    </Wrapper>
  );
};

export default App;
