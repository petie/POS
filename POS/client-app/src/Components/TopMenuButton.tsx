import { createStyles, Theme, withStyles, Grid, Button, WithStyles } from "@material-ui/core";
import React from "react";

type TopMenuButtonProps = WithStyles & {
    text: string;
}
const styles = (theme: Theme) => createStyles({
    button: {
        height: theme.spacing.unit * 14,
        marginTop: theme.spacing.unit * 2,
        marginBottom: theme.spacing.unit * 2
      }
});

class TopMenuButton extends React.Component<TopMenuButtonProps> {
    render() {
        const {classes} = this.props;
        return <Grid item md={2}>
        <Button
          fullWidth
          variant="outlined"
          size="large"
          className={classes.button}
        >
          <Grid container>
            <Grid item md={12}>
              {this.props.children}
            </Grid>
            <Grid item md={12}>
              {this.props.text}
            </Grid>
          </Grid>
        </Button>
      </Grid>
    }
}

export default withStyles(styles)(TopMenuButton);