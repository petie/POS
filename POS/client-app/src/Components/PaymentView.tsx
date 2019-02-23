import { Dialog, Slide, DialogTitle, DialogContent, Grid, Typography, TextField, Button, Theme, createStyles, withStyles, StyledComponentProps, Divider } from "@material-ui/core";
import React from "react";
import { IRootState } from "../Reducers/Index";
import { connect } from "react-redux";
import { compose } from "recompose";
import { paySubmit, payCancelDialog } from "../Reducers/PaymentReducer";
import { FormattedNumber } from "react-intl";

function Transition(props: any) {
    return <Slide direction="up" {...props} />;
}

const styles = (theme: Theme) =>
    createStyles({
        paper: {
            minWidth: 800
        },
        inputField: {
            fontSize: theme.typography.h4.fontSize
        },
        row: {
            marginBottom: theme.spacing.unit * 3,
            marginTop: theme.spacing.unit * 3
        },
        inputRow: {
            marginTop: theme.spacing.unit
        },
        currencySign:{
            marginLeft: theme.spacing.unit
        }
    });

class PaymentView extends React.Component<PaymentViewProps, any> {
    constructor(props) {
        super(props);
        this.state = { amount: 0 };
    }
    componentWillReceiveProps(nextProps) {
        if (nextProps.receiptTotal !== this.props.receiptTotal || nextProps.total !== this.props.total) {
            this.setState({ amountString: nextProps.receiptTotal, amount: nextProps.total });
        }
    }
    render() {
        const { classes } = this.props;
        const c = classes || {};
        const amount = this.state && this.state.amount ? this.state.amount : 0;
        return (
            <Dialog open={this.props.showPayDialog} TransitionComponent={Transition} maxWidth="xl" disableEscapeKeyDown={false} onEscapeKeyDown={event => this.handleEscape(event)}>
                <DialogContent className={c.paper}>
                    <Grid container className={c.row}>
                        <Grid item md={4}>
                            <Typography variant="h4">Do zapłaty: </Typography>
                        </Grid>
                        <Grid item md={4}>
                            <Typography variant="h3">{this.props.receiptTotal}</Typography>
                        </Grid>
                    </Grid>
                    <Divider />
                    <Grid container className={c.row}>
                        <Grid item md={4} className={c.inputRow}>
                            <Typography variant="h4">Wpłata:</Typography>
                        </Grid>
                        <Grid item md={3}>
                            <TextField
                                autoFocus
                                margin="dense"
                                id="amount"
                                type="number"
                                fullWidth
                                InputProps={{ className: c.inputField }}
                                value={this.state.amount}
                                onFocus={event => this.handleFocus(event)}
                                onKeyPress={event => this.handleKeys(event)}
                                onChange={event => this.handleChange(event)}
                            />
                        </Grid>
                        <Grid item md={1} className={c.inputRow}>
                        <Typography variant="h4" className={c.currencySign}>zł</Typography>
                    </Grid>
                    </Grid>
                    <Divider />
                    <Grid container className={c.row}>
                        <Grid item md={4}>
                            <Typography variant="h4">Reszta: </Typography>
                        </Grid>
                        <Grid item md={4}>
                            <Typography variant="h4">
                                <FormattedNumber value={amount < this.props.total ? 0 : amount - this.props.total} style="currency" currency="PLN" />
                            </Typography>
                        </Grid>
                    </Grid>
                    <Divider />
                    <Grid container className={c.row} spacing={32}>
                        <Grid item md={6}>
                            <Button color="primary" size="large" fullWidth variant="contained" disabled={amount < this.props.amount} onClick={event => this.handleSubmit(event)}>
                                Zatwierdź
                            </Button>
                        </Grid>
                        <Grid item md={6}>
                            <Button color="secondary" size="large" fullWidth variant="contained" onClick={event => this.handleCancel(event)}>
                                Anuluj
                            </Button>
                        </Grid>

                    </Grid>
                </DialogContent>
            </Dialog>
        );
    }
    handleEscape(event: React.SyntheticEvent<{}, Event>): void {
        this.props.payCancelDialog();
    }
    handleCancel(event?: React.MouseEvent<HTMLElement, MouseEvent>): void {
        this.props.payCancelDialog();
    }
    handleSubmit(event?: React.MouseEvent<HTMLElement, MouseEvent>): void {
        this.props.paySubmit(this.props.id, this.state.amount);
    }
    handleChange(event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>): void {
        this.setState({ amount: event.target.value });
    }
    handleKeys(event: React.KeyboardEvent<HTMLDivElement>): void {
        if (event.key === "Enter") {
            event.preventDefault();
            this.handleSubmit();
        } else if (event.key === "Escape") {
            event.preventDefault();
            this.handleCancel();
        }
    }
    handleFocus(event: any): void {
        event.target.select();
    }
}

const mapStateToProps = (store: IRootState) => ({
    ...store.payment,
    receiptTotal: store.receipt.receiptTotal,
    total: store.receipt.total
});

const mapDispatchToProps = { paySubmit, payCancelDialog };

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
