import React from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCircleUp } from '@fortawesome/free-regular-svg-icons';

const CryptoCard = (props) => {
   
    const overlay = document.getElementById("overlay")
    
    const handleConfirmInv = () => {
        const invName = document.querySelector("#cryptoInvName")
        const moneyInv = document.querySelector('[name="moneyInv"]')
        const marketValue = document.querySelector('[name="marketValue"]')

        props.confirmInvest(invName.textContent, marketValue.value, moneyInv.value)
    }

    const invBtn = () => {
        const cryptoInvestment = document.getElementById("cryptoInvestment");
        const hasFade = [document.querySelector(".has-fade"), document.querySelector(".has-invfade")];
        const invName = document.querySelector("#cryptoInvName")
        const marketValue = document.querySelector('[name="marketValue"]')

        cryptoInvestment.classList.add("cryptoInvestment__active");
        hasFade.forEach(x =>{
            x.classList.add("fade-in");
            x.classList.remove("fade-out");
        })
        invName.textContent = props.name
        marketValue.value = props.worthUSD
    }

    const closeInv= () => { 
        const cryptoInvestment = document.getElementById("cryptoInvestment");
        const hasFade = [document.querySelector(".has-fade"), document.querySelector(".has-invfade")];
        const moneyInv = document.querySelector('[name="moneyInv"]')

        if(cryptoInvestment.classList.contains("cryptoInvestment__active")){
            cryptoInvestment.classList.remove("cryptoInvestment__active");
            hasFade.forEach(x =>{
                x.classList.remove("fade-in");
                x.classList.add("fade-out");
            })
            moneyInv.innerHTML = ""
        }
    }

    overlay.onclick = closeInv
   
    return (
    <>
    <div className="crypto">
        <div className="crypto__info flex flex-ai-c flex-jc-sb">
            <div>
                <p className="cryptoName">{props.name}</p>
            </div>
            <img src={props.img} alt="crypto"/>
        </div>
        <p className="cryptoAmount"><span>Market value:</span> {parseFloat(props.worthUSD).toLocaleString('en-US')}$</p>
        <div className="crypto__options">
            <button onClick={()=>invBtn()} id="invBtn"><FontAwesomeIcon icon={faCircleUp} style={{marginRight:"5px"}}/> Buy coins</button>
        </div>
    </div>
    <div className="has-invfade" id="cryptoInvestment">
        <div className="fields">
            <h3 id="cryptoInvName"></h3>
            <div className="form-group">
                <span>Total Deposit</span>
                <input type="text" className="form-field" value={parseFloat(props.totalDeposit).toLocaleString('en-US')+"$"} readOnly/>
            </div>
            <div className="form-group">
                <span>Amount to Invest</span>
                <input pattern="\d+" autoComplete="off" className="form-field" type="text" placeholder="ex: 5000" name="moneyInv"/>
            </div>
            <input type="hidden" name="marketValue"></input>
            <button className="cc" id="investMoney" onClick={handleConfirmInv}>Invest Money</button>
        </div>
    </div>
    </>
    );
}

export default CryptoCard;