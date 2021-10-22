import { Link } from "react-router-dom";

import logo from "../assets/logo.png";
import styles from "./style.module.css";

const Navigation = () => {
  return (
    <header>
      <Link to="/">
        <img className={styles["header-logo"]} src={logo} alt="logo" />
      </Link>
      <nav className={styles["app-navigation"]}>
        <ul>
          <li>
            <Link to="/profile">Profile</Link>
          </li>
        </ul>
      </nav>
    </header>
  );
};

export default Navigation;
