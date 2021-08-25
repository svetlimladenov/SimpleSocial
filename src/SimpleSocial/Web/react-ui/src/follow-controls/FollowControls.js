import React from "react";
import SimpleSocialButton from "../shared/SimpleSocialButton/SimpleSocialButton";

function FollowControls({ currentUserId, profileId }) {
  if (currentUserId === profileId) {
    return "";
  }
  return (
    <SimpleSocialButton
      handleClick={() => console.log("Following" + profileId)}
    >
      Follow
    </SimpleSocialButton>
  );
}

export default FollowControls;
