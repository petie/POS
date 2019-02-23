import React from "react";
import { Grid, withStyles, Theme, createStyles, StyledComponentProps } from "@material-ui/core";
import { Search, PlusOne, CheckCircle, Cancel, ExitToApp, RemoveShoppingCart } from "@material-ui/icons";
import TopMenuButton from "./TopMenuButton";
import { compose } from "recompose";
import { startShiftShow, endShiftShow, getCurrentShift, startShiftExistingShow } from "../Reducers/ShiftReducer";
import { changeQuantityShow, deleteReceiptShow, removeItemFromReceipt, getCurrentReceipt } from "../Reducers/ReceiptReducer";
import { connect } from "react-redux";
import { IRootState } from "../Reducers/Index";
import { productDialogShow } from "../Reducers/ProductReducer";
import { payShowDialog } from "../Reducers/PaymentReducer";
import { HotKeys } from "react-hotkeys";

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
};
type TopMenuHandlers = {
    handleProductList: any;
    handleChangeQuantity: any;
    handlePay: any;
    handleRemoveReceiptItem: any;
    handleCancelReceipt: any;
    handleCloseShift: any;
    handleOpenShift: any;
}
type TopMenuState = typeof initialState;

class TopMenu extends React.Component<TopMenuProps, TopMenuState> {
    constructor(props){
        super(props);
        this.props.getCurrentShift();
        this.props.getCurrentReceipt();

    }
    // handleProductList = event => {
    //     if (this.props.shift.isOpen)
    //         this.props.productDialogShow();
    // };
    // handleChangeQuantity = (event: any) => {
    //     if (this.props.canChangeQuantity) {
    //         event.preventDefault();
    //         this.props.changeQuantityShow();
    //     }
    // };
    // handlePay = (event: any) => {
    //     if (this.props.canPay)
    //         this.props.payShowDialog(this.props.receipt.id);
    // };
    // handleRemoveReceiptItem = (event: any) => {
    //     if (this.props.canRemoveItem) {
    //         event.preventDefault();
    //         this.props.removeItemFromReceipt(this.props.receipt.id, this.props.receipt.selectedReceiptItem || 1);
    //     }
    // };
    // handleCancelReceipt = (event: any) => {
    //     if (this.props.canCancelReceipt)
    //         this.props.deleteReceiptShow();
    // };
    // handleCloseShift = (event: any) => {
    //     if (this.props.shift.canCloseShift)
    //         this.props.endShiftShow();
    // };
    // handleOpenShift = (event: any) => {
    //     if (this.props.shift.canOpenShift)
    //         if (this.props.shift.isCreated)
    //             this.props.startShiftExistingShow();
    //         else
    //             this.props.startShiftShow();
    // };
    render() {
        let { classes, shift } = this.props;
        classes = classes || {};
        return (
            <Grid container spacing={8} className={classes.mainGrid}>
                <TopMenuButton text="Znajdź produkt [Ins]" disabled={!shift.isOpen} onClick={this.props.handleProductList}>
                    <Search className={classes.icon} />
                </TopMenuButton>
                <TopMenuButton text="Zmień ilość [ * ]" disabled={!this.props.canChangeQuantity} onClick={this.props.handleChangeQuantity}>
                    <PlusOne className={classes.icon} />
                </TopMenuButton>
                <TopMenuButton text="Zapłata [ / ]" disabled={!this.props.canPay} onClick={this.props.handlePay}>
                    <CheckCircle className={classes.icon} />
                </TopMenuButton>
                <TopMenuButton text="Usuń pozycję [ - ]" disabled={!this.props.canRemoveItem} onClick={this.props.handleRemoveReceiptItem}>
                    <RemoveShoppingCart className={classes.icon} />
                </TopMenuButton>
                <TopMenuButton text="Anuluj paragon [Alt+Del]" disabled={!this.props.canCancelReceipt} onClick={this.props.handleCancelReceipt}>
                    <Cancel className={classes.icon} />
                </TopMenuButton>
                <TopMenuButton
                    text={shift.isOpen ? "Zamknij zmianę" : "Otwórz zmianę"}
                    disabled={shift.isOpen ? !shift.canCloseShift : !shift.canOpenShift}
                    onClick={shift.isOpen ? this.props.handleCloseShift : this.props.handleOpenShift}
                >
                    <ExitToApp className={classes.icon} />
                </TopMenuButton>
            </Grid>
        );
    }
}

const mapStateToProps = (store: IRootState) => ({
    shift: store.shift,
    receipt: store.receipt,
    canSearchProduct: store.product.canSearchProduct,
    canChangeQuantity: store.receipt.canChangeQuantity,
    canPay: store.receipt.canPay,
    canRemoveItem: store.receipt.canRemoveItem,
    canCancelReceipt: store.receipt.canCancelReceipt
});

const mapDispatchToProps = { payShowDialog, productDialogShow, deleteReceiptShow, changeQuantityShow, endShiftShow, startShiftShow, getCurrentShift, startShiftExistingShow, removeItemFromReceipt, getCurrentReceipt };

type StateProps = ReturnType<typeof mapStateToProps>;
type DispatchProps = typeof mapDispatchToProps;
type TopMenuProps = StyledComponentProps & StateProps & DispatchProps & TopMenuHandlers;
export default compose<TopMenuProps, TopMenuHandlers>(
    withStyles(styles),
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(TopMenu);