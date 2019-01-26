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
import { startShift, initializeShift, endShiftShow, endShiftSubmit } from "../Reducers/ShiftReducer";
import { connect } from "react-redux";
import compose from "recompose/compose";

function Transition(props: any) {
  return <Slide direction="up" {...props} />;
}

const styles = (theme: Theme) => createStyles({});

type TillProps = StyledComponentProps & StateProps & DispatchProps;
type TillState = {
  amount: string;
};
class TillManagement extends React.Component<TillProps, TillState> {
  constructor(props: TillProps) {
    super(props);
    this.state = { amount: "" };
  }
  render() {
    return (
      <div>
        {this.props.showStartShift ? (
          <Dialog open={this.props.showDialog} TransitionComponent={Transition}>
            <DialogTitle id="form-dialog-title">Otwarcie zmiany</DialogTitle>
            <DialogContent>
              <Grid container>
                <Grid item md={12}>
                  <Typography>
                    Aktualna kwota na stanowisku: {this.props.startAmount}
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
                  label="amount"
                  type="number"
                  value={this.state.amount}
                  fullWidth
                />
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Button color="primary">Zatwierdź</Button>
                </Grid>
                <Grid item md={6}>
                  <Button color="primary">Anuluj</Button>
                </Grid>
              </Grid>
            </DialogContent>
          </Dialog>
        ) : (
          <Dialog open={this.props.showDialog} TransitionComponent={Transition}>
            <DialogTitle id="form-dialog-title">Zamknięcie zmiany</DialogTitle>
            <DialogContent>
              <Grid container>
                <Grid item md={6}>
                  <Typography>Kwota z przeniesienia:</Typography>
                </Grid>
                <Grid item md={6}>
                  <Typography>{this.props.startAmount}</Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography>Wpłata początkowa:</Typography>
                </Grid>
                {/* <Grid item md={6}>
                  <Typography>{this.props.amount}</Typography>
                </Grid> */}
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography>Ilość paragonów:</Typography>
                </Grid>
                <Grid item md={6}>
                  <Typography>{this.props.numberOfReceipts}</Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography>Ilość anulowanych paragonów:</Typography>
                </Grid>
                <Grid item md={6}>
                  <Typography>
                    {this.props.numberOfCancelledReceipts}
                  </Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography>Ilość anulowanych pozycji:</Typography>
                </Grid>
                <Grid item md={6}>
                  <Typography>
                    {this.props.numberOfCancelledReceiptItems}
                  </Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography>Sprzedaż:</Typography>
                </Grid>
                <Grid item md={6}>
                  <Typography>{this.props.sales}</Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={3}>
                  <Typography>Wpłata początkowa</Typography>
                </Grid>
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Typography>Pozostaje:</Typography>
                </Grid>
                {/* <Grid item md={6}>
                  <Typography>{this.props.}</Typography>
                </Grid> */}
              </Grid>
              <Grid container>
                <Grid item md={6}>
                  <Button color="primary">Zamknij zmianę</Button>
                </Grid>
                <Grid item md={6}>
                  <Button color="primary">Anuluj</Button>
                </Grid>
              </Grid>
            </DialogContent>
          </Dialog>
        )}
      </div>
    );
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

const mapDispatchToProps = { startShift, initializeShift, endShiftShow, endShiftSubmit}

type StateProps = ReturnType<typeof  mapStateToProps>;
type DispatchProps = typeof mapDispatchToProps;

export default compose<TillProps, {}>(withStyles(styles),connect(mapStateToProps, mapDispatchToProps))(TillManagement);
