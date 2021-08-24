import React from "react";
import styles from "./user-details.module.css";

function UserDetails({ details }) {
  return <div className={styles["user-details"]}>{renderDetails(details)}</div>;
}

function renderDetails(details) {
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
  return "";
}

function generateHeaderText(title) {
  return title[0].toUpperCase() + title.slice(1, title.length);
}

export default UserDetails;
