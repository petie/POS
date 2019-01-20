import React from "react";
import {
  Grid,
  withStyles,
  Theme,
  createStyles,
} from "@material-ui/core";
import {
  Search,
  PlusOne,
  CheckCircle,
  Cancel,
  ExitToApp,
  RemoveShoppingCart
} from "@material-ui/icons";
import TopMenuButton from "./TopMenuButton";

const styles = (theme: Theme) =>
  createStyles({
    mainGrid: {
      paddingLeft: theme.spacing.unit,
      paddingRight: theme.spacing.unit
    },
    icon: {
      marginRight: theme.spacing.unit,
      fontSize: 54
    }
  });

class TopMenu extends React.Component<any, any> {
  render() {
    const { classes } = this.props;
    return (
      <Grid container spacing={8} className={classes.mainGrid}>
        <TopMenuButton text="Znajdź produkt"><Search className={classes.icon} /></TopMenuButton>
        <TopMenuButton text="Zmień ilość"><PlusOne className={classes.icon} /></TopMenuButton>
        <TopMenuButton text="Zapłata"><CheckCircle className={classes.icon} /></TopMenuButton>
        <TopMenuButton text="Usuń pozycję"><RemoveShoppingCart className={classes.icon} /></TopMenuButton>
        <TopMenuButton text="Anuluj paragon"><Cancel className={classes.icon} /></TopMenuButton>
        <TopMenuButton text="Zamknij zmianę"><ExitToApp className={classes.icon} /></TopMenuButton>
      </Grid>
    );
  }
}

export default withStyles(styles)(TopMenu);
