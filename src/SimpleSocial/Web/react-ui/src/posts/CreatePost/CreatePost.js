import React from "react";
import styles from "./create-post.module.css";

class CreatePost extends React.Component {
  render() {
    const { content, handlePostInput, handlePostSubmit } = this.props;
    return (
      <div className={styles["create-post-wrapper"]}>
        <form onSubmit={handlePostSubmit}>
          <textarea
            placeholder="What's up today?"
            cols="70"
            rows="4"
            value={content}
            onChange={handlePostInput}
          ></textarea>
          <div className={styles["post-functions"]}>
            <div className={styles["icons"]}></div>
            <div className={styles["functions"]}>
              <button className={styles["submit-button"]}>Post</button>
            </div>
          </div>
        </form>
      </div>
    );
  }
}

export default CreatePost;
