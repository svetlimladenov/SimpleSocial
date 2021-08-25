import React from "react";
import styles from "./aside-user-box.module.css";
import ProfilePicture from "../../shared/ProfilePicture/ProfilePicture";
import FollowControls from "../../follow-controls/FollowControls";

function AsideUserBox({ userId, username, profilePictureSrc, details }) {
  return (
    <div className={`row text-center ${styles["user-info"]}`}>
      <ProfilePicture
        width="210px"
        height="210px"
        src={profilePictureSrc}
        alt={username}
      />
      <h2>{username}</h2>
      <AsideUserBoxDetails details={details} />
      <FollowControls currentUserId={userId} profileId={userId} />
    </div>
  );
}

function AsideUserBoxDetails({ details }) {
  return <div className={styles["user-details"]}>{renderDetails(details)}</div>;
}

function renderDetails(details) {
  if (!details) {
    return;
  }
  return Object.keys(details).map((key, idx) => {
    if (!details[key]) {
      return null;
    }

    const headerIcon = generateHeaderIcon(key);
    const headerText = generateHeaderText(key);

    return (
      <div key={idx}>
        <div>
          <b>
            {headerIcon}
            {headerText}
          </b>
        </div>
        <div>{details[key]}</div>
      </div>
    );
  });
}

function generateHeaderIcon(title) {
  const headerIcons = {
    birthday: "fas fa-birthday-cake",
    location: "fas fa-map-marker-alt"
  };

  if (headerIcons.hasOwnProperty(title)) {
    return <i className={headerIcons[title]}></i>;
  }
}

function generateHeaderText(title) {
  return title[0].toUpperCase() + title.slice(1, title.length);
}

export default AsideUserBox;
