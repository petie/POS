import { IRootState } from "../Reducers/Index";
import React from "react";
import { compose } from "recompose";
import { withStyles, StyledComponentProps, createStyles, Theme, Dialog, DialogTitle, DialogContent, Grid, TextField, Button, Paper, Typography } from "@material-ui/core";
import { connect } from "react-redux";
import MUIDataTable from "mui-datatables";
import { changeQuantitySubmit, changeQuantityCancel, deleteReceiptSubmit, deleteReceiptCancel } from "../Reducers/ReceiptReducer";

const styles = (theme: Theme) =>
    createStyles({
        dialog: {
            minWidth: 500
        },
        grid: {
            marginBottom: theme.spacing.unit * 2
        },
        inputField: {
            fontSize: theme.typography.h4.fontSize
        }
    });

type ChangeQuantityState = {
    quantity: number;
};

class ChangeQuantity extends React.Component<ChangeQuantityProps, ChangeQuantityState> {
    constructor(props){
        super(props);
        this.state = {quantity:1};
    }
    handleTextChange(event: any): void {
        const q = Number(event.target.value);
        if (!isNaN(q)) this.setState({ quantity: q });
    }
    handleCancel(event?: React.MouseEvent<HTMLElement, MouseEvent>): void {
        this.props.deleteReceiptCancel();
    }
    handleSubmit(event?: React.MouseEvent<HTMLElement, MouseEvent>): void {
        this.props.deleteReceiptSubmit(this.props.id);
    }
    handleFocus(event: any): void {
        event.target.select();
    }
    handleEnterKey(event: React.KeyboardEvent<HTMLDivElement>): void {
        if (event.key === "Enter") {
            event.preventDefault();
            this.handleSubmit()
        }
        else if (event.key === "Escape") {
            event.preventDefault();
            this.handleCancel()
        }
    }
    render() {
        const { classes } = this.props;
        const c = classes || {};
        return (
            <Dialog open={this.props.showReceiptCancelDialog} maxWidth={false}>
                <DialogContent>
                    <Grid container className={c.grid}>
                    <Typography variant="h4">Czy na pewno usunąć paragon?</Typography>
                    </Grid>
                    <Grid container spacing={32}>
                        <Grid item md={6}>
                            <Button color="primary" size="large" variant="contained" fullWidth onClick={event => this.handleSubmit(event)} disabled={this.state.quantity <= 0}>
                                Tak
                            </Button>
                        </Grid>
                        <Grid item md={6}>
                            <Button color="primary" size="large" variant="contained" fullWidth onClick={event => this.handleCancel(event)}>
                                Nie
                            </Button>
                        </Grid>
                    </Grid>
                </DialogContent>
            </Dialog>
        );
    }

}
const mapStateToProps = (store: IRootState) => ({
    ...store.receipt
});

const mapDispatchToProps = { deleteReceiptSubmit, deleteReceiptCancel };

type StateProps = ReturnType<typeof mapStateToProps>;
type DispatchProps = typeof mapDispatchToProps;
type ChangeQuantityProps = StyledComponentProps & StateProps & DispatchProps;
export default compose<ChangeQuantityProps, {}>(
    withStyles(styles),
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(ChangeQuantity);
