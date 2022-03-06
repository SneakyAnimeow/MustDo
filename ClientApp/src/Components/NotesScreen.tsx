import axios, { AxiosError } from "axios";
import { useEffect, useState } from "react";
import { Alert, Button, Form } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import NoteObject from "../Objects/NoteObject";
import { Add } from "../Redux/Actions/CounterAction";
import { SetSessionId } from "../Redux/Actions/LoginAction";
import Bar from "./Bar";
import Note from "./Note";
import "./styles.css";

export const NotesScreen = () => {
  const [name, setName] = useState("");
  const [content, setContent] = useState("");
  const [notes, setNotes] = useState<Array<NoteObject>>();
  const [addNoteVisible, setAddNoteVisible] = useState(false);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  //@ts-ignore
  const login = useSelector((state) => state.login);

  //@ts-ignore
  const counter = useSelector((state) => state.counter);

  const dispatch = useDispatch();

  const navigate = useNavigate();

  useEffect(() => {
    axios.get(`/api/GetNotes?sessionId=${login.SessionId}`).then((response) => {
      let notesArray = Array<NoteObject>();
      for (let i = 0; i < response.data.length; i++) {
        notesArray.push({
          Name: response.data[i].name,
          Content: response.data[i].content,
          Id: response.data[i].id,
        });
      }
      setNotes(Array<NoteObject>());
      setNotes(notesArray);
    });
  }, [dispatch, counter]);

  return (
    <>
      <Bar />
      <Alert variant="info">
        <Alert.Heading>
          <p>Welcome back {login.Name}!</p>
        </Alert.Heading>
        <div className="oneLine">
          <Button
            variant="success"
            onClick={() => {
              setAddNoteVisible(true);
            }}
          >
            + Add note
          </Button>{" "}
          <Button
            variant="danger"
            onClick={() => {
              dispatch(SetSessionId(""));
            }}
          >
            Log out
          </Button>{" "}
        </div>
      </Alert>

      {addNoteVisible && (
        <Alert
          variant="dark"
          onClose={() => setAddNoteVisible(false)}
          dismissible
        >
          <Alert.Heading>Add note</Alert.Heading>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label>Name</Form.Label>
              <Form.Control
                type="text"
                placeholder="Name"
                onChange={(event) => {
                  setName(event.target.value);
                }}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Content</Form.Label>
              <Form.Control
                type="text"
                placeholder="Content"
                onChange={(event) => {
                  setContent(event.target.value);
                }}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Button
                variant="primary"
                onClick={() => {
                  axios
                    .post(`/api/AddNote?sessionId=${login.SessionId}`, {
                      Name: name,
                      Content: content,
                    })
                    .then((response) => {
                      dispatch(Add());
                    })
                    .catch((e: AxiosError) => {
                      alert(e.response?.data);
                      if (e.response?.data + "" === "Session expired.") {
                        dispatch(SetSessionId(""));
                        navigate("/login");
                      }
                    });
                }}
              >
                Add
              </Button>
            </Form.Group>
          </Form>
        </Alert>
      )}

      <div className="grid">
        {notes?.map((note) => {
          return <Note name={note.Name} content={note.Content} nid={note.Id} />;
        })}
      </div>
    </>
  );
};

export default NotesScreen;
