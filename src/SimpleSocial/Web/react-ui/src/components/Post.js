import { useContext } from "react";
import styled from "styled-components";
import ThemeContext from "../context/ThemeContext";
import ProfilePictureLink from "./ProfilePictureLink";

const Post = ({ id, user, content, comments }) => {
  const theme = useContext(ThemeContext);

  return (
    <StyledPost theme={theme}>
      <header>
        <ProfilePictureLink
          to={`/${user.name}`}
          src={user.profilePicture}
          username={user.name}
        />
        <div>{user.name}</div>
        <button className="post-controls">...</button>
      </header>
      <section>
        <p>{content}</p>
      </section>
    </StyledPost>
  );
};

const StyledPost = styled.article`
  display: flex;
  flex-direction: column;
  background: ${(props) => props.theme.secondary};
  border-radius: 0.7em;
  margin: 0.5em 0;
  padding: 1em 1em;
  color: ${(props) => props.theme.text};

  & header {
    display: flex;
    align-items: center;
  }

  & .post-controls {
    cursor: pointer;
    color: ${(props) => props.theme.text};
    font-weight: bold;
    border: none;
    background: inherit;
    margin-left: auto;
  }
`;

export default Post;
