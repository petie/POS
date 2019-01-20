import { Dialog, Slide, DialogTitle, DialogContent, Grid, Typography, TextField, Button, Theme, createStyles, withStyles } from "@material-ui/core";
import React from "react";

function Transition(props: any) {
    return <Slide direction="up" {...props} />;
}

const styles = (theme: Theme) => createStyles({});

class PaymentView extends React.Component<any, any> {
    render() {
        return <Dialog open={this.props.open} TransitionComponent={Transition}>
            <DialogTitle id="form-dialog-title">Otwarcie zmiany</DialogTitle>
            <DialogContent>
                <Grid container>
                    <Grid item md={12}>
                        <Typography>Aktualna kwota na stanowisku: {this.props.startAmount}</Typography>
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
                        <Button color="primary">
                            Zatwierdź
            </Button>
                    </Grid>
                    <Grid item md={6}>
                        <Button color="primary">
                            Anuluj
            </Button>
                    </Grid>
                </Grid>
            </DialogContent>
        </Dialog>;
    }
}

export default withStyles(styles)(PaymentView)