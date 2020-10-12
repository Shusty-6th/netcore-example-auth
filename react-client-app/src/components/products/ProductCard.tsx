import classes from "*.module.css";
import {
  makeStyles,
  Card,
  CardActionArea,
  CardContent,
  Typography,
  CardActions,
  Button,
  IconButton,
} from "@material-ui/core";
import React, { ReactElement } from "react";
import DeleteForeverIcon from "@material-ui/icons/DeleteForever";
import { ProductModel } from "./ProductList";

const useStyles = makeStyles({
  root: {
    maxWidth: 445,
    margin: 10,
    minWidth: 190,
  },
});

interface ProductCardProps {
  product: ProductModel;
  handleDeleteProduct: (id: number) => void;
  canModify?: boolean;
}

function ProductCard({
  product,
  handleDeleteProduct,
  canModify = false,
}: ProductCardProps): ReactElement {
  const classes = useStyles();

  const { id, isGoodQuality, name, color } = product;

  return (
    <Card className={classes.root}>
      <CardActionArea>
        <CardContent>
          <Typography gutterBottom variant="h5" component="h2">
            {name}
          </Typography>
          <Typography variant="body2" component="p">
            Quality product: {isGoodQuality ? "✔️" : "❌"}
          </Typography>
          <Typography variant="body2" component="p">
            Color: {color}
          </Typography>
          <Typography variant="body2" color="textSecondary" component="p">
            Id: {id}
          </Typography>
        </CardContent>
      </CardActionArea>
      <CardActions>
        <IconButton
          color="secondary"
          aria-label="delete product"
          component="span"
          onClick={() => handleDeleteProduct(id)}
          disabled={!canModify}
        >
          <DeleteForeverIcon />
        </IconButton>
      </CardActions>
    </Card>
  );
}

export default ProductCard;
