import { HubConnectionBuilder } from "@aspnet/signalr";
import { HubConnection } from "@aspnet/signalr/dist/esm/HubConnection";
import {
  Box,
  Button,
  List,
  makeStyles,
  Paper,
  TextField,
  Typography,
} from "@material-ui/core";
import moment from "moment";
import React, { ReactElement, useContext, useEffect, useState } from "react";
import { useRef } from "react";
import AppConfig from "../../appconfig";
import { StoreContext } from "../../stores/StoreContext";
import ChatMessage from "./ChatMessage";

const useStyles = makeStyles((theme) => ({
  main: {
    marginTop: theme.spacing(4),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  box: {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    padding: theme.spacing(4),
  },
  messagebox: {
    paddingTop: 0,
    boxShadow: "inset 5px 5px 15px 5px rgba(0,0,0,0)",
  },
  inpuitMessage: {
    width: "100%",
  },
  chatBox: {
    width: 450,
    maxHeight: 400,
    minHeight: 400,
    marginTop: theme.spacing(2),
    marginBottom: theme.spacing(2),
    backgroundColor: theme.palette.background.default,
    boxShadow:
      "inset rgba(0, 0, 0, 0.2) 0px 3px 3px -2px, inset rgba(0, 0, 0, 0.14) 0px 3px 4px 0px, inset rgba(0, 0, 0, 0.12) 0px 1px 8px 0px",
  },
}));

interface ChatMessageModel {
  message: string;
  user: string;
  time: string;
}

const defaultMessages: ChatMessageModel[] = [
  {
    message:
      "Chat 💬 with other website users & feel power of websockets. You can also open the page in a different browser and chat from both!",
    user: "Bot",
    time: moment().format("HH:mm:ss"),
  },
];

function Chat(): ReactElement {
  const classes = useStyles();

  const [hubConnection] = useState<HubConnection>(
    new HubConnectionBuilder().withUrl(AppConfig.apiUrl + "/chat").build()
  );
  const { user } = useContext(StoreContext).userStore;

  const [messages, setmessages] = useState<ChatMessageModel[]>(defaultMessages);

  const [chatMessage, setChatMessage] = useState("");
  const [error, setError] = useState("");
  const nick = user?.userFullName || "Anonymous";

  const box = useRef<HTMLDivElement>();

  useEffect(() => {
    if (box.current) {
      box.current.scrollTop = box.current?.scrollHeight;
    }
  }, [messages]);

  useEffect(() => {
    hubConnection
      .start()
      .then(() => console.log("Connection started!"))
      .catch(() => console.log("Error while establishing connection :("));

    hubConnection.on("sendToAll", (user, message, time) => {
      setmessages((prev) => prev.concat({ message, user, time }));
    });

    return () => {
      hubConnection.stop();
    };
  }, [hubConnection]);

  const sendMessage = () => {
    if (validate(chatMessage)) {
      hubConnection
        .invoke("sendToAll", nick, chatMessage)
        .then(() => {
          setChatMessage("");
        })
        .catch((err) => console.error(err));
    }
  };

  const handleOnPressedEnter = (e: React.KeyboardEvent) => {
    if (e.key === "Enter") {
      sendMessage();
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setChatMessage(e.target.value);
    validate(e.target.value);
  };

  const validate = (message: string): boolean => {
    const errorMessage = message ? "" : "Write something.";
    setError(errorMessage);
    return !errorMessage;
  };

  const messegesComponents = messages.map((m, i) => (
    <ChatMessage key={i} nick={m.user} message={m.message} time={m.time} />
  ));

  return (
    <div className={classes.main}>
      <Paper className={classes.box}>
        <Typography color="primary" variant="h4" component="h5">
          Chat Box
        </Typography>
        <Box
          {...{ ref: box }}
          boxShadow="3"
          className={classes.chatBox}
          overflow="auto"
        >
          <List className={classes.messagebox}>{messegesComponents}</List>
        </Box>
        <TextField
          error={!!error}
          helperText={error}
          onChange={handleChange}
          onKeyUp={handleOnPressedEnter}
          className={classes.inpuitMessage}
          value={chatMessage}
          autoFocus
        />
        <Button onClick={sendMessage}>Send message</Button>
      </Paper>
    </div>
  );
}

export default Chat;
