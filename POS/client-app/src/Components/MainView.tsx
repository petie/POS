import React from "react";
import ReceiptView from "./ReceiptView";
import TopMenu from "./TopMenu";
import TillManagement from "./TillManagement";
import {
  Paper,
  Theme,
  createStyles,
  withStyles,
  AppBar,
  Toolbar,
  Typography,
  Divider
} from "@material-ui/core";
import BottomMenu from "./BottomMenu";

const styles = (theme: Theme) =>
  createStyles({
    layout: {
      width: "auto",
      marginLeft: theme.spacing.unit * 3,
      marginRight: theme.spacing.unit * 3,
      marginTop: theme.spacing.unit * 3,
      [theme.breakpoints.up(1100 + theme.spacing.unit * 3 * 2)]: {
        marginLeft: theme.spacing.unit * 2,
        marginRight: theme.spacing.unit * 2
      }
    },
    divider: {
      marginTop: theme.spacing.unit * 2
    },
    paper: {
        marginBottom: theme.spacing.unit * 2
    }
  });

class MainView extends React.Component<any, any> {
  componentDidMount(){
    
  }
  render() {
    const { classes } = this.props;
    return (
      <div>
        <AppBar position="static" color="primary">
          <Toolbar variant="dense">
            <Typography variant="h6" color="inherit">
              Biovert POS
            </Typography>
          </Toolbar>
        </AppBar>
        <div className={classes.layout}>
          <Paper className={classes.paper}>
            <TopMenu />
          </Paper>
          <Paper className={classes.paper}>
            <ReceiptView />
          </Paper>
          <Paper className={classes.paper}>
            <BottomMenu />
          </Paper>
          <TillManagement />
        </div>
      </div>
    );
  }
}

export default withStyles(styles)(MainView);
