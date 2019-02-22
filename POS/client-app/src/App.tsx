import React, { Component } from 'react';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import MainView from './Components/MainView';
import { createStyles, Theme, withStyles } from '@material-ui/core';
import './fonts.css';
import './App.css';
import { Provider } from 'react-redux';
import initStore from './Stores/AppStore';
import {IntlProvider} from 'react-intl'

const store = initStore();

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
        <Provider store={store}>
        <IntlProvider locale="pl" defaultLocale="pl">
            <Router>
                <Route path="/" component={MainView} />
            </Router>
            </IntlProvider>
            </Provider>
      </div>
    );
  }
}

export default withStyles(styles)(App);