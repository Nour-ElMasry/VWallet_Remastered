import React from "react";
import { Link } from "react-router-dom";

const Header = (props) => {
    return (
        <header>
            <nav className="navbar container flex flex-jc-sb flex-ai-c">
                <h1 className="brand">v-Wallet</h1>
                {(props.log) && <div className="navbar__links">
                    <a href="/creditCards">CreditCards</a>
                    <a href="/crypto">CryptoWallet</a>
                    <a href="/profile">Profile</a>
                </div>}
            </nav>
        </header>
    )
}

export default Header;