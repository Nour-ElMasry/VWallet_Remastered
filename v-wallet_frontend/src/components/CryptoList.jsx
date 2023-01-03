import React, {useEffect, useState} from "react";
import CryptoCard from "./CryptoCard";
import GeneralAxoisService from "../services/GeneralAxoisService"
import {Spinner} from "react-bootstrap"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMoneyBill1 } from '@fortawesome/free-regular-svg-icons';

const CryptoList = () => {
    const [loader, setLoader] = useState(true);
    const [defaultCryptos, setDefaultCryptos] = useState([]);
    const [cryptos, setCryptos] = useState([]);
    const [deposit, setDeposit] = useState(0); 
    const [refreshKey, setRefreshKey] = useState(0);
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    

    useEffect(() => {
        getCurrencyValues()
    }, [])

    useEffect(() => {
        getCryptos()
        getTotalDeposit()
    }, [refreshKey])

    const getCryptos = () => {
        GeneralAxoisService.getMethod("/" + user.customer.id + "/Crypto/All")
        .then(res => {
            setCryptos(res.data)
        })
    }

    const getTotalDeposit = () => {
        GeneralAxoisService.getMethod("/" + user.customer.id + "/CreditCards/All")
        .then(res => {
            var totalDeposit = 0;
            res.data.forEach(c => {
                totalDeposit += parseFloat(c.deposit);
            });

            setDeposit(totalDeposit)
        })
    }

    const getCurrencyValues = async() => {
        var tmp = await GeneralAxoisService.getDefaultCryptos()
        setDefaultCryptos(tmp)
        setLoader(false)
    }

    const confirmInvest = (name, value, inv) => {
            if(deposit > inv){
                GeneralAxoisService.postMethod("/" + user.customer.id + "/Crypto/Investment", 
                {
                    Name: name.toString(),
                    value: parseFloat(value),
                    investment: parseFloat(inv),
                }
                ).then(res => setRefreshKey(oldKey => oldKey + 1))
                .catch(err => console.error(err))
    
            }else{
                alert("Insuficient funds!")
            }   
    }

    const showTable = () =>{
        return <table className="table">
            <thead className="table__thead">
                <tr className="table__head">
                    <th className="table__th">Crypto Currency</th>
                    <th className="table__th">Current Value</th>
                    <th className="table__th">Coins Owned</th>
                    <th className="table__th">Buy-In Value</th>
                    <th className="table__th">Investment</th>
                    <th className="table__th">Controls</th>
                </tr>
            </thead>
            <tbody>
                {
                    cryptos.sort((c1, c2) => c2.investment - c1.investment).map((c, i) => {
                        return ( <tr className="table__tr" key={i}>
                            <td className="table__td">{c.name}</td>
                            <td className="table__td">{defaultCryptos.filter(cr => cr.name === c.name)[0].worthUSD.toLocaleString('en-US')+"$"}</td>
                            <td className="table__td">{(parseFloat(c.investment)/(defaultCryptos.filter(cr => cr.name === c.name)[0].worthUSD)).toLocaleString('en-US', {maximumFractionDigits: 3})}</td>
                            <td className="table__td">{parseFloat(c.value).toLocaleString('en-US')+"$"}</td>
                            <td className="table__td">{parseFloat(c.investment).toLocaleString('en-US')+"$"}</td>
                            <td className="table__td">
                                <button className="cashOut" onClick={() => {
                                        GeneralAxoisService.deleteMethodWithParams("/" + user.customer.id + "/Crypto/" + c.cryptoId + "/Cash-out/" , {value: parseFloat(defaultCryptos.filter(cr => cr.name === c.name)[0].worthUSD)})
                                        .then(res => setRefreshKey(oldKey => oldKey + 1))
                                        .catch(err => console.error(err))
                                    }
                                }><FontAwesomeIcon icon={faMoneyBill1} style={{marginRight:"5px"}}/>Cash out</button>
                            </td>
                        </tr>)
                    })
                }
            </tbody>
        </table>
    }

    return (
        <section className="cryptoSection container">
        <div className="has-fade" id="overlay"></div>
            <div className="CryptoInv">
                <h2>Crypto Investments: </h2>
                {loader ? <><hr/><Spinner animation="border" className="spinner loader"/></> : 
                    (cryptos.length === 0) ? <><hr/><p className="emptyCards">There are no crypto Investments on your account yet!</p></>: showTable()}
            </div>
            <div className="allCrypto">
                <h2>Crypto Currencies: </h2>
                <hr/>
                {loader ? <Spinner animation="border" className="spinner loader"/> : <>
                <div className="cryptoList flex flex-jc-c flex-ai-c">
                {defaultCryptos.map((c, i)=>{
                    return (
                        <CryptoCard 
                            key={i}
                            name={c.name} 
                            worthUSD={c.worthUSD}
                            own={c.own}
                            img={c.img}
                            totalDeposit={deposit}
                            confirmInvest={confirmInvest}
                        />
                    );
                })}
                </div></>}
            </div>
        </section>
    )
}

export default CryptoList;