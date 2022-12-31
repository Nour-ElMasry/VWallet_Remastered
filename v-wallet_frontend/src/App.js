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
import LoggedPageLayout from "./components/LoggedPageLayout";


const App = () => {

    return <>
        {/* {(window.location.href === "http://localhost:3000/" || window.location.href === "http://localhost:3000/signup") ? <Header log={false}/> : <Header log={true}/>} */}
        <Router>
        <Routes>
                <Route path="/" element={<>
                    <Header log={false}/>
                    <LogIn/>
                </>}/>
                <Route path="/signup" element={<>
                    <Header log={false}/>
                    <SignUp/>
                </>}/>
                <Route path="/creditCards" element={<LoggedPageLayout>
                    <CreditCards />
                </LoggedPageLayout>}/>
                <Route path="/crypto" element={<LoggedPageLayout>
                    <Crypto />
                </LoggedPageLayout>}/>
                <Route path="/profile" element={<LoggedPageLayout>
                    <Profile />
                </LoggedPageLayout>}/>
                <Route path="*" element={<>
                    <Header log={false}/>
                    <NotFound />
                </>}/>
        </Routes>
        
        </Router>
        <Footer/>
    </>
}

export default App;