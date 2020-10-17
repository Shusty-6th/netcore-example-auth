import { HubConnectionBuilder } from "@aspnet/signalr";
import { HubConnection } from "@aspnet/signalr/dist/esm/HubConnection";
import { Button, List, makeStyles, Paper, TextField } from "@material-ui/core";
import moment from "moment";
import React, { ReactElement, useContext, useEffect, useState } from "react";
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
    width: 450,
    maxHeight: 500,
  },
  inpuitMessage: {
    width: "100%",
  },
}));

interface ChatMessageModel {
  message: string;
  user: string;
  time: string;
}

const defaultMessages: ChatMessageModel[] = [
  {
    message: "Chat 💬 with other website users & feel power of websockets",
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
        <TextField
          error={!!error}
          helperText={error}
          onChange={handleChange}
          onKeyUp={handleOnPressedEnter}
          className={classes.inpuitMessage}
          value={chatMessage}
        />
        <Button onClick={sendMessage}>Send message</Button>

        <List className={classes.messagebox} style={{ overflow: "auto" }}>
          {messegesComponents}
        </List>
      </Paper>
    </div>
  );
}

export default Chat;
