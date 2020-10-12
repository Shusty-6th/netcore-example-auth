import React, { ReactElement } from 'react'

import classes from '*.module.css';
import { Avatar, Button, Container, makeStyles, TextField, Typography } from '@material-ui/core';


const useStyles = makeStyles((theme) => ({
    paper: {
      marginTop: theme.spacing(8),
      display: "flex",
      flexDirection: "column",
      alignItems: "center",
    },
    avatar: {
      margin: theme.spacing(1),
      backgroundColor: theme.palette.secondary.main,
    },
    form: {
      marginTop: theme.spacing(1),
    },
    submit: {
      margin: theme.spacing(3, 0, 2),
    },
  }));

interface Props {
    
}

function ProductList({}: Props): ReactElement {



    return (
        <Container component="main" maxWidth="xs">

        <div className={classes.paper}>
          <Typography component="h1" variant="h5">
            Welcome to our Page.
          </Typography>
          
        </div>
      </Container>
    )
}

export default ProductList
