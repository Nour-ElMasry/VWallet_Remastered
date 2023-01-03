import PropTypes from 'prop-types';
import Box from '@mui/material/Box';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import TableSortLabel from '@mui/material/TableSortLabel';
import Paper from '@mui/material/Paper';
import { visuallyHidden } from '@mui/utils';
import { useEffect, useState } from 'react';
import DateService from '../services/DateService';
import GeneralAxoisService from '../services/GeneralAxoisService';
import { Spinner } from 'react-bootstrap';
import ErrorMsg from "../ErrorMsg";
import DeleteIcon from '@mui/icons-material/Delete';
import { Button } from '@mui/material';

function createData(id, username, name, dateOfBirth, country, city) {
  return {
    id,
    username,
    name,
    dateOfBirth,
    country,
    city,
  };
}

function descendingComparator(a, b, orderBy) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

function getComparator(order, orderBy) {
  return order === 'desc'
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

function stableSort(array, comparator) {
  const stabilizedThis = array.map((el, index) => [el, index]);
  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0]);
    if (order !== 0) {
      return order;
    }
    return a[1] - b[1];
  });
  return stabilizedThis.map((el) => el[0]);
}

const headCells = [
  {
    id: 'username',
    numeric: false,
    disablePadding: false,
    label: 'Username',
  },
  {
    id: 'name',
    numeric: false,
    disablePadding: false,
    label: 'Name',
  },
  {
    id: 'dateOfBirth',
    numeric: false,
    disablePadding: false,
    label: 'Date Of Birth',
  },
  {
    id: 'country',
    numeric: false,
    disablePadding: false,
    label: 'Country',
  },
  {
    id: 'city',
    numeric: false,
    disablePadding: false,
    label: 'City',
  },
];



function EnhancedTableHead(props) {
  const { order, orderBy, onRequestSort } =
    props;
  const createSortHandler = (property) => (event) => {
    onRequestSort(event, property);
  };

  return (
    <TableHead>
      <TableRow>
        {headCells.map((headCell) => (
          <TableCell
            key={headCell.id}
            align='center'
            padding='normal'
            sortDirection={orderBy === headCell.id ? order : false}
          >
            <TableSortLabel
              active={orderBy === headCell.id}
              direction={orderBy === headCell.id ? order : 'asc'}
              onClick={createSortHandler(headCell.id)}
            >
              {headCell.label}
              {orderBy === headCell.id ? (
                <Box component="span" sx={visuallyHidden}>
                  {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                </Box>
              ) : null}
            </TableSortLabel>
          </TableCell>
        ))}
        <TableCell
            align='center'
            padding='normal'
          >Action</TableCell>
      </TableRow>
    </TableHead>
  );
}

EnhancedTableHead.propTypes = {
  onRequestSort: PropTypes.func.isRequired,
  order: PropTypes.oneOf(['asc', 'desc']).isRequired,
  orderBy: PropTypes.string.isRequired,
};

const Users = () => {
  const [order, setOrder] = useState('asc');
  const [orderBy, setOrderBy] = useState('id');
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [user] = useState(JSON.parse(localStorage.getItem("User")));
  const [rows, setRows] = useState([]);
  const [loading, setLoading] = useState(true);
  const [hasError, setHasError] = useState(false);
  const [refresh, setRefresh] = useState(0);

  const getDate = (date) => {
      var dateAge = DateService.dateLongFormat(date);
      return dateAge;
  }

  const DeleteUser = (userId) => {
      GeneralAxoisService.deleteMethod("/" + userId)
      .then(() => setRefresh(prev => prev + 1))
  }

  useEffect(() => {
      GeneralAxoisService.getMethod("/All")
        .then((res) => setRows(res.data.filter(u => user.customer.id !== u.id).map((row) => createData(row.id, row.username, row.name, getDate(row.dateOfBirth), row.userAddress.country, row.userAddress.city))))
        .then(() => setLoading(false))
        .catch((err) => {
            setLoading(false)
            setHasError(true)
    })
  }, [page, refresh, user]);

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === 'asc';
    setOrder(isAsc ? 'desc' : 'asc');
    setOrderBy(property);
  };

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  // Avoid a layout jump when reaching the last page with empty rows.
  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

  return <>
    {loading && <Spinner animation="border" className="spinner loader"/>}
    {hasError && <ErrorMsg msg="Oops! Something went wrong!"/>}
    {!!(!loading & !hasError & rows.length < 1) && <ErrorMsg msg="Oops! There seems to be no users in the database!"/>}
    {!!(!loading & !hasError & rows.length > 0) && <div className='container container--pa'>
      <Paper sx={{ width: '100%', boxShadow: '2px 5px 15px 1px rgba(0,0,0,0.75);' }}>
        <TableContainer>
          <Table
            sx={{ minWidth: 750 }}
            aria-labelledby="tableTitle"
            size='medium'
          >
            <EnhancedTableHead
              order={order}
              orderBy={orderBy}
              onRequestSort={handleRequestSort}
            />
            <TableBody>
              {stableSort(rows, getComparator(order, orderBy))
                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                .map((row, index) => {
                  return (
                    <TableRow
                      hover
                      tabIndex={-1}
                      key={row.id}
                    >
                      <TableCell
                        align="center"
                        scope="row"
                        padding="none"
                      >
                        {row.name}
                      </TableCell>
                      <TableCell align="center">{row.username}</TableCell>
                      <TableCell align="center">{row.dateOfBirth}</TableCell>
                      <TableCell align="center">{row.country}</TableCell>
                      <TableCell align="center">{row.city}</TableCell>
                      <TableCell align="center">
                        <Button className='flex flex-jc-c flex-ai-c' sx={{gap: '0.25rem'}} variant='contained' color='error' onClick={() => DeleteUser(row.id)}><DeleteIcon /> Delete</Button>
                      </TableCell>
                    </TableRow>
                  );
                })}
              {emptyRows > 0 && (
                <TableRow
                  style={{
                    height: 53 * emptyRows,
                  }}
                >
                  <TableCell colSpan={6} />
                </TableRow>
              )}
            </TableBody>
          </Table>
        </TableContainer>
        <TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component="div"
          count={rows.length}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </Paper>
    </div>}
    </>;
}

export default Users; 