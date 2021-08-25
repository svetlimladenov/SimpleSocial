import React from "react";
import styles from "./profile-picture.module.css";

class ProfilePicture extends React.Component {
  render() {
    const { src, alt, width, height } = this.props;

    return (
      <div className={styles["profile-picture"]}>
        <img
          src={src}
          alt={alt}
          className={styles.profilePicture}
          width={width}
          height={height}
        />
      </div>
    );
  }
}

export default ProfilePicture;
