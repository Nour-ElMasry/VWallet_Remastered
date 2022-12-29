import React, {useState, useEffect} from "react";
import GeneralAxoisService from "../services/GeneralAxoisService";

const UserInfo = (props) => {
    const [loaded, setLoaded] = useState(false)
    const [defCryptos, setDefCryptos] = useState([])

    useEffect(()=>{
        getCryptoValues()
    }, [])

    const getCryptoValues = async() => {
        var tmp = await GeneralAxoisService.getDefaultCryptos()
        setDefCryptos(tmp)
        setLoaded(true)
    }

    var totalDeposit = 0;
    props.user.creditCards.forEach(c => {
        totalDeposit += parseFloat(c.deposit);
    });

    if(loaded){
        props.user.cryptoCurrencies.forEach(c => {
            totalDeposit += ((defCryptos.filter(cr => cr.name === c.name)[0].worthUSD) * (parseFloat(c.investment)/parseFloat(c.value)));
        });
    }
    
    const cryptoDisplay = () => {
        if(props.user.cryptoCurrencies.length === 0){
            return (
                <ul className="cryptoList">
                    <li>No Investments yet</li>
                </ul>
            )
        }else{
            return (
                <ul className="cryptoList">
                        {
                            [...new Set(props.user.cryptoCurrencies.map(i => i.name))].map((c, i)=>{
                            return <li key={i}>{c}</li>
                            })
                        }
                </ul>
            )
        }
    }

    return <div className="user flex flex-ai-center">
        <div className="user_leftSide">
            <img className="userImg" src="https://i.pinimg.com/736x/28/ec/f8/28ecf8709cc250f00ffd1cc81607cc84.jpg" alt="userImg"></img>
            <h1 className="userName">{props.user.name}</h1>
            <p className="userEmail">{props.user.email}</p>
            <p className="userPhone ">{props.user.phone}</p>
        </div>
        <div className="user_rightSide">
            <p>- Date of birth: <span>{props.user.dateOfBirth}</span></p>
            <p>- Address: <span>{props.user.address.street}, {props.user.address.city}, {props.user.address.country}</span></p>
            <p>- Total Deposit: {totalDeposit.toLocaleString('en-US')}$</p>
            <p>- Number of CreditCards: {props.user.creditCards.length}</p>
            <p>- Cypto investments: </p>
            {cryptoDisplay()}
        </div>
    </div>
}

export default UserInfo;