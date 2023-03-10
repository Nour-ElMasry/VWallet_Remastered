import React from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFile, faTrashCan, faWindowClose } from '@fortawesome/free-regular-svg-icons';
import GeneralAxoisService from "../services/GeneralAxoisService";
import Dialog from '@mui/material/Dialog';
import CardTransactions from "./CardTransactions";
import RemoveCreditCardDialog from "../RemoveCreditCardDialog";

const Card = (props) => {
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const [removeCardDialog, setRemoveCardDialog,] = React.useState(false);

    const handleRemoveCardOpen = () => {
        setRemoveCardDialog(true);
    };

    const handleRemoveCardClose = () => {
        setRemoveCardDialog(false);
    };

    const overlay = document.getElementById("overlay")

    const dateFormatter = (date) => {
        const formattedDate = new Date(Date.parse(date));
        var month = formattedDate.getMonth() + 1;
        if(month.length === 1)
            month = "0" + month
        
        return month + "/" + formattedDate.getFullYear()
    }

    const transAddBtn = () => {
        const transfer = document.getElementById("transferMoney");
        const hasTransFade = [document.querySelector(".has-fade"), document.querySelector(".has-transfade")];
        const senderIban = document.querySelector('[name="senderIban"]')

        transfer.classList.add("transferMoney__active");
        hasTransFade.forEach(x =>{
            x.classList.add("fade-in");
            x.classList.remove("fade-out");
        })
        senderIban.value = props.iban
    }

    const closeTrans = () => { 
        const transfer = document.getElementById("transferMoney");
        const hasTransFade = [document.querySelector(".has-fade"), document.querySelector(".has-transfade")];
        const ibanTrans = document.querySelector('[name="ibanTrans"]')
        const depoTrans = document.querySelector('[name="moneyAmount"]')

        if(transfer.classList.contains("transferMoney__active")){
            transfer.classList.remove("transferMoney__active");
            hasTransFade.forEach(x =>{
                x.classList.remove("fade-in");
                x.classList.add("fade-out");
            })
            ibanTrans.innerHTML = ""
            depoTrans.value = ""
        }
    }

    overlay.onclick = closeTrans

    const confirmTrans = ()=>{
        const ibanTrans = document.querySelector('[name="ibanTrans"]')
        const depoTrans = document.querySelector('[name="moneyAmount"]')
        const senderIban = document.querySelector('[name="senderIban"]')
        
        if(ibanTrans.value === senderIban.value){
            alert("Can't make transaction between the same ibans")
            return;
        }
        const money = depoTrans.value;
        GeneralAxoisService.getMethod("/CreditCards/CheckIban/" + ibanTrans.value)
        .then((res)=>{
            if(res.data){
                props.moneyTrans(senderIban.value, ibanTrans.value, money);
            }else{
                alert("Iban does not exist")
            }
        }).catch((err)=>{
            console.error(err)
            alert("An error occurred")
        });
        
        closeTrans()
    }

    const removeCard = (id) => {
        props.remove(id)
    }

    return (
    <>
    <div className="card flex flex-jc-sb flex-ai-c">
        <div className="card__info">
            <h5 className="flex flex-jc-sb flex-ai-c">
            FUNDS 
            <span className="info_button" onClick={handleClickOpen}><FontAwesomeIcon icon={faFile}/></span>
            </h5>
            <h1 className="cardAmount">{
                parseFloat(props.amount).toLocaleString('en-US')
            }$</h1>
        </div>
        <div className="card__options">
            <div>
                <button className="transferMoneyBtn" onClick={() => {
                    if(props.cards.length > 1){
                        transAddBtn()
                    }else{
                        alert("Must have at least 2 cards to execute transfers!")
                    }
                }}>+ New Transfer</button>
            </div>
            <div className="flex flex-ai-c">
                <RemoveCreditCardDialog open={removeCardDialog} handleClose={handleRemoveCardClose} handleRemove={() => {
                    handleRemoveCardClose()
                    removeCard(props.id)
                }} />
                <button id="removeCard" onClick={handleRemoveCardOpen}><FontAwesomeIcon icon={faTrashCan} style={{marginRight:"5px"}}/> Remove</button>
            </div>
        </div>
    </div>
    <div className="has-transfade flex flex-jc-c flex-ai-c" id="transferMoney">
        <div className="fields">
            <div className="form-group">
                <span>IBAN</span>
                <input pattern="[A-Za-z]{2}\d{18}" autoComplete="off" className="form-field" type="text" placeholder="ROxxxxxxxxxxxxxxxxx" name="ibanTrans"/>
            </div>
            <div className="form-group">
                <span>Money to Transfer</span>
                <input pattern="\d+" autoComplete="off" className="form-field" type="number" placeholder="ex: 20000" name="moneyAmount"/>
            </div>
            <input type="hidden" value={props.iban} name="senderIban"/>
            <button className="cc" onClick={confirmTrans}>Transfer Money</button>
        </div>
        <div className="transPic">
            <img src="./images/transfer.png" alt="img"/>
        </div>
    </div>
    <Dialog
        open={open}
        maxWidth={'100%'}
    >
       <div className="cardInfoDialog">
        <div className="cardInfoDialog__Title flex flex-jc-sb flex-ai-c">
          <h2>Card Information</h2>
          <span style={{cursor: 'pointer'}} onClick={handleClose}><FontAwesomeIcon icon={faWindowClose} size="lg"/></span>
        </div>
        <div className="cardInfoDialog__Info">
            <p>IBAN: <span>{props.iban}</span></p>
            <div className="splitter flex flex-ai-c">
                <p>FUNDS: <span>{parseFloat(props.amount).toLocaleString('en-US')}$</span></p>
                <p>EXP: <span>{dateFormatter(props.exp)}</span></p>
            </div>
        </div>
        <div className="cardInfoDialog__Transactions">
            <h3>Transactions</h3>
            <hr></hr>
            <CardTransactions id={props.id} />
        </div>
       </div>
    </Dialog>
    </>);
}

export default Card;