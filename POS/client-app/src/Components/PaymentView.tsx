import { Dialog, Slide, DialogTitle, DialogContent, Grid, Typography, TextField, Button, Theme, createStyles, withStyles, StyledComponentProps } from "@material-ui/core";
import React from "react";
import { IRootState } from "../Reducers/Index";
import { connect } from "react-redux";
import { compose } from "recompose";
import { paySubmit, payCancelDialog } from "../Reducers/PaymentReducer";
import { FormattedNumber} from 'react-intl';

function Transition(props: any) {
    return <Slide direction="up" {...props} />;
}

const styles = (theme: Theme) => createStyles({
    paper: {
        minWidth: 900
    }
});

class PaymentView extends React.Component<PaymentViewProps, any> {
    constructor(props){
        super(props);
        this.state = { amount: 0 }
    }
    componentWillReceiveProps(nextProps) {
        if (nextProps.receiptTotal !== this.props.receiptTotal || nextProps.amount !== this.props.amount) {
            this.setState({ amountString: nextProps.receiptTotal, amount: nextProps.amount });
        }
    }
    render() {
        const {classes} = this.props;
        const c = classes || {};
        const amount = this.state && this.state.amount ? Number(this.state.amount) : 0;
        return <Dialog open={this.props.showPayDialog} TransitionComponent={Transition} maxWidth="xl">
            <DialogTitle id="form-dialog-title">Płatność</DialogTitle>
            <DialogContent className={c.paper}>
                <Grid container>
                    <Grid item md={3}>
                        <Typography variant="h4">Do zapłaty: </Typography>
                    </Grid>
                    <Grid item md={9}>
                        <Typography variant="h3">{this.props.receiptTotal}</Typography>
                    </Grid>
                </Grid>
                <Grid container>
                    <Grid item md={3}>
                        <Typography variant="h4">Wpłata:</Typography>
                    </Grid>
                    <Grid item md={9}>
                    <TextField
                        autoFocus
                        margin="dense"
                        id="amount"
                        label="Kwota wpłaty"
                        type="number"
                        value={this.state.amount}
                        onFocus={(event) => this.handleFocus(event)}
                        onKeyPress={(event) => this.handleKeys(event)}
                        onChange={(event) => this.handleChange(event)}
                    />
                </Grid>
                </Grid>

                <Grid container>
                    <Grid item md={3}>
                        <Typography variant="h4">Reszta: </Typography>
                    </Grid>
                    <Grid item md={9}>
                        <Typography variant="h4"><FormattedNumber  value={amount < this.props.amount ? 0 : (amount - this.props.amount)} style="currency" currency="PLN"/></Typography>
                    </Grid>
                </Grid>
                <Grid container>
                    <Grid item md={6}>
                        <Button color="primary" size="large" variant="contained" disabled={amount < this.props.amount} onClick={(event) => this.handleSubmit(event)} >
                            Zatwierdź
            </Button>
                    </Grid>
                    <Grid item md={6}>
                        <Button color="secondary" size="large" variant="contained" onClick={(event) => this.handleCancel(event)}>
                            Anuluj
            </Button>
                    </Grid>
                </Grid>
            </DialogContent>
        </Dialog>;
    }
    handleCancel(event?: React.MouseEvent<HTMLElement, MouseEvent>): void {
        this.props.payCancelDialog();
    }
    handleSubmit(event?: React.MouseEvent<HTMLElement, MouseEvent>): void {
        this.props.paySubmit(this.props.id, this.state.amount);
    }
    handleChange(event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>): void {
        this.setState({amount: event.target.value})
    }
    handleKeys(event: React.KeyboardEvent<HTMLDivElement>): void {
        if (event.key === "Enter") {
            event.preventDefault();
            this.handleSubmit()
        }
        else if (event.key === "Escape") {
            event.preventDefault();
            this.handleCancel()
        }
    }
    handleFocus(event: any): void {
        event.target.select();
    }
}

const mapStateToProps = (store: IRootState) => ({
    ...store.payment,
    receiptTotal: store.receipt.receiptTotal
});

const mapDispatchToProps = { paySubmit, payCancelDialog  };

type StateProps = ReturnType<typeof mapStateToProps>;
type DispatchProps = typeof mapDispatchToProps;
type PaymentViewProps = StyledComponentProps & StateProps & DispatchProps;
export default compose<PaymentViewProps, {}>(
    withStyles(styles),
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(PaymentView);