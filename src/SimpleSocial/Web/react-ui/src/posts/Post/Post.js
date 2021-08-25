import React from "react";
import styles from "./post.module.css";
import PostHeader from "./PostHeader/PostHeader";

class Post extends React.Component {
  render() {
    const { content, createdOn } = this.props.post;
    return (
      <div className={styles["single-post"]}>
        <PostHeader
          createdOn={createdOn}
          profilePictureSrc={
            "http://res.cloudinary.com/svetlinmld/image/upload/v1629809928/3_Profile_Picture.jpg"
          }
          username="SvetlinMld"
        />
        <div className={styles.content}>{content}</div>
      </div>
    );
  }
}

export default Post;
