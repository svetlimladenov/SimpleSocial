import { Link } from "react-router-dom";

const Header = () => {
  return (
    <header className="app-header">
      <div className="header-logo">
        <h4>Logo</h4>
      </div>
      <nav className="header-tabs">
        <ul>
          <li>
            <Link to="/">
              <i className="fas fa-home"></i>
            </Link>
          </li>
          <li>
            <Link to="/">
              <i className="fas fa-users"></i>
            </Link>
          </li>
        </ul>
      </nav>
      <div className="header-user-controls">
        <Link to="/profile">
          <img
            className="logo"
            src="https://images.unsplash.com/photo-1534528741775-53994a69daeb?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=50&q=80"
            alt=""
          />
        </Link>
      </div>
    </header>
  );
};

export default Header;
