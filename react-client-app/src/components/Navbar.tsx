import React, { useContext } from "react";
import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import IconButton from "@material-ui/core/IconButton";
import MenuIcon from "@material-ui/icons/Menu";
import LinkRouteButton from "./common/LinkRouteButton";
import { StoreContext } from "../stores/StoreContext";
import { Chip } from "@material-ui/core";
import { observer } from "mobx-react";
import MeetingRoomIcon from "@material-ui/icons/MeetingRoom";
import { useHistory } from "react-router";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      flexGrow: 1,
    },
    menuButton: {
      marginRight: theme.spacing(2),
    },
    title: {
      flexGrow: 1,
    },
    userLabel: {
      marginLeft: theme.spacing(4),
    },
  })
);

const Navbar = () => {
  const classes = useStyles();
  const history = useHistory();
  const { user, logout } = useContext(StoreContext).userStore;

  const handleClickLogout = () => {
    const location = {
      pathname: "/login",
    };
    logout();
    history.push(location);
  };

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <IconButton
            edge="start"
            className={classes.menuButton}
            color="inherit"
            aria-label="menu"
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" className={classes.title}>
            Example SPA app
          </Typography>

          <LinkRouteButton to="/">Home</LinkRouteButton>
          <LinkRouteButton to="products">Products</LinkRouteButton>

          {user?.username ? (
            <>
              <Chip
                className={classes.userLabel}
                label={`Hello ${user.username}!`}
              />
              <Chip
                label="Logout"
                clickable
                color="primary"
                onClick={handleClickLogout}
                icon={<MeetingRoomIcon />}
              />
            </>
          ) : (
            <LinkRouteButton to="login">Login</LinkRouteButton>
          )}
        </Toolbar>
      </AppBar>
    </div>
  );
};

export default observer(Navbar);
