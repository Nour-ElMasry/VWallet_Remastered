import React, {useEffect, useState} from "react";
import Card from "./Card";
import GeneralAxoisService from "../services/GeneralAxoisService";
import {Spinner} from "react-bootstrap"

const CardsList = () => {
    const [c, setC] = useState({ cards: []});
    const [loader, setLoader] = React.useState(true);
    const [refreshKey, setRefreshKey] = useState(0);
    
    const iban = document.querySelector('[name="iban"]')
    const cvv = document.querySelector('[name="cvv"]')
    const exp = document.querySelector('[name="exp"]')
    var errFlag = false;

    useEffect(() => {
        getData()
    }, [refreshKey])

    const getData = async () => {
        GeneralAxoisService.getMethod("http://localhost:8080/api/v1/1/creditCards").then((res) => {
            setC({cards: res.data})
            setLoader(false)
        })
    }
    const activateAdd = () => {
        const plus = document.getElementById("addCC");
        const hasFade = [document.querySelector(".has-fade"), document.querySelector(".has-addfade")]
        if(plus.classList.contains("addCC__active")){
            plus.classList.remove("addCC__active");
            hasFade.forEach(x =>{
                x.classList.remove("fade-in");
                x.classList.add("fade-out");
            })
        }else{
            plus.classList.add("addCC__active");
            clearField(iban)
            clearField(cvv)
            clearField(exp)
            hasFade.forEach(x =>{
                x.classList.add("fade-in");
                x.classList.remove("fade-out");
            })
        }
    }

    const patternError = (elem) =>{
        if(elem.validity.patternMismatch){
          elem.style.background = "red";
        }else{
          elem.style.background = "white";
        } 
    };

    const errorCheck = () => {
        patternError(iban)
        patternError(cvv)
        patternError(exp)
        
        if((iban.validity.patternMismatch || iban.value==="")||
        (cvv.validity.patternMismatch || cvv.value==="") ||
        (exp.validity.patternMismatch || exp.value==="")){
            errFlag = true;
        }else{
            errFlag = false;
        }
    }

    const clearField = (elem) => {
        elem.value = ""
    }
    
    const addCard = () => {
        errorCheck()
        if(errFlag === false){
            const [day, month, year] = exp.value.split('-');
            const date1 = new Date([year, month, day].join('-'));
            const date2 = new Date();

            if(date1 > date2){
                GeneralAxoisService.postMethod(
                    "http://localhost:8080/api/v1/1/creditCards",
                    {
                        iban: iban.value,
                        cvv: cvv.value,
                        expiration: exp.value
                    }
                ).then(res =>{
                    setRefreshKey(oldKey => oldKey + 1)
                    clearField(iban)
                    clearField(cvv)
                    clearField(exp)
                })
            }else{
                alert("Please enter a valid date!")
            } 
        }
    }

    const transferMoney = (id, sender, obj) => {
        GeneralAxoisService.updateCard(
            "http://localhost:8080/api/v1/1/creditCards/"+id+"/"+sender, obj
        ).then(res => setRefreshKey(oldKey => oldKey + 1))
    }

    const removeCard = (id) => {
        GeneralAxoisService.removeCard(
            "http://localhost:8080/api/v1/1/creditCards/"+id
        ).then(res => setRefreshKey(oldKey => oldKey + 1))
    }

    const cardsDisplay = () => {
        if(c.cards.length === 0){
            return (
                <p className="emptyCards">There are no credit cards in your account yet!</p>
            )
        }else{
            return (
                <div className="cardsList">
                    {c.cards.sort((c1, c2) => c2.deposit - c1.deposit ).map((x, i) => {
                        return (
                            <Card 
                                key={i}
                                id={x.id}
                                iban={x.iban} 
                                amount={x.deposit}
                                exp={x.expiration}
                                cards={c.cards}
                                moneyTrans={transferMoney}
                                remove={removeCard}
                            />
                        );})
                    }
                </div>
            )
        }
    }

    return (
        <section className="cardsSection container flex flex-jc-c flex-ai-c">
            <div className="has-fade" id="overlay"></div>
            <div className="has-addfade flex flex-jc-c flex-ai-c" id="infoAddCard">
                <div className="fields">
                    <div className="form-group">
                        <span>IBAN</span>
                        <input pattern="[A-Za-z]{2}\d{12}" autoComplete="off" className="form-field" type="text" placeholder="ROxxxxxxxxxxxxxx" name="iban"/>
                    </div>
                    <div className="form-group">
                        <span>Expiration</span>
                        <input pattern="\d{2}[-]\d{2}[-]\d{4}" autoComplete="off" className="form-field" type="text" placeholder="12-12-2012" name="exp"/>
                    </div>
                    <div className="form-group">
                        <span>CVV</span>
                        <input pattern="\d{3}" className="form-field" autoComplete="off" type="text" placeholder="XXX" name="cvv"/>
                    </div>
                    <button className="cc" type="submit" onClick={() => {addCard()}}>Add Credit Card</button>
                </div>
                <div className="walletPic">
                    <img src="./images/wallet.png" alt="img"/>
                </div>
            </div>
            
            {loader ? <Spinner animation="border" className="spinner loader"/> : cardsDisplay()}
        
            <button className="addCC flex flex-ai-c" id="addCC" onClick={activateAdd}>
                <div style={{"position": "relative"}}>
                <span className="plusLine"></span>
                <span className="plusLine"></span>
                </div>
                <span id="addText">Add Credit card</span>
            </button>
        </section>
    )
}

export default CardsList;