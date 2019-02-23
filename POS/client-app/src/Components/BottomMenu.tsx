import { Theme, createStyles, withStyles, Grid, TextField, StyledComponentProps } from "@material-ui/core";

import React from "react";
import { IRootState } from "../Reducers/Index";
import { compose } from "recompose";
import { connect } from "react-redux";
import { addItemToReceipt } from "../Reducers/ReceiptReducer";

const styles = (theme: Theme) =>
    createStyles({
        textField: {
            marginLeft: theme.spacing.unit * 4,
            marginTop: theme.spacing.unit * 4,
            marginBottom: theme.spacing.unit * 4
        },
        inputField: {
            fontSize: theme.typography.h4.fontSize
        },
        label: {
            fontSize: theme.typography.h4.fontSize
        }
    });

type BottomMenuState = {
    name: string;
};

class BottomMenu extends React.Component<BottomMenuProps, BottomMenuState> {
    constructor(props: any) {
        super(props);
        this.state = {
            name: ""
        };
    }
    handlePickProduct() {
        this.props.addItemToReceipt(this.state.name);
        this.setState({name:""})
    }
    handleChange(name: string) {
        return (event: any) => {
            if (event.key === "Enter") this.handlePickProduct();
            else
                return this.setState({
                    [name]: event.target.value
                } as BottomMenuState);
        };
    }
    render() {
        const { classes } = this.props;
        const c = classes || {}
        return (
            <Grid container>
                <Grid item md={8}>
                    <TextField
                        autoFocus={true}
                        fullWidth
                        id="standard-name"
                        label="Dodaj produkt"
                        variant="outlined"
                        className={c.textField}
                        InputProps={{ className: c.inputField }}
                        InputLabelProps={{ className: c.label }}
                        value={this.state.name}
                        disabled={!this.props.shift.isOpen}
                        onKeyPress={(event) => this.handleEnterKey(event)}
                        onChange={(event) => this.handleEanChange(event)}
                        margin="normal"
                    />
                </Grid>
            </Grid>
        );
    }
    handleEnterKey(event: React.KeyboardEvent<HTMLDivElement>): void {
        if (event.key === "Enter") {
            //event.preventDefault();
            this.handlePickProduct();
        }
    }
    handleEanChange(event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>): void {
        this.setState({
            ["name"]: event.target.value
        } as BottomMenuState);
    }
}
const mapStateToProps = (store: IRootState) => ({
    ...store.product,
    shift: store.shift
});
const mapDispatchToProps = { addItemToReceipt };

type StateProps = ReturnType<typeof mapStateToProps>;
type DispatchProps = typeof mapDispatchToProps;
type BottomMenuProps = StyledComponentProps & StateProps & DispatchProps;
export default compose<BottomMenuProps, {}>(
    withStyles(styles),
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(BottomMenu);
