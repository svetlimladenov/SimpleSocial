import styled from "styled-components";
import ProfilePictureLink from "./ProfilePictureLink";

const Post = ({ id, user, content, comments }) => {
  return (
    <StyledPost>
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
  background: #eee;
  border-radius: 0.3em;
  margin: 2em 0;
  padding: 1em 1em;

  & header {
    display: flex;
    align-items: center;
  }

  & .post-controls {
    cursor: pointer;
    color: black;
    font-weight: bold;
    border: none;
    background: inherit;
    margin-left: auto;
  }
`;

export default Post;
