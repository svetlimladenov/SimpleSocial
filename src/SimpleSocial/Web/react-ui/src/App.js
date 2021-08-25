import "./App.css";
import MyProfile from "./my-profile/MyProfile";
import Container from "./shared/Container/Container";

function App() {
  return (
    <div className="body-wrapper">
      <Container>
        <MyProfile userId={3} />
      </Container>
    </div>
  );
}

export default App;
