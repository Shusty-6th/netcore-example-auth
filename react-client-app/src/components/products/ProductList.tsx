import React, { ReactElement, useContext, useEffect, useState } from "react";
import {
  Avatar,
  Button,
  CircularProgress,
  Container,
  makeStyles,
  TextField,
  Typography,
} from "@material-ui/core";
import { useHistory } from "react-router";
import ProductCard from "./ProductCard";
import { cleanup } from "@testing-library/react";
import axios from "axios";
import AppConfig from "../../appconfig";
import MuiAlert, { AlertProps } from "@material-ui/lab/Alert";
import { StoreContext } from "../../stores/StoreContext";

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(5),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  item: {
    marginBottom: theme.spacing(2),
  },
}));

export interface ProductModel {
  id: number;
  name: string;
  color: string;
  isGoodQuality: boolean;
}

interface Props {}

function ProductList({}: Props): ReactElement {
  const classes = useStyles();
  const history = useHistory();
  const { authToken, isModerator } = useContext(StoreContext).userStore;

  const [loading, setloading] = useState<boolean>(true);
  const [error, seterror] = useState<string>("");
  const [products, setproducts] = useState<ProductModel[]>(
    [] as ProductModel[]
  );

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = () => {
    setloading(true);
    
    setproducts([] as ProductModel[]);

    const url = AppConfig.apiUrl + "/api/Product";
    setTimeout(() => {
      axios
        .get(url, {
          headers: {
            Authorization: "Bearer " + authToken,
          },
          responseType: "json",
        })
        .then((res) => {
          console.log(res);
          console.log(res.data);

          setproducts(res.data as ProductModel[]);
        })
        .catch((error) => {
          console.log(error);
          if (error.response) {
            console.log(error.response);
            seterror(
              `ERROR request get Products! ${error.response.status} ${error.response.statusText}`
            );
          }
        })
        .finally(() => {
          setloading(false);
        });
    }, 1000);
  };

  const deleteProduct = (id: number) => {
    setloading(true);
    seterror("");

    const url = AppConfig.apiUrl + `/api/Product/${id}`;
    axios
      .delete(url, {
        headers: {
          Authorization: "Bearer " + authToken,
        },
        responseType: "json",
      })
      .then((res) => {
        console.log(res);
        console.log(res.data);
      })
      .catch((error) => {
        console.log(error);
        if (error.response) {
          console.log(error.response);
          seterror(
            `ERROR remove product API request! ${error.response.status} ${error.response.statusText}`
          );
        }
      })
      .finally(() => {
        fetchProducts();
      });
  };

  const productCards = products.map((p) => (
    <ProductCard
      key={p.id}
      product={p}
      handleDeleteProduct={deleteProduct}
      canModify={isModerator}
    />
  ));

  return (
    <Container component="main" maxWidth="md">
      <div className={classes.paper}>
        <Typography
          className={classes.item}
          component="h1"
          variant="h5"
          color="primary"
        >
          Introducing our amazing products:
        </Typography>
        {loading && <CircularProgress />}
        {productCards}
        {error && (
          <MuiAlert elevation={6} variant="filled" severity="error">
            {error}
          </MuiAlert>
        )}
      </div>
    </Container>
  );
}

export default ProductList;
