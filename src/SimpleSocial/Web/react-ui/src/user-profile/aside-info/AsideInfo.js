import React from "react";
import styles from "./aside-info.module.css";
import ProfilePicture from "../../shared/ProfilePicture/ProfilePicture";
import UserDetails from "../user-details/UserDetails";
import SimpleSocialButton from "../../shared/SimpleSocialButton/SimpleSocialButton";

class AsideInfo extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      userInfo: {
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
    fetch(`https://localhost:5003/Account/GetUserBoxInfo/${this.props.userId}`)
      .then((res) => res.json())
      .then((data) => {
        this.setState({
          userInfo: {
            username: data.username,
            profilePictureSrc: data.profilePictureUrl,
            details: {
              fullname: data.fullName,
              location: data.location,
              age: data.age,
              birthday: data.birthday
            }
          }
        });
      });
  }

  render() {
    const { username, profilePictureSrc, details } = this.state.userInfo;

    return (
      <div className={`row text-center ${styles["user-info"]}`}>
        <ProfilePicture src={profilePictureSrc} alt={username} />
        <h2>{username}</h2>
        <UserDetails details={details} />
        <SimpleSocialButton>Follow</SimpleSocialButton>
      </div>
    );
  }
}

export default AsideInfo;
