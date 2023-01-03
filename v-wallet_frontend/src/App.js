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
import AdminPageLayout from "./components/AdminPageLayout";
import Users from "./components/Users";
import AdminsCryptos from "./components/AdminCryptos";
import AdminCreditCards from "./components/AdminCreditCards";

const App = () => {

    return <>
        {/* {(window.location.href === "http://localhost:3000/" || window.location.href === "http://localhost:3000/signup") ? <Header log={false}/> : <Header log={true}/>} */}
        <Router>
        <Routes>
                <Route path="/" element={<>
                    <Header />
                    <LogIn/>
                </>}/>
                <Route path="/signup" element={<>
                    <Header />
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
                <Route path="admin/users" element={<AdminPageLayout>
                    <Users />
                </AdminPageLayout>}/>
                <Route path="admin/creditCards" element={<AdminPageLayout>
                    <AdminCreditCards />
                </AdminPageLayout>}/>
                <Route path="admin/crypto" element={<AdminPageLayout>
                    <AdminsCryptos />
                </AdminPageLayout>}/>
                <Route path="*" element={<>
                    <Header />
                    <NotFound />
                </>}/>
        </Routes>
        
        </Router>
        <Footer/>
    </>
}

export default App;