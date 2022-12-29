import React, {useEffect, useState} from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrashCan } from '@fortawesome/free-regular-svg-icons';

const Card = (props) => {
    const [loaded, setLoaded] = useState(false)
    useEffect(()=>{
        setLoaded(true) 
    }, [])

    const transfer = document.getElementById("transferMoney");
    const hasFade = [document.querySelector(".has-fade"), document.querySelector(".has-transfade")];
    const ibanTrans = document.querySelector('[name="ibanTrans"]')
    const depoTrans = document.querySelector('[name="moneyTrans"]')
    const expTrans = document.querySelector('[name="expTrans"]')
    const senderId = document.querySelector('[name="senderId"]')

    const overlay = document.getElementById("overlay")

    const transAddBtn = () => {
        transfer.classList.add("transferMoney__active");
        hasFade.forEach(x =>{
            x.classList.add("fade-in");
            x.classList.remove("fade-out");
        })
        senderId.value = props.id
        props.cards.filter(x => x.id != props.id).map((c) =>{
            let opt = document.createElement("option")
            opt.textContent = c.iban
            opt.value = c.id
            ibanTrans.appendChild(opt)
        })
    }

    const closeTrans = () => { 
        if(transfer.classList.contains("transferMoney__active")){
            transfer.classList.remove("transferMoney__active");
            hasFade.forEach(x =>{
                x.classList.remove("fade-in");
                x.classList.add("fade-out");
            })
            ibanTrans.innerHTML = ""
            depoTrans.value = ""
        }
    }

    overlay.onclick = closeTrans

    const confirmTrans = ()=>{
        props.moneyTrans(ibanTrans.value, senderId.value,
        {
            iban: ibanTrans.options[ibanTrans.selectedIndex].text,
            deposit: depoTrans.value,
            expiration: expTrans.value
        });

        closeTrans()
    }

    const removeCard = (id) => {
        props.remove(id)
    }

    

    return (
    <>
        <div className="card">
        <div className="card__info">
            <p className="cardIban"><span>IBAN: </span>{props.iban}</p>
            <p className="cardAmount"><span>AMOUNT: </span>{
                parseFloat(props.amount).toLocaleString('en-US')
            }$</p>
            <p className="cardDeposit"><span>EXP: </span>{props.exp}</p>
        </div>
        <div className="card__options">
            <button className="transferMoneyBtn" onClick={() => {
                if(props.cards.length > 1){
                    transAddBtn()
                }else{
                    alert("Must have at least 2 cards to execute transfers!")
                }
            }}>+ New Transfer</button>
            <button id="removeCard" onClick={() => removeCard(props.id)}><FontAwesomeIcon icon={faTrashCan} style={{marginRight:"5px"}}/> Remove</button>
        </div>
    </div>
    <div className="has-transfade flex flex-jc-c flex-ai-c" id="transferMoney">
        <div className="fields">
            <div className="form-group">
                <span>IBAN</span>
                <select className="form-field"  name="ibanTrans"></select>
            </div>
            <div className="form-group">
                <span>Money to Transfer</span>
                <input pattern="\d+" autoComplete="off" className="form-field" type="text" placeholder="ex: 20000" name="moneyTrans"/>
            </div>
            <input type="hidden" value={props.exp} name="expTrans"/>
            <input type="hidden" value={props.id} name="senderId"/>
            <button className="cc" id="transMoney" onClick={confirmTrans}>Transfer Money</button>
        </div>
        <div className="transPic">
            <img src="./images/transfer.png" alt="img"/>
        </div>
    </div>
    </>);
}

export default Card;