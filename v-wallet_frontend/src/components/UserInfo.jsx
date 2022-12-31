import React, {useState, useEffect} from "react";
import GeneralAxoisService from "../services/GeneralAxoisService";

const UserInfo = (props) => {
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const [loaded, setLoaded] = useState(false)
    const [defCryptos, setDefCryptos] = useState([])
    const [creditCards, setCreditCards] = useState([])
    const [cryptoCurrencies, setCryptoCurrencies] = useState([])

    useEffect(()=>{
        getCryptoValues()
        GeneralAxoisService.getMethod("/" + user.customer.id + "/CreditCards/All")
        .then((response) => {
            setCreditCards(response.data)
        }).catch((error) =>{
            console.log(error)
        });

        GeneralAxoisService.getMethod("/" + user.customer.id + "/Crypto/All")
        .then((response) => {
            setCryptoCurrencies(response.data)
        }).catch((error) =>{
            console.log(error)
        });
    }, [])

    const getCryptoValues = async() => {
        var tmp = await GeneralAxoisService.getDefaultCryptos()
        setDefCryptos(tmp)
        setLoaded(true)
    }

    var totalDeposit = 0;
    creditCards.forEach(c => {
        totalDeposit += parseFloat(c.deposit);
    });

    if(loaded){
        cryptoCurrencies.forEach(c => {
            totalDeposit += ((defCryptos.filter(cr => cr.name === c.name)[0].worthUSD) * (parseFloat(c.investment)/parseFloat(c.value)));
        });
    }
    
    const cryptoDisplay = () => {
        if(cryptoCurrencies.length === 0){
            return (
                <ul className="cryptoList">
                    <li>No Investments yet</li>
                </ul>
            )
        }else{
            return (
                <ul className="cryptoList">
                        {
                            [...new Set(cryptoCurrencies.map(i => i.name))].map((c, i)=>{
                            return <li key={i}>{c}</li>
                            })
                        }
                </ul>
            )
        }
    }

    const dateFormatter = () => {
        var date = new Date(props.user.dateOfBirth);
        var day = date.getDate();
        var month = date.getMonth() + 1;

        if (day.toString().length === 1) {
            day = "0" + day
        }

        if (month.toString().length === 1) {
            month = "0" + month
        }
        return day + '/' + month + '/' + date.getFullYear()
    }

    return <div className="user flex flex-ai-center">
        <div className="user_leftSide">
            <img className="userImg" src="https://i.pinimg.com/736x/28/ec/f8/28ecf8709cc250f00ffd1cc81607cc84.jpg" alt="userImg"></img>
            <h1 className="userName">{props.user.name}</h1>
            <p className="userEmail">{props.user.username}</p>
        </div>
        <div className="user_rightSide">
            <p>- Date of birth: <span>{dateFormatter()}</span></p>
            <p>- Address: <span>{props.user.address.street}, {props.user.address.city}, {props.user.address.country}</span></p>
            <p>- Total Deposit: {totalDeposit.toLocaleString('en-US')}$</p>
            <p>- Number of CreditCards: {creditCards.length}</p>
            <p>- Cypto investments: </p>
            {cryptoDisplay()}
        </div>
    </div>
}

export default UserInfo;