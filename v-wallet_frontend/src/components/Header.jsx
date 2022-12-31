import React from "react";
import { Link, useNavigate } from "react-router-dom";
import LogoutIcon from '@mui/icons-material/Logout';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';

const Header = (props) => {
    const [open, setOpen] = React.useState(false);
    const navigate = useNavigate();

    const handleClickOpen = () => {
      setOpen(true);
    };

    const handleClose = () => {
      setOpen(false);
    };

    const handleLogOut = () => {
        localStorage.removeItem("User");
        navigate("/");
    }
    
    return (
        <header>
            <nav className="navbar container flex flex-jc-sb flex-ai-c">
                <h1 className="brand">v-Wallet</h1>
                {(props.log) && <div className="flex flex-ai-c navbar__links">
                    <Link to="/creditCards">CreditCards</Link>
                    <Link to="/crypto">CryptoWallet</Link>
                    <Link to="/profile">Profile</Link>
                    <Link className="logOut" onClick={handleClickOpen}><LogoutIcon /></Link>
                </div>}
            </nav>
            <Dialog
              open={open}
            >
              <div className="logOutDialog">
                <DialogTitle>
                  {"Are you sure you want to log out?"}
                </DialogTitle>
                <DialogContent>
                  <DialogContentText>
                    Click on "Log out" if yes, otherwise, click on "Cancel".
                  </DialogContentText>
                </DialogContent>
                <DialogActions>
                  <Button variant="contained" onClick={handleLogOut}>
                    Logout
                  </Button>
                  <Button onClick={handleClose}>Cancel</Button>
                </DialogActions>
              </div>
            </Dialog>
        </header>
    )
}

export default Header;