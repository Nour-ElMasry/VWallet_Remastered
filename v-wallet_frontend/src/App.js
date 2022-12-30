import React from "react";
import Header from "./components/Header";
import CreditCards from "./CreditCards";
import Crypto from "./Crypto";
import Profile from "./Profile";
import Footer from "./components/Footer";
import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import LogIn from "./SignInPage";
import SignUp from "./SignUpPage";
import NotFound from "./NotFound";


const App = () => {

    return <>
        {(window.location.href === "http://localhost:3000/" || window.location.href === "http://localhost:3000/signup") ? <Header log={false}/> : <Header log={true}/>}
        <Router>
        <Routes>
                <Route path="/" element={<LogIn/>}/>
                <Route path="/signup" element={<SignUp/>}/>
                <Route path="/creditCards" element={<CreditCards />}/>
                <Route path="/crypto" element={<Crypto />}/>
                <Route path="/profile" element={<Profile />}/>
                <Route path="*" element={<NotFound />}/>
        </Routes>
        
        </Router>
        <Footer/>
    </>
}

export default App;