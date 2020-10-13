import { HubConnectionBuilder } from "@aspnet/signalr";
import { HubConnection } from "@aspnet/signalr/dist/esm/HubConnection";
import { Button, FilledInput, Input, InputLabel, TextField } from "@material-ui/core";
import React, { ReactElement, useEffect, useState } from "react";

interface Props {}

function Chat({}: Props): ReactElement {
  const [hubConnection, sethubConnection] = useState<HubConnection>(
    new HubConnectionBuilder().withUrl("https://localhost:44328/chat").build()
  );
  const [messages, setmessages] = useState<String[]>([]);

  const [cahtMess, setcahtMess] = useState('')

  useEffect(() => {
    hubConnection
      .start()
      .then(() => console.log("Connection started!"))
      .catch((err) => console.log("Error while establishing connection :("));

    hubConnection.on(
      "sendToAll",
      (nick , receivedMessage ) => {
        const text = `${receivedMessage}`;

        const messages2 = messages.concat(text);
        // setmessages(messages2);

        setmessages((prev) => prev.concat(text));
      }
    );

    return () => {};
  }, []);

  const sendMessage = () => {
    hubConnection
      .invoke("sendToAll", cahtMess, cahtMess)
      .catch((err) => console.error(err));

    // this.setState({ message: "" });
  };

  const messegesComponents = messages.map((m, i) => <p key={i}>{m}</p>);
  return (
    <div>
        <TextField onChange={e=>{setcahtMess(e.target.value)}}>{cahtMess}</TextField>
      <Button onClick={sendMessage}>Send message</Button>
      {messegesComponents}
    </div>
  );
}

export default Chat;
