import React from "react";
import {
  DialogTitle,
  DialogContent,
  DialogContentText,
  Dialog,
  DialogActions,
  Grid,
  Typography,
  TextField,
  Button,
  Slide,
  Theme,
  createStyles,
  withStyles,
  WithStyles,
  StyledComponentProps
} from "@material-ui/core";
import { IRootState } from "../Reducers/Index";
import { startShiftShow, endShiftShow, endShiftSubmit, endShiftShowCancel, startShiftShowCancel, startShiftSubmit } from "../Reducers/ShiftReducer";
import { connect } from "react-redux";
import compose from "recompose/compose";

function Transition(props: any) {
  return <Slide direction="up" {...props} />;
}

const styles = (theme: Theme) => createStyles({
  dialog: {
    minWidth: 1000
  }
});


type TillState = {
  amount: string;
};
class TillManagement extends React.Component<TillProps, TillState> {
  constructor(props: TillProps) {
    super(props);
    this.state = { amount: "" };
  }
  render() {
    const { classes } = this.props;
    const c = classes || {};
    return (
      <div>
        {this.props.showStartShift ? (
          <Dialog open={this.props.showDialog} className={c.dialog} TransitionComponent={Transition} maxWidth="xl">
            <DialogTitle id="form-dialog-title">Otwarcie zmiany</DialogTitle>
            <DialogContent>
              <Grid container>
                <Grid item md={9}>
                  <Typography variant="h4">
                    Aktualna kwota na stanowisku:
                  </Typography>
                </Grid>
                <Grid item md={3}>
                  <Typography variant="h4">
                    {this.props.startMoney}
                  </Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={3}>
                  <Typography>Wpłata początkowa</Typography>
                </Grid>
              </Grid>
              <Grid item md={9}>
                <TextField
                  autoFocus
                  margin="dense"
                  id="amount"
                  label="Kwota początkowa"
                  type="number"
                  value={this.state.amount}
                  onKeyDown={(event) => this.handleEnter(event)}
                  onChange={(event) => this.handleChangeAmount(event)}
                  fullWidth
                />
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Button color="primary" onClick={(event) => this.handleSubmitStartShift(event)} >Zatwierdź</Button>
                </Grid>
                <Grid item md={6}>
                  <Button color="primary" onClick={(event) => this.handleCancelStartShift(event)} >Anuluj</Button>
                </Grid>
              </Grid>
            </DialogContent>
          </Dialog>
        ) : (
          <Dialog open={this.props.showDialog} className={c.dialog} TransitionComponent={Transition}>
            <DialogTitle id="form-dialog-title">Zamknięcie zmiany</DialogTitle>
            <DialogContent>
              <Grid container>
                <Grid item md={6}>
                  <Typography variant="h4">Kwota z przeniesienia:</Typography>
                </Grid>
                <Grid item md={6}>
                  <Typography variant="h4">{this.props.startMoney}</Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography variant="h4">Wpłata początkowa:</Typography>
                </Grid>
                {/* <Grid item md={6}>
                  <Typography>{this.props.amount}</Typography>
                </Grid> */}
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography variant="h4">Ilość paragonów:</Typography>
                </Grid>
                <Grid item md={6}>
                  <Typography variant="h4">{this.props.numberOfReceipts}</Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography variant="h4">Ilość anulowanych paragonów:</Typography>
                </Grid>
                <Grid item md={6}>
                  <Typography variant="h4">
                    {this.props.cancelledReceiptsCount}
                  </Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography variant="h4">Ilość anulowanych pozycji:</Typography>
                </Grid>
                <Grid item md={6}>
                  <Typography variant="h4">
                    {this.props.removedItemsCount}
                  </Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography variant="h4">Sprzedaż:</Typography>
                </Grid>
                <Grid item md={6}>
                  <Typography variant="h4">{this.props.sales}</Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={3}>
                  <Typography variant="h4">Wpłata początkowa</Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography variant="h4">Pozostaje:</Typography>
                </Grid>
                {/* <Grid item md={6}>
                  <Typography>{this.props.}</Typography>
                </Grid> */}
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Button color="primary" onClick={(event) => this.handleSubmitCloseShift(event)}>Zamknij zmianę</Button>
                </Grid>
                <Grid item md={6}>
                  <Button color="primary" onClick={(event) => this.handleCancelCloseShift(event)}>Anuluj</Button>
                </Grid>
              </Grid>
            </DialogContent>
          </Dialog>
        )}
      </div>
    );
  }
  handleEnter(event: React.KeyboardEvent<HTMLDivElement>): void {
    if (event.key === 'Enter') {
      event.preventDefault();
      this.handleSubmitStartShift();
    }
  }
  handleChangeAmount(event: any): void {
    this.setState({amount: event.target.value})
  }
  handleCancelStartShift(event: React.MouseEvent<HTMLElement, MouseEvent>): void {
    this.props.startShiftShowCancel();
  }
  handleSubmitStartShift(event?: React.MouseEvent<HTMLElement, MouseEvent>): void {
    this.props.startShiftSubmit(this.props.id, Number(this.state.amount));
  }
  handleSubmitCloseShift(event: React.MouseEvent<HTMLElement, MouseEvent>): void {
    this.props.endShiftSubmit();
  }
  handleCancelCloseShift(event: React.MouseEvent<HTMLElement, MouseEvent>): void {
    this.props.endShiftShowCancel();
  }
}

const mapStateToProps = (store: IRootState) => ({
    ...store.shift
    // hasOpenShift: store.shift.hasOpenShift,
    // previousAmount: store.shift.previousAmount,
    // startAmount: store.shift.startAmount,
    // isLoading: store.shift.isLoading,
    // numberOfReceipts: store.shift.numberOfReceipts
});

const mapDispatchToProps = { startShift: startShiftShow, endShiftShow, endShiftSubmit, endShiftShowCancel, startShiftSubmit, startShiftShowCancel}

type StateProps = ReturnType<typeof  mapStateToProps>;
type DispatchProps = typeof mapDispatchToProps;
type TillProps = StyledComponentProps & StateProps & DispatchProps;
export default compose<TillProps, {}>(withStyles(styles),connect(mapStateToProps, mapDispatchToProps))(TillManagement);
