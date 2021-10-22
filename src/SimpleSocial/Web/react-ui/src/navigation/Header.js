import { BrowserRouter as Router } from "react-router-dom";

import Navigation from "./Navigation";
import NavigationSwitch from "./NavigationSwitch";

const Header = () => {
  return (
    <Router>
      <Navigation />
      <NavigationSwitch />
    </Router>
  );
};

export default Header;
