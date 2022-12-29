import React, {useState, useEffect} from "react";
import Header from "./components/Header";
import Login from "./Login";
import CreditCards from "./CreditCards";
import Crypto from "./Crypto";
import Profile from "./Profile";
import Footer from "./components/Footer";
import {BrowserRouter as Router, Routes, Route} from "react-router-dom";

const App = () => {
    const checkUser = (username, password) => {
        if(username === "Nour" && password === "Nour1234"){
            window.location.href = "/creditCards";
        }else{
            alert("Wrong Credentials")
        }
    }

    return <>
        {(window.location.href === "http://localhost:3000/") ? <Header log={false}/> : <Header log={true}/>}
        <Router>
        <Routes>
                <Route path="/" element={<Login check={checkUser}/>}/>
                <Route path="/creditCards" element={<CreditCards />}/>
                <Route path="/crypto" element={<Crypto />}/>
                <Route path="/profile" element={<Profile />}/>
        </Routes>
        
        </Router>
        <Footer/>
    </>
}

export default App;