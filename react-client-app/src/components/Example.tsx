import { Button, Container, makeStyles, Paper, Typography } from "@material-ui/core";
import { observer } from "mobx-react";
import React, { useState } from "react";
import { useContext } from "react";
import { StoreContext } from "../stores/StoreContext";

const useStyles = makeStyles((theme) => ({
  root: {
    marginTop: theme.spacing(6),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  paper:{
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    marginTop: theme.spacing(2),
    padding: theme.spacing(3),
  },
  item:{
    margin: theme.spacing(2),
  }
}));

export default observer(function Example() {
  const classes = useStyles();

  const [counter, setcounter] = useState<number>(0);

  const handleClick = () => setcounter(counter + 1);

  return (
    <Container className={classes.root} >
      <Typography variant="h3" component="h2">
        Welcome in React SPA APP!
      </Typography>
      <Paper className={classes.paper}>
      <Typography className={classes.item} variant="h5" component="h3" color="primary">
        Click and see some magic!
      </Typography>
      <Button  variant="contained" onClick={handleClick}>+1</Button>

      <Typography className={classes.item} variant="h6" component="h4" color="primary">
        Counter: {counter}
      </Typography>
      </Paper>

    </Container>
  );
});
