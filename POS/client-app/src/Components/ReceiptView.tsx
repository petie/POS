import React from "react";
import { Grid, withStyles, Theme, createStyles, StyledComponentProps } from "@material-ui/core";
import ReceiptDetails from "./ReceiptDetails";
import ReceiptSummary from "./ReceiptSummary";
import { IRootState } from "../Reducers/Index";
import { compose } from "recompose";
import { connect } from "react-redux";

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

type ReceiptViewState = {
    selectedEan: string;
}
class ReceiptView extends React.Component<ReceiptViewProps, ReceiptViewState> {
    constructor(props: any){
        super(props);
        this.state = {
            selectedEan: ""
        }
    }
    render() {
        const { classes } = this.props;
        const c = classes || {};
        return <Grid container >
            <Grid item className={c.detailsGrid} md={9}>
                <ReceiptDetails rows={this.props.items}/>
            </Grid>
            <Grid item md={3} className={c.summaryGrid}>
                <ReceiptSummary receiptTotal={this.props.receiptTotal}/>
            </Grid>
        </Grid>
    }
}

const mapDispatchToProps = {  };

const mapStateToProps = (store: IRootState) => ({
    ...store.receipt
});

type StateProps = ReturnType<typeof mapStateToProps>;
type DispatchProps = typeof mapDispatchToProps;
type ReceiptViewProps = StyledComponentProps & StateProps & DispatchProps;
export default compose<ReceiptViewProps, {}>(
    withStyles(styles),
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(ReceiptView);