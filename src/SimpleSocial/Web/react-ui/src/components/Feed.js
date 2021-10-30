import { useContext, useEffect, useState } from "react";
import styled from "styled-components";
import ThemeContext, { themes } from "../context/ThemeContext";

import fakePosts from "../fakeData/posts";
import Post from "./Post";

const Feed = () => {
  const theme = useContext(ThemeContext);
  const [posts, setPosts] = useState([]);

  useEffect(() => {
    setPosts(fakePosts);
  }, []);

  return (
    <StyledFeed theme={theme}>
      <Main>
        {posts.map((post) => {
          return <Post key={post.id} {...post} />;
        })}
      </Main>
      {/* <Aside></Aside> */}
    </StyledFeed>
  );
};

const StyledFeed = styled.div`
  display: flex;
  background: ${(props) => props.theme.primary};
`;

StyledFeed.defaultProps = {
  theme: themes.dark,
};

const Main = styled.main`
  display: flex;
  flex-direction: column;
  max-width: 800px;
  margin: 0 auto;
`;

export default Feed;
