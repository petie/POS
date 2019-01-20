import React, { createRef } from "react";
import { Table, TableHead, TableRow, TableCell, TableBody, Theme, createStyles, withStyles } from "@material-ui/core";

const styles = (theme: Theme) => createStyles({
    table: {
        paddingLeft: theme.spacing.unit,
        paddingRight: theme.spacing.unit        
    },
    cell: {

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

class ReceiptDetails extends React.Component<any, ReceiptDetailsState> {
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
            this.setState({selectedId: id});
        }
    }
    el = createRef<HTMLDivElement>();
    render() {
        const { rows, classes } = this.props;
        return <div><Table className={classes.table}>
            <TableHead>
                <TableRow>
                    <TableCell className={classes.headerCell}>Lp.</TableCell>
                    <TableCell className={classes.headerCell}>EAN</TableCell>
                    <TableCell className={classes.headerCell}>Nazwa</TableCell>
                    <TableCell className={classes.headerCell}>Ilość</TableCell>
                    <TableCell className={classes.headerCell}>Jm</TableCell>
                    <TableCell className={classes.headerCell}>Cena</TableCell>
                    <TableCell className={classes.headerCell}>Wartość</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {rows && rows.map((row: any) => {
                    return (
                        <TableRow key={row.ean} hover={true} onClick={this.selectRow(row.id).bind(this)} selected={this.state.selectedId == row.id}>
                            <TableCell className={classes.cell}>{row.id + "."}</TableCell>
                            <TableCell component="th" scope="row">{row.ean}</TableCell>
                            <TableCell className={classes.cell}>{row.name}</TableCell>
                            <TableCell className={classes.cell}>{row.quantity}</TableCell>
                            <TableCell className={classes.cell}>{row.unit}</TableCell>
                            <TableCell className={classes.cell}>{row.price}</TableCell>
                            <TableCell className={classes.cell}>{row.value}</TableCell>
                        </TableRow>
                    );
                })}
            </TableBody>
        </Table>
        <div ref={this.el}/>
        </div>;
    }
}

export default withStyles(styles)(ReceiptDetails)