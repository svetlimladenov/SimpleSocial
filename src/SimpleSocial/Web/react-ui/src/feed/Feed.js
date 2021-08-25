import React from "react";
import CreatePost from "../posts/CreatePost/CreatePost";
import Post from "../posts/Post/Post";
import { fetchUserPosts } from "../posts/services/post-services";

class Feed extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      newPost: {
        content: ""
      },
      posts: []
    };

    this.handlePostInput = this.handlePostInput.bind(this);
    this.handlePostSubmit = this.handlePostSubmit.bind(this);
    this.renderPosts = this.renderPosts.bind(this);
    this.fetchPosts = this.fetchPosts.bind(this);
  }

  componentDidMount() {
    this.fetchPosts();
  }

  fetchPosts() {
    const { userInfo } = this.props;
    if (userInfo.userId) {
      fetchUserPosts(userInfo.userId).then((data) => {
        this.setState((state) => {
          return {
            posts: [...state.posts, ...data]
          };
        });
      });
    }
  }

  handlePostSubmit(e) {
    e.preventDefault();
    this.setState((state) => {
      return {
        posts: [
          ...state.posts,
          { id: state.posts.length + 20, content: state.newPost.content }
        ]
      };
    });
  }

  handlePostInput(e) {
    this.setState({
      newPost: {
        content: e.target.value
      }
    });
  }

  renderPosts() {
    return this.state.posts.map((post) => {
      return <Post key={post.id} post={post} />;
    });
  }

  render() {
    return (
      <div>
        <CreatePost
          handlePostSubmit={this.handlePostSubmit}
          handlePostInput={this.handlePostInput}
        />
        {this.renderPosts()}
      </div>
    );
  }
}

export default Feed;
