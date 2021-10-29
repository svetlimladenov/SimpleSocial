import { Switch, Route } from "react-router";
import Feed from "./components/Feed";
import Profile from "./profile/Profile";

const NavigationSwitch = () => {
  return (
    <Switch>
      <Route exact path="/">
        <Feed />
      </Route>
      <Route path="/profile">
        <Profile />
      </Route>
      <Route path="/feed">
        <Feed />
      </Route>
    </Switch>
  );
};

export default NavigationSwitch;
