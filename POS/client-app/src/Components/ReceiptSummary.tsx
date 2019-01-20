import React from "react";
import { List, ListItem, ListItemText, createStyles, withStyles, Theme, Grid, Typography } from "@material-ui/core";

const styles = (theme: Theme) => createStyles({});

class ReceiptSummary extends React.Component<any> {
    render() {
        return <List>
            <ListItem>
                <Grid container>
                <Grid item md={6}>
                <Typography variant="h6">Suma</Typography>
                </Grid>
                <Grid item md={6}>
                <Typography variant="h2">{this.props.receiptTotal}</Typography>
                </Grid>
                </Grid>
            </ListItem>
        </List>
    }
}

export default withStyles(styles)(ReceiptSummary)