import "bootstrap/dist/css/bootstrap.css";
import ReactDOM from "react-dom";
import App from "./App";
import * as serviceWorkerRegistration from "./serviceWorkerRegistration";
import reportWebVitals from "./reportWebVitals";
import { combineReducers, createStore } from "redux";
import { Provider } from "react-redux";
import CounterReducer from "./Redux/Reducers/CounterReducer";
import LoginReducer from "./Redux/Reducers/LoginReducer";

const rootElement = document.getElementById("root");

const store = createStore(
  combineReducers({
    counter: CounterReducer, //For enforcing updates
    login: LoginReducer,
  }),
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  rootElement
);

serviceWorkerRegistration.unregister();

reportWebVitals();
