import React from "react";
import Row from "../shared/Row/Row";
import AsideUserInfo from "./aside-info/AsideInfo";

class UserProfile extends React.Component {
  render() {
    const { userId } = this.props;
    return (
      <Row>
        <div className="col-md-3">
          <AsideUserInfo userId={userId} />
        </div>
        <div className="col-md-6">Middle</div>
        <div className="col-md-3">
          <AsideUserInfo userId={2} />
        </div>
      </Row>
    );
  }
}

export default UserProfile;
