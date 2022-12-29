import React from "react";

const Footer = () => {
    var year = new Date().getFullYear();
    return <footer>
        <p>Copyright &copy; {year}</p>
    </footer>
}

export default Footer;