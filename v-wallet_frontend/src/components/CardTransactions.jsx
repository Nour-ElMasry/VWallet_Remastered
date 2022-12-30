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
            console.log(response.data);
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

    return <>
        {loader && <TableContainer component={Paper}>
    <Table sx={{ minWidth: 'fit-content', border: "1px solid black" }} aria-label="simple table">
      <TableHead>
        <TableRow>
          <TableCell>Amount</TableCell>
          <TableCell >Time</TableCell>
          <TableCell >Date</TableCell>
          <TableCell >Sender/Sent To</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {rows.map((row) => (
          <TableRow
            key={row.transactionId}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
          >
            <TableCell component="th" scope="row">
              {row.amount}
            </TableCell>
            <TableCell>{timeFormatter(row.dateOfTransaction)}</TableCell>
            <TableCell>{dateFormatter(row.dateOfTransaction)}</TableCell>
            <TableCell>{row.transactionIssuer}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  </TableContainer>}
    </>
}

export default CardTransactions;
