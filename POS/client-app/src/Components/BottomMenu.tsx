import {
  Theme,
  createStyles,
  withStyles,
  Grid,
  TextField
} from "@material-ui/core";

import React from "react";

const styles = (theme: Theme) => createStyles({
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

class BottomMenu extends React.Component<any, BottomMenuState> {
    constructor(props:any){
        super(props);
        this.state = {
            name:""
        }
    }
  handleChange(name: string) {
    return (event: any) => {
      return this.setState({
        [name]: event.target.value
      } as BottomMenuState);
    };
  }
  render() {
    const { classes } = this.props;
    return (
      <Grid container>
        <Grid item md={8}>
          <TextField
            fullWidth
            id="standard-name"
            label="Dodaj produkt"
            variant="outlined"
            className={classes.textField}
            InputProps={{className: classes.inputField}}
            InputLabelProps={{className: classes.label}}
            value={this.state.name}
            onChange={this.handleChange("name")}
            margin="normal"
          />
        </Grid>
      </Grid>
    );
  }
}

export default withStyles(styles)(BottomMenu);
