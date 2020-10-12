import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter as Router, Route, Link, Switch } from "react-router-dom";
import {
  Button,
  Checkbox,
  CssBaseline,
  FormControlLabel,
  Grid,
  Paper,
  TextField,
} from "@material-ui/core";
import Login from "./components/login/Login";
import Example from "./components/Example";
import Navbar from "./components/Navbar";
import { observer } from "mobx-react";
import Register from "./components/login/Register";

function App() {
  return (
    <>
      <CssBaseline />
      <Navbar />

      <Switch>
        <Route path="/home">
          <Example />
        </Route>
        <Route path="/login">
          <Login />
        </Route>
        <Route path="/register">
          <Register />
        </Route>

        {/* <PrivateRoute path="/protected">
        <Example />
      </PrivateRoute> */}
      </Switch>
    </>
  );
}

export default observer(App);
