import React, { createRef } from "react";
import { Table, TableHead, TableRow, TableCell, TableBody, Theme, createStyles, withStyles, StyledComponentProps } from "@material-ui/core";
import { compose } from "recompose";
import { connect } from "react-redux";
import { IRootState } from "../Reducers/Index";
import { selectReceiptItem } from "../Reducers/ReceiptReducer";
import ReceiptItem from "../Models/ReceiptItem";

const styles = (theme: Theme) => createStyles({
    table: {
        paddingLeft: theme.spacing.unit,
        paddingRight: theme.spacing.unit        
    },
    cell: {
        fontSize: "1rem"
    },
    headerCell: { 
        fontSize: "1rem",
        position: "sticky",
        top: 0,
        zIndex: 10,
        backgroundColor: "white"
    }
});
type ReceiptDetailsState = {
    selectedId: number | null;
}
type BaseProps = {
    rows: ReceiptItem[]
}

class ReceiptDetails extends React.Component<ReceiptDetailsProps, ReceiptDetailsState> {
    constructor(props: any){
        super(props);
        this.state = {
            selectedId: this.props.rows.length || null
        }
    }
    componentWillReceiveProps(newProps: any){
        if  (newProps !== this.props){
            if (this.props.rows && this.props.rows.length > 0)
                this.setState({selectedId: this.props.rows.length});
            else    
                this.setState({selectedId: null});
        }
    }
    componentDidMount() {
        if (this.el && this.el.current)
            this.el.current.scrollIntoView();
    }
    componentDidUpdate() {
        if (this.el && this.el.current)
            this.el.current.scrollIntoView();
    }
    selectRow(id: number) {
        return (event: any) => {
            this.props.selectReceiptItem(id);
        }
    }
    el = createRef<HTMLDivElement>();
    render() {
        const { rows, classes } = this.props;
        const c = classes || {};
        return <div><Table className={c.table}>
            <TableHead>
                <TableRow>
                    <TableCell className={c.headerCell}>Lp.</TableCell>
                    <TableCell className={c.headerCell}>EAN</TableCell>
                    <TableCell className={c.headerCell}>Nazwa</TableCell>
                    <TableCell className={c.headerCell}>Ilość</TableCell>
                    <TableCell className={c.headerCell}>Jm</TableCell>
                    <TableCell className={c.headerCell}>Cena</TableCell>
                    <TableCell className={c.headerCell}>Wartość</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {rows && rows.map((row: any) => {
                    return (
                        <TableRow key={row.id} hover={true} onClick={this.selectRow(row.id).bind(this)} selected={this.props.selectedReceiptItem == row.id}>
                            <TableCell className={c.cell}>{row.ordinalNumber + "."}</TableCell>
                            <TableCell className={c.cell} component="th" scope="row">{row.ean}</TableCell>
                            <TableCell className={c.cell}>{row.name}</TableCell>
                            <TableCell className={c.cell}>{row.quantity}</TableCell>
                            <TableCell className={c.cell}>{row.unit}</TableCell>
                            <TableCell className={c.cell}>{row.price}</TableCell>
                            <TableCell className={c.cell}>{row.value}</TableCell>
                        </TableRow>
                    );
                })}
            </TableBody>
        </Table>
        <div ref={this.el}/>
        </div>;
    }
}

const mapStateToProps = (store: IRootState) => ({
    selectedReceiptItem: store.receipt.selectedReceiptItem
});

const mapDispatchToProps = { selectReceiptItem };

type StateProps = ReturnType<typeof mapStateToProps>;
type DispatchProps = typeof mapDispatchToProps;
type ReceiptDetailsProps = StyledComponentProps & StateProps & DispatchProps & BaseProps;
export default compose<ReceiptDetailsProps, BaseProps>(
    withStyles(styles),
    connect(
        mapStateToProps,
        mapDispatchToProps
    )
)(ReceiptDetails);