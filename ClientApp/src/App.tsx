import { useSelector } from "react-redux";
import {
  BrowserRouter as Router,
  Routes as Switch,
  Route,
  Navigate,
} from "react-router-dom";
import LoginScreen from "./Components/LoginScreen";
import NotesScreen from "./Components/NotesScreen";

export const App = () => {
  //@ts-ignore
  let login = useSelector((state) => state.login);

  return (
    <Router>
      <Switch>
        <Route path="login" element={<LoginScreen />} />

        {login.SessionId && (
          <>
            <Route path="notes" element={<NotesScreen />} />
            <Route path="*" element={<Navigate to="/notes" />} />
          </>
        )}

        {!login.SessionId && (
          <>
            <Route path="notes" element={<Navigate to="/login" />} />
            <Route path="*" element={<Navigate to="/login" />} />
          </>
        )}
      </Switch>
    </Router>
  );
};

export default App;
