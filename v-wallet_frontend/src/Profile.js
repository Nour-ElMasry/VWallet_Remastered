import React, {useState, useEffect} from "react";
import GeneralAxoisService from "./services/GeneralAxoisService";
import UserInfo from "./components/UserInfo";
import {Spinner} from "react-bootstrap"

const Profile = () => {
    const [token] = useState(JSON.parse(localStorage.getItem("User")));

    const [user, setUser] = useState({
        id: 0,
        name:"",
        username:"",
        phone: "",
        dateOfBirth: "",
        address: {},
    });
    const [loader, setLoader] = React.useState(true);

    useEffect(() => {
        GeneralAxoisService.getMethod("/" + token.customer.id).then((res) => {
            setUser({
                id: res.data.id,
                username: res.data.username,
                name: res.data.name,
                dateOfBirth: res.data.dateOfBirth,
                address: res.data.userAddress,
            });
            setLoader(false);
        });
    }, [])

    return <section className="profileSection container">
        {loader ? <Spinner animation="border" className="spinner loader"/>:<UserInfo user={user}/>}
    </section>
}

export default Profile;
