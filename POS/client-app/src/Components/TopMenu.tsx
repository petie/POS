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

const initialState = {
  selectedItemId: null
}

type TopMenuState = typeof initialState;

class TopMenu extends React.Component<any, TopMenuState> {
  handleProductList = event => {
    this.props.showProductDialog();
  }
  handleChangeQuantity = (event: any) => {
    this.props.changeQuantity(this.props.receiptId, this.state.selectedItemId);
  }
  handlePay = (event: any) => {
    this.props.pay(this.props.receiptId);
  }
  handleRemoveReceiptItem = (event: any) => {
    this.props.removeReceiptItem(this.props.receiptId, this.state.selectedItemId)
  }
  handleCancelRecceipt = (event: any) => {
    this.props.cancelReceipt(this.props.receiptId);
  }
  handleCloseShift = (event: any) => {
    this.props.closeShift();
  }
  handleOpenShift = (event: any) => {
    this.props.openShift();
  }
  render() {
    const { classes } = this.props;
    return (
      <Grid container spacing={8} className={classes.mainGrid}>
        <TopMenuButton text="Znajdź produkt" onClick={this.handleProductList}><Search className={classes.icon} /></TopMenuButton>
        <TopMenuButton text="Zmień ilość" onClick={this.handleChangeQuantity}><PlusOne className={classes.icon} /></TopMenuButton>
        <TopMenuButton text="Zapłata" onClick={this.handlePay}><CheckCircle className={classes.icon} /></TopMenuButton>
        <TopMenuButton text="Usuń pozycję" onClick={this.handleRemoveReceiptItem}><RemoveShoppingCart className={classes.icon} /></TopMenuButton>
        <TopMenuButton text="Anuluj paragon" onClick={this.handleCancelRecceipt}><Cancel className={classes.icon} /></TopMenuButton>
        <TopMenuButton text={this.props.hasOpenShift ? "Zamknij zmianę": "Otwórz zmianę"} onClick={this.props.hasOpenShift ? this.handleCloseShift : this.handleOpenShift}><ExitToApp className={classes.icon} /></TopMenuButton>
      </Grid>
    );
  }
}


export default withStyles(styles)(TopMenu);
