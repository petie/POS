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
import ProductList from "./ProductList";
import ChangeQuantity from "./ChangeQuantity";
import DeleteReceipt from "./DeleteReceipt";
import PaymentView from "./PaymentView";
import { payShowDialog } from "../Reducers/PaymentReducer";

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

type TopMenuState = typeof initialState;

class TopMenu extends React.Component<TopMenuProps, TopMenuState> {
    constructor(props){
        super(props);
        this.props.getCurrentShift();
        this.props.getCurrentReceipt();
    }
    handleProductList = event => {
        this.props.productDialogShow();
    };
    handleChangeQuantity = (event: any) => {
        this.props.changeQuantityShow();
    };
    handlePay = (event: any) => {
        this.props.payShowDialog(this.props.receipt.id);
    };
    handleRemoveReceiptItem = (event: any) => {
        this.props.removeItemFromReceipt(this.props.receipt.id, this.props.receipt.selectedReceiptItem || 1);
    };
    handleCancelRecceipt = (event: any) => {
        this.props.deleteReceiptShow();
    };
    handleCloseShift = (event: any) => {
        this.props.endShiftShow();
    };
    handleOpenShift = (event: any) => {
        if (this.props.shift.isCreated)
            this.props.startShiftExistingShow();
        else
            this.props.startShiftShow();
    };
    render() {
        let { classes, shift } = this.props;
        classes = classes || {};
        return (
            <Grid container spacing={8} className={classes.mainGrid}>
                <TopMenuButton text="Znajdź produkt" disabled={!shift.isOpen} onClick={this.handleProductList}>
                    <Search className={classes.icon} />
                </TopMenuButton>
                <TopMenuButton text="Zmień ilość" disabled={!this.props.canChangeQuantity} onClick={this.handleChangeQuantity}>
                    <PlusOne className={classes.icon} />
                </TopMenuButton>
                <TopMenuButton text="Zapłata" disabled={!this.props.canPay} onClick={this.handlePay}>
                    <CheckCircle className={classes.icon} />
                </TopMenuButton>
                <TopMenuButton text="Usuń pozycję" disabled={!this.props.canRemoveItem} onClick={this.handleRemoveReceiptItem}>
                    <RemoveShoppingCart className={classes.icon} />
                </TopMenuButton>
                <TopMenuButton text="Anuluj paragon" disabled={!this.props.canCancelReceipt} onClick={this.handleCancelRecceipt}>
                    <Cancel className={classes.icon} />
                </TopMenuButton>
                <TopMenuButton
                    text={shift.isOpen ? "Zamknij zmianę" : "Otwórz zmianę"}
                    disabled={shift.isOpen ? !shift.canCloseShift : !shift.canOpenShift}
                    onClick={shift.isOpen ? this.handleCloseShift : this.handleOpenShift}
                >
                    <ExitToApp className={classes.icon} />
                </TopMenuButton>
                <ProductList />
                <ChangeQuantity />
                <DeleteReceipt />
                <PaymentView />
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
type TopMenuProps = StyledComponentProps & StateProps & DispatchProps;
export default compose<TopMenuProps, {}>(
    withStyles(styles),
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(TopMenu);
