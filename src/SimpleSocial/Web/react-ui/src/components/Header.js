import { Link } from "react-router-dom";
import logo from "../assets/logo_mini.png";
import styled from "styled-components";

import ProfilePictureLink from "./ProfilePictureLink";
import ThemeContext from "../context/ThemeContext";
import { useContext } from "react";

const Header = () => {
  const theme = useContext(ThemeContext);

  return (
    <StyledHeader theme={theme}>
      <Logo />
      <nav>
        <ul>
          <li>
            <Link to="/">
              <i className="fas fa-home"></i>
            </Link>
          </li>
          <li>
            <Link to="/users">
              <i className="fas fa-users"></i>
            </Link>
          </li>
        </ul>
      </nav>
      <div>
        <ProfilePictureLink
          to="/profile"
          username="Svetlin"
          src="https://images.unsplash.com/photo-1534528741775-53994a69daeb?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=50&q=80"
        />
      </div>
    </StyledHeader>
  );
};

const StyledHeader = styled.header`
  display: flex;
  align-items: center;
  justify-content: space-between;
  background: ${(props) => props.theme.secondary};
  padding: 0.5em 1em;

  & nav ul {
    display: flex;
  }

  & nav ul li {
    display: block;
    font-size: 1.7em;
    padding: 0 1em;
  }

  & nav ul li a {
    color: ${(props) => props.theme.text};
  }
`;

const Logo = styled.img`
  display: block;
  width: 150px;
`;

Logo.defaultProps = {
  src: logo,
};

export default Header;
