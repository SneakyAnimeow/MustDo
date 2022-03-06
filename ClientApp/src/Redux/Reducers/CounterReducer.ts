import { AnyAction } from "redux";

export const CounterReducer = (state = 0, action: AnyAction) => {
  switch (action.type) {
    case "ADD":
      return state + 1;
    case "SUB":
      return state - 1;
  }
  return state;
};

export default CounterReducer;
