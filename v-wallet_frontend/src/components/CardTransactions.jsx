import { useEffect, useState } from "react";
import GeneralAxoisService from "../services/GeneralAxoisService";
import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

const CardTransactions = (props) => {
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const [rows, setRows] = useState([])
    const [loader, setLoader] = useState(false)

    useEffect(() => {
        GeneralAxoisService.getMethod("/" + user.customer.id + "/CreditCards/" + props.id + "/Transactions")
        .then((response) => {
            setRows(response.data);
            setLoader(true);
        }).catch((error) => {
            console.error(error);
        });
    }, [props.id, user.customer.id])

    const dateFormatter = (transDate) => {
        const date = new Date(Date.parse(transDate));
        var day = date.getDate();
        var month = date.getMonth() + 1;

        if (day.toString().length === 1) {
            day = "0" + day
        }

        if (month.toString().length === 1) {
            month = "0" + month
        }
        return day + "/" + month + "/" + date.getFullYear()
    }

    const timeFormatter = (transDate) => {
        const date = new Date(Date.parse(transDate));
        var hours = date.getHours()
        var minutes = date.getMinutes()

        if (hours.toString().length === 1) {
            hours = "0" + hours
        }

        if (minutes.toString().length === 1) {
            minutes = "0" + minutes
        }
        return hours + ":" + minutes
    }

    const amountFormatter = (amount) => {
      if(!amount.toString().includes("-"))
        return "+" + amount
      return amount
    }

    return <div className="transactionsContainer">
        {(loader && rows.length === 0 ) && <p className="emptyCards">There are no Transactions on this credit card</p>}
        {(loader && rows.length > 0)  && <div className="transactionsTable"><TableContainer component={Paper}>
    <Table sx={{ minWidth: 'fit-content', border: "1px solid black" }} aria-label="simple table">
      <TableHead>
        <TableRow sx={{ '&:last-child td, &:last-child th': { border: 1 } }}>
          <TableCell sx={{ textAlign: 'center' }}>Amount</TableCell>
          <TableCell sx={{ textAlign: 'center' }}>Time</TableCell>
          <TableCell sx={{ textAlign: 'center' }}>Date</TableCell>
          <TableCell sx={{ textAlign: 'center' }}>Sender/Sent To</TableCell>
        </TableRow>
      </TableHead>
      <TableBody >
        {rows.map((row) => (
          <TableRow
            key={row.transactionId}
            sx={{ 'td, th': { border: 1 } }}
          >
            <TableCell sx={{ textAlign: 'center' }}  component="th" scope="row">
              {amountFormatter(row.amount)}$
            </TableCell>
            <TableCell sx={{ textAlign: 'center' }}>{timeFormatter(row.dateOfTransaction)}</TableCell>
            <TableCell sx={{ textAlign: 'center' }}>{dateFormatter(row.dateOfTransaction)}</TableCell>
            <TableCell sx={{ textAlign: 'center' }}>{row.transactionIssuer}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  </TableContainer></div>}
    </div>
}

export default CardTransactions;
