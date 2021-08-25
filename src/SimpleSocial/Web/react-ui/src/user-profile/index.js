import React from "react";
import Row from "../shared/Row/Row";
import AsideUserInfo from "./aside-info/AsideInfo";
import Feed from "../feed/Feed";
import { loadUserData } from "./services/user-services";

class UserProfile extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      userInfo: {
        id: this.props.userId,
        username: "",
        profilePictureSrc: "",
        details: {
          fullname: "",
          location: "",
          age: "",
          birthday: ""
        }
      }
    };
  }

  componentDidMount() {
    loadUserData(this.props.userId).then((data) => {
      this.setState((state) => {
        return {
          userInfo: {
            userId: state.userInfo.id,
            username: data.username,
            profilePictureSrc: data.profilePictureUrl,
            details: {
              fullname: data.fullName,
              location: data.location,
              age: data.age,
              birthday: data.birthday
            }
          }
        };
      });
    });
  }

  render() {
    const { userInfo } = this.state;
    return (
      <Row>
        <div className="col-md-3">
          <AsideUserInfo userInfo={userInfo} />
        </div>
        <div className="col-md-6">
          <Feed userInfo={userInfo} />
        </div>
        <div className="col-md-3">
          <AsideUserInfo userInfo={userInfo} />
        </div>
      </Row>
    );
  }
}

export default UserProfile;
