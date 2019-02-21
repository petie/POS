import { IRootState } from "../Reducers/Index";
import React from "react";
import { compose } from "recompose";
import { withStyles, StyledComponentProps, createStyles, Theme, Dialog, DialogTitle, DialogContent, Grid, TextField, Button, Paper } from "@material-ui/core";
import { connect } from "react-redux";
import MUIDataTable from "mui-datatables";
import { changeQuantitySubmit, changeQuantityCancel } from "../Reducers/ReceiptReducer";

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
        this.props.changeQuantityCancel();
    }
    handleSubmit(event?: React.MouseEvent<HTMLElement, MouseEvent>): void {
        this.props.changeQuantitySubmit(this.props.selectedReceiptItem || 1, this.state.quantity);
    }
    handleFocus(event: any): void {
        event.target.select();
    }
    componentWillReceiveProps(nextProps) {
        if (nextProps.selectedReceiptItemQuantity !== this.props.selectedReceiptItemQuantity) {
            this.setState({ quantity: nextProps.selectedReceiptItemQuantity });
        }
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
            <Dialog open={this.props.showQuantityDialog} maxWidth={false}>
                <DialogTitle id="form-dialog-title">Podaj ilość</DialogTitle>
                <DialogContent>
                    <Grid container className={c.grid} spacing={8}>
                        <Grid item md={6}>
                            <TextField InputProps={{ className: c.inputField }} autoFocus margin="dense" id="amount" onKeyDown={event => this.handleEnterKey(event)} onChange={event => this.handleTextChange(event)} onFocus={event => this.handleFocus(event)} value={this.state.quantity} type="number" fullWidth />
                        </Grid>
                        <Grid item md={3}>
                            <Button color="primary" size="large" variant="contained" fullWidth onClick={event => this.handleSubmit(event)} disabled={this.state.quantity <= 0}>
                                Zatwierdź
                            </Button>
                        </Grid>
                        <Grid item md={3}>
                            <Button color="secondary" size="large" variant="contained" fullWidth onClick={event => this.handleCancel(event)}>
                                Anuluj
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

const mapDispatchToProps = { changeQuantityCancel, changeQuantitySubmit };

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
