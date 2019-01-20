import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';
import MainView from './Components/MainView';
import { createStyles, Theme, withStyles, AppBar } from '@material-ui/core';
import './fonts.css';
import './App.css';

const styles = (theme: Theme) => createStyles({
  root: {
    flexGrow: 1
  }
});
class App extends Component<any, any> {
  render() {
    const { classes } = this.props;
    return (
        <div className={classes.root}>
            <Router>
                <Route path="/" component={MainView} />
            </Router>
      </div>
    );
  }
}

export default withStyles(styles)(App);
