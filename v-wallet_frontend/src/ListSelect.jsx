import { TextField } from '@mui/material';
import MenuItem from '@mui/material/MenuItem';
import { useState } from 'react';

const ListSelect = (props) => {
    var selectOption = props.NoneSelect ? " " : ""
    var formId = props.formId ? props.formId : props.label.toLowerCase()
    const [itemSelected, setItemSelected] = useState(selectOption);

    const itemSelectedChangeHandler = value => {
        setItemSelected(value.target.value);
    }

    return <TextField
      required={props.required}
      select
      fullWidth
      autoComplete='off'
      value={itemSelected}
      label={props.label}
      error={!!props.errors[formId]}
      helperText={props.errors[formId]?.message}
      {...props.register(formId)}
      onChange={itemSelectedChangeHandler}
    >
      {props.NoneSelect && <MenuItem disabled value={" "}>
        None
      </MenuItem>}
      {props.list !== [] && props.list.map((c,i) => {
        return <MenuItem value={c.value} key={i}>{c.label}</MenuItem>
      })}
    </TextField>
}

export default ListSelect;