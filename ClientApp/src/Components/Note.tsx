import axios from "axios";
import { useState } from "react";
import { Card, Button } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { Add } from "../Redux/Actions/CounterAction";
import "./styles.css";

export const Note = (props: any) => {
  const [name, setName] = useState(props.name);
  const [content, setContent] = useState(props.content);
  const [id, setId] = useState(props.nid);

  //@ts-ignore
  const login = useSelector((state) => state.login);

  const dispatch = useDispatch();

  return (
    <Card style={{ width: "18rem" }} className="noteSpace">
      <Card.Body>
        <Card.Title>{name}</Card.Title>
        <Card.Text>{content}</Card.Text>
      </Card.Body>
      <Button
        variant="danger"
        onClick={() => {
          axios
            .delete(`/api/DeleteNote?sessionId=${login.SessionId}&noteId=${id}`)
            .then((response) => {
              dispatch(Add());
            });
        }}
      >
        Delete
      </Button>
    </Card>
  );
};

export default Note;
