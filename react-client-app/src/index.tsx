import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import UserStore from "./stores/UserStore";
import { Provider } from "mobx-react";
import { StoreProvider } from "./stores/StoreContext";
import { RootStore } from "./stores/RootStore";
import { BrowserRouter as Router, Route, Link } from "react-router-dom";

const stores = {
  userStore: new UserStore(),
};

ReactDOM.render(
  <React.StrictMode>
    <StoreProvider store={new RootStore()}>
      <Router>
        <App />
      </Router>
    </StoreProvider>
  </React.StrictMode>,
  document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
