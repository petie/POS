import { IRootState } from "../Reducers/Index";
import React from "react";
import { compose } from "recompose";
import { withStyles, StyledComponentProps, createStyles, Theme, Dialog, DialogTitle, DialogContent, Grid, TextField, Button, Paper } from "@material-ui/core";
import { connect } from "react-redux";
import Product from "../Models/Product";
import { productFetch, productDialogClose } from "../Reducers/ProductReducer";
import MUIDataTable from "mui-datatables";
import { addItemToReceipt } from "../Reducers/ReceiptReducer";

const styles = (theme: Theme) =>
    createStyles({
        dialog: {
            minWidth: 1000
        },
        grid: {
            marginBottom: theme.spacing.unit * 2
        }
    });

const initialState = {
    eanValue: ""
};

type ProductListState = typeof initialState;
const columns = [
    {
        name: "ean",
        label: "EAN",
        options: {
            filter: true,
            sort: true
        }
    },
    {
        name: "name",
        label: "Nazwa",
        options: {
            filter: true,
            sort: true
        }
    },
    {
        name: "price",
        label: "Cena",
        options: {
            filter: false,
            sort: false
        }
    },
    {
        name: "tax",
        label: "VAT",
        options: {
            filter: false,
            sort: false
        }
    },
    {
        name: "unit",
        label: "Jm",
        options: {
            filter: false,
            sort: false
        }
    }
];

class ProductList extends React.Component<ProductListProps, ProductListState> {
    constructor(props) {
        super(props);
        this.props.productFetch();
        this.state = initialState;
    }
    handleTextChange(event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>): void {
        this.setState({ eanValue: event.target.value });
    }
    handleCancel(event: React.MouseEvent<HTMLElement, MouseEvent>): void {
        this.props.productDialogClose();
    }
    handleSubmit(event: React.MouseEvent<HTMLElement, MouseEvent>): void {
        if (this.state.eanValue && this.state.eanValue != "") {
            this.props.addItemToReceipt(this.state.eanValue);
            this.props.productDialogClose();
            this.setState({eanValue: ""});
        }
    }
    handleRowClick(rowData: string[], rowMeta: { dataIndex: number, rowIndex: number }) {
        this.setState({ eanValue: rowData[0] });
    }
    options() {
        return {
            filterType: "checkbox",
            responsive: "scroll",
            download: false,
            print: false,
            filter: false,
            sort: false,
            selectableRows: false,
            onRowClick: this.handleRowClick.bind(this),
            elevation:0
        };
    }
    render() {
        const { classes } = this.props;
        const c = classes || {};
        return (
            <Dialog open={this.props.showProductDialog} maxWidth="xl">
                <DialogTitle id="form-dialog-title">Wybierz produkt</DialogTitle>
                <DialogContent className={c.dialog}>
                    <Grid container className={c.grid} spacing={8}>
                        <Grid item md={8}>
                            <TextField autoFocus margin="dense" id="amount" onChange={event => this.handleTextChange(event)} value={this.state.eanValue} label="EAN" type="text" fullWidth />
                        </Grid>
                        <Grid item md={2}>
                            <Button color="primary" size="large" variant="contained" fullWidth onClick={event => this.handleSubmit(event)} disabled={this.state && this.state.eanValue === ""}>
                                Zatwierdź
                            </Button>
                        </Grid>
                        <Grid item md={2}>
                            <Button color="secondary" size="large" variant="contained" fullWidth onClick={event => this.handleCancel(event)}>
                                Anuluj
                            </Button>
                        </Grid>
                    </Grid>
                    <MUIDataTable
                        title={""}
                        data={this.props.products.map(item => {
                            return [item.ean, item.name, item.price, item.tax, item.unit];
                        })}
                        columns={columns}
                        options={this.options()}
                    />
                </DialogContent>
            </Dialog>
        );
    }
}
const mapStateToProps = (store: IRootState) => ({
    ...store.product
});

const mapDispatchToProps = { productFetch, productDialogClose, addItemToReceipt };

type StateProps = ReturnType<typeof mapStateToProps>;
type DispatchProps = typeof mapDispatchToProps;
type ProductListProps = StyledComponentProps & StateProps & DispatchProps;
export default compose<ProductListProps, {}>(
    withStyles(styles),
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(ProductList);
