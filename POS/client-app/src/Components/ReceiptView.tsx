import React from "react";
import { Grid, withStyles, Theme, createStyles } from "@material-ui/core";
import ReceiptDetails from "./ReceiptDetails";
import ReceiptSummary from "./ReceiptSummary";

const styles = (theme: Theme) => createStyles({
    summaryGrid: {
        borderLeft: "1px solid lightgray"
    },
    detailsGrid: {
        padding: theme.spacing.unit * 1,
        minHeight: "500px",
        maxHeight: "500px",
        overflowY: "scroll",
        backgroundColor: "white"
    }
});
const rows = [
    {
        id: 1,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    },
    {
        id: 2,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    },
    {
        id: 3,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    },
    {
        id: 4,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    },
    {
        id: 5,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    },
    {
        id: 6,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    }
    ,
    {
        id: 7,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    }
    ,
    {
        id: 8,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    }
    ,
    {
        id: 9,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    }
    ,
    {
        id: 10,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    }
    ,
    {
        id: 11,
        name: "Test",
        price: "10,99 zł",
        value:  "109,90 zł",
        unit:"szt",
        quantity: "10",
        ean: "1234567890123"
    }
]

type ReceiptViewState = {
    selectedEan: string;
}
class ReceiptView extends React.Component<any, ReceiptViewState> {
    constructor(props: any){
        super(props);
        this.state = {
            selectedEan: ""
        }
    }
    render() {
        const { classes } = this.props;
        return <Grid container >
            <Grid item className={classes.detailsGrid} md={9}>
                <ReceiptDetails rows={rows}/>
            </Grid>
            <Grid item md={3} className={classes.summaryGrid}>
                <ReceiptSummary receiptTotal="100,00zł"/>
            </Grid>
        </Grid>
    }
}

export default withStyles(styles)(ReceiptView);