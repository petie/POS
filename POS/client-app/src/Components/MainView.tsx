import React from "react";
import ReceiptView from "./ReceiptView";
import TopMenu from "./TopMenu";
import TillManagement from "./TillManagement";
import { HotKeys } from "react-hotkeys";
import { Paper, Theme, createStyles, withStyles, AppBar, Toolbar, Typography, Divider, StyledComponentProps } from "@material-ui/core";
import BottomMenu from "./BottomMenu";
import ProductList from "./ProductList";
import ChangeQuantity from "./ChangeQuantity";
import DeleteReceipt from "./DeleteReceipt";
import PaymentView from "./PaymentView";
import { IRootState } from "../Reducers/Index";
import { compose } from "recompose";
import { connect } from "react-redux";
import { payShowDialog } from "../Reducers/PaymentReducer";
import { productDialogShow } from "../Reducers/ProductReducer";
import { deleteReceiptShow, changeQuantityShow, removeItemFromReceipt, getCurrentReceipt, selectNextReceiptItem, selectPrevReceiptItem } from "../Reducers/ReceiptReducer";
import { endShiftShow, startShiftShow, getCurrentShift, startShiftExistingShow } from "../Reducers/ShiftReducer";

const styles = (theme: Theme) =>
    createStyles({
        layout: {
            width: "auto",
            marginLeft: theme.spacing.unit * 3,
            marginRight: theme.spacing.unit * 3,
            marginTop: theme.spacing.unit * 3,
            [theme.breakpoints.up(1100 + theme.spacing.unit * 3 * 2)]: {
                marginLeft: theme.spacing.unit * 2,
                marginRight: theme.spacing.unit * 2
            }
        },
        divider: {
            marginTop: theme.spacing.unit * 2
        },
        paper: {
            marginBottom: theme.spacing.unit * 2
        }
    });
const keyMap = {
    PRODUCTS: "ins",
    QUANTITY: "*",
    REMOVE_ITEM: "-",
    REMOVE_RECEIPT: "alt+del",
    PAY: "/",
    PREVIOUS: "up",
    NEXT: "down",
    FOCUS_ADD: "plus"
};

class MainView extends React.Component<MainViewProps, any> {
    handlers: any;
    constructor(props) {
        super(props);
        this.handlers = {
            PRODUCTS: this.handle.handleProductList,
            QUANTITY: this.handle.handleChangeQuantity,
            REMOVE_ITEM: this.handle.handleRemoveReceiptItem,
            REMOVE_RECEIPT: this.handle.handleCancelReceipt,
            PAY: this.handle.handlePay,
            PREVIOUS: this.handle.handlePrev,
            NEXT: this.handle.handleNext,
            FOCUS_ADD: this.handle.handleFocus
        };
    }
    handle = {
        handleFocus: event => {
            event.preventDefault();
            const input = document.getElementById("standard-name");
            if (input) input.focus();
        },
        handleProductList: event => {
            if (this.props.shift.isOpen) this.props.productDialogShow();
        },
        handleChangeQuantity: (event: any) => {
            if (this.props.receipt.canChangeQuantity) {
                event.preventDefault();
                this.props.changeQuantityShow();
            }
        },
        handlePay: (event: any) => {
            if (this.props.receipt.canPay) {
                event.preventDefault();
                this.props.payShowDialog(this.props.receipt.id);
            }
        },
        handleRemoveReceiptItem: (event: any) => {
            if (this.props.receipt.canRemoveItem) {
                event.preventDefault();
                this.props.removeItemFromReceipt(this.props.receipt.id, this.props.receipt.selectedReceiptItem || 1);
            }
        },
        handleCancelReceipt: (event: any) => {
            if (this.props.receipt.canCancelReceipt) this.props.deleteReceiptShow();
        },
        handleCloseShift: (event: any) => {
            if (this.props.shift.canCloseShift) this.props.endShiftShow();
        },
        handleOpenShift: (event: any) => {
            if (this.props.shift.canOpenShift)
                if (this.props.shift.isCreated) this.props.startShiftExistingShow();
                else this.props.startShiftShow();
        },
        handleNext: (event: any) => {
            this.props.selectNextReceiptItem();
        },
        handlePrev: (event: any) => {
            this.props.selectPrevReceiptItem();
        }
    };
    componentDidMount() {}
    render() {
        const { classes } = this.props;
        const c = classes || {};
        return (
            <div>
                <AppBar position="static" color="primary">
                    <Toolbar variant="dense">
                        <Typography variant="h6" color="inherit">
                            Biovert POS
                        </Typography>
                    </Toolbar>
                </AppBar>
                <div className={c.layout}>
                    <HotKeys keyMap={keyMap} handlers={this.handlers}>
                        <Paper className={c.paper}>
                            <TopMenu {...this.handle} />
                        </Paper>
                        <Paper className={c.paper}>
                            <ReceiptView />
                        </Paper>
                        <Paper className={c.paper}>
                            <BottomMenu />
                        </Paper>
                    </HotKeys>
                    <TillManagement />
                    <ProductList />
                    <ChangeQuantity />
                    <DeleteReceipt />
                    <PaymentView />
                </div>
            </div>
        );
    }
}
const mapStateToProps = (store: IRootState) => ({
    ...store
});

const mapDispatchToProps = {
    payShowDialog,
    productDialogShow,
    deleteReceiptShow,
    changeQuantityShow,
    endShiftShow,
    startShiftShow,
    getCurrentShift,
    startShiftExistingShow,
    removeItemFromReceipt,
    getCurrentReceipt,
    selectNextReceiptItem,
    selectPrevReceiptItem
};

type StateProps = ReturnType<typeof mapStateToProps>;
type DispatchProps = typeof mapDispatchToProps;
type MainViewProps = StyledComponentProps & StateProps & DispatchProps;
export default compose<MainViewProps, {}>(
    withStyles(styles),
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(MainView);
