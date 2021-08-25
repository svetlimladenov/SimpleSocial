import React from "react";
import styles from "./post-header.module.css";

import ProfilePicture from "../../../shared/ProfilePicture/ProfilePicture";

function PostHeader({ username, createdOn, profilePictureSrc }) {
  return (
    <div className={styles["header-wrapper"]}>
      <div className={styles["left-header"]} data-attr={createdOn}>
        <ProfilePicture
          width="48px"
          height="48px"
          src={profilePictureSrc}
          alt={username}
        />
        <div className={styles["user-name"]}>
          <a href="/">{username}</a>
        </div>
      </div>
      <div className={styles["right-header"]}></div>
    </div>
  );
}

export default PostHeader;
