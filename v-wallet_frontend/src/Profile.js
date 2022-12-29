import React, {useState, useEffect} from "react";
import GeneralAxoisService from "./services/GeneralAxoisService";
import UserInfo from "./components/UserInfo";
import {Spinner} from "react-bootstrap"

const Profile = () => {
    const [user, setUser] = useState({
        id: 0,
        name:"",
        email:"",
        phone: "",
        dateOfBirth: "",
        address: {},
        creditCards: [],
        cryptoCurrencies: []
    });
    const [loader, setLoader] = React.useState(true);

    useEffect(() => {
        GeneralAxoisService.getMethod("http://localhost:8080/api/v1/1/profile").then((res) => {
            setUser({
                id: res.data.id,
                name: res.data.name,
                email: res.data.email,
                phone: res.data.phone,
                dateOfBirth: res.data.dateOfBirth,
                address: res.data.address,
                creditCards: res.data.creditCards,
                cryptoCurrencies: res.data.cryptoCurrencies
            });
            setLoader(false);
        });
    }, [])

    return <section className="profileSection container">
        {loader ? <Spinner animation="border" className="spinner loader"/>:<UserInfo user={user}/>}
    </section>
}

export default Profile;
