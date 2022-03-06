import axios, { AxiosError } from "axios";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { SetName, SetSessionId } from "../Redux/Actions/LoginAction";
import { useNavigate } from "react-router-dom";
import Bar from "./Bar";
import { Button, Form } from "react-bootstrap";

export const LoginScreen = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const dispatch = useDispatch();

  let navigate = useNavigate();

  return (
    <>
      <Bar />
      <Form>
        <Form.Group className="mb-3">
          <Form.Label>Username</Form.Label>
          <Form.Control
            type="text"
            placeholder="Username"
            onChange={(event) => {
              setUsername(event.target.value);
            }}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Password</Form.Label>
          <Form.Control
            type="password"
            placeholder="Password"
            onChange={(event) => {
              setPassword(event.target.value);
            }}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Button
            variant="primary"
            onClick={() => {
              axios
                .post("/api/Login", {
                  Name: username,
                  Password: password,
                })
                .then((response) => {
                  dispatch(SetSessionId(response.data));
                  dispatch(SetName(username));
                  navigate("/notes");
                })
                .catch((e: AxiosError) => {
                  alert(e.response?.data);
                });
            }}
          >
            Login
          </Button>

          <Button
            variant="primary"
            onClick={() => {
              axios
                .post("/api/Register", {
                  Name: username,
                  Password: password,
                })
                .then((response) => {
                  dispatch(SetSessionId(response.data));
                  dispatch(SetName(username));
                  alert("Successfully registered.");
                  navigate("/notes");
                })
                .catch((e: AxiosError) => {
                  alert("User already exists.");
                });
            }}
          >
            Register
          </Button>
        </Form.Group>
      </Form>
    </>
  );
};

export default LoginScreen;
