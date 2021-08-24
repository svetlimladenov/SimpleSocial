import "./App.css";
import UserProfile from "./user-profile";
import Container from "./shared/Container/Container";

function App() {
  return (
    <div className="body-wrapper ">
      <Container>
        <UserProfile userId={3} />
      </Container>
    </div>
  );
}

export default App;
