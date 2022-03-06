import { AnyAction } from "redux";
import LoginObject from "../../Objects/LoginObject";

const InitialState: LoginObject = {
  Name: "",
  SessionId: "",
};

export const LoginReducer = (state = InitialState, action: AnyAction) => {
  switch (action.type) {
    case "SET_SESSION_ID":
      return { ...state, SessionId: action.payload };
    case "SET_NAME":
      return { ...state, Name: action.payload };
  }
  return state;
};

export default LoginReducer;
