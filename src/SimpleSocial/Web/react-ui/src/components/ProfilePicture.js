import { Link } from "react-router-dom";
import styled from "styled-components";

const ProfilePictureLink = ({ to, src, username }) => {
  return (
    <StyledLink to={to}>
      <img src={src} alt={username} />
    </StyledLink>
  );
};

const StyledLink = styled(Link)`
  display: block;
  width: 50px;
  position: relative;
  overflow: hidden;
  border-radius: 100%;

  &:before {
    display: block;
    content: "";
    padding-bottom: 100%;
  }

  & > img {
    display: block;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }
`;

export default ProfilePictureLink;
