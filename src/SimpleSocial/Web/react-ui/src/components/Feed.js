import { useEffect, useState } from "react";
import styled from "styled-components";

import fakePosts from "../fakeData/posts";
import Post from "./Post";

const Feed = () => {
  const [posts, setPosts] = useState([]);

  useEffect(() => {
    setPosts(fakePosts);
  }, []);

  return (
    <StyledFeed>
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
`;

const Main = styled.main`
  display: flex;
  flex-direction: column;
  max-width: 800px;
  margin: 0 auto;
`;

const Aside = styled.aside``;

export default Feed;
