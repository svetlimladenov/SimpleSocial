import React from "react";
import styles from "./aside-info.module.css";
import ProfilePicture from "../../shared/ProfilePicture/ProfilePicture";
import UserDetails from "../user-details/UserDetails";
import SimpleSocialButton from "../../shared/SimpleSocialButton/SimpleSocialButton";

class AsideInfo extends React.Component {
  render() {
    const { username, profilePictureSrc, details } = this.props.userInfo;

    return (
      <div className={`row text-center ${styles["user-info"]}`}>
        <ProfilePicture
          width="210px"
          height="210px"
          src={profilePictureSrc}
          alt={username}
        />
        <h2>{username}</h2>
        <UserDetails details={details} />
        <SimpleSocialButton>Follow</SimpleSocialButton>
      </div>
    );
  }
}

export default AsideInfo;
