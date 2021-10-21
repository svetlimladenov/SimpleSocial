import { BrowserRouter as Router } from "react-router-dom";

import NavigationBar from "./NavigationBar";
import NavigationSwitch from "./NavigationSwitch";

const Navigation = () => {
  return (
    <Router>
      <NavigationBar />
      <NavigationSwitch />
    </Router>
  );
};

export default Navigation;
