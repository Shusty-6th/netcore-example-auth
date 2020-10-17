import {
  Divider,
  ListItem,
  ListItemAvatar,
  Avatar,
  ListItemText,
  Typography,
  makeStyles,
} from "@material-ui/core";
import { deepPurple } from "@material-ui/core/colors";
import React, { ReactElement } from "react";

const useStyles = makeStyles((theme) => ({
  purple: {
    color: theme.palette.getContrastText(deepPurple[500]),
    backgroundColor: deepPurple[500],
  },
  typo: {
    wordWrap: "break-word",
  },
}));

interface Props {
  message: string;
  nick: string;
  time: string;
}

function ChatMessage({ message, nick, time }: Props): ReactElement {
  const classes = useStyles();

  return (
    <>
      <Divider variant="inset" component="li" />
      <ListItem alignItems="flex-start">
        <ListItemAvatar>
          <Avatar className={classes.purple} alt="user">
            {nick[0].toUpperCase()}
          </Avatar>
        </ListItemAvatar>
        <ListItemText
          className={classes.typo}
          primary={message}
          secondary={
            <React.Fragment>
              <Typography component="span" variant="body2" color="textPrimary">
                {time} - {nick}
              </Typography>
            </React.Fragment>
          }
        />
      </ListItem>
    </>
  );
}

export default ChatMessage;
