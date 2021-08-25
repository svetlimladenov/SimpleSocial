import React from "react";
import Row from "../shared/Row/Row";
import AsideUserBox from "../user/aside-user-box/AsideUserBox";
import Feed from "../feed/Feed";
import { loadUserData } from "../user/services/user-services";

class MyProfile extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      userInfo: {
        userId: props.userId,
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
            userId: state.userInfo.userId,
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
          <AsideUserBox {...userInfo} />
        </div>
        <div className="col-md-6">
          <Feed userInfo={userInfo} />
        </div>
        <div className="col-md-3">
          <AsideUserBox {...userInfo} />
        </div>
      </Row>
    );
  }
}

export default MyProfile;
