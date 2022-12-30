import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { useForm } from "react-hook-form";
import { Link, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import Alert from '@mui/material/Alert';
import decodeJwt from 'jwt-decode';
import GeneralAxoisService from './services/GeneralAxoisService';

export default function LogIn() {
    const { register, handleSubmit, formState: { errors }} = useForm();
    const [wrongCredentials, setWrongCredentials] = useState(false);
    const [successfulLogIn, setSuccessfulLogIn] = useState(false);
    const navigate = useNavigate();

    const handleCredentialsSubmit = (data) => {
      var credentials = {
        username: data['username'],
        password: data['password'],
      };
      GeneralAxoisService.postMethod("/Auth", credentials)
      .then((res) => {
        localStorage.setItem("User", JSON.stringify(res.data));
        setWrongCredentials(false);
        setSuccessfulLogIn(true);
        setTimeout(() => {
          if(decodeJwt(res.data.token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === "Admin"){
            navigate("/users");
          }else{
            navigate("/creditCards");
          }
        }, 1500)
      })
      .catch((err) => {
        setWrongCredentials(true);
        setSuccessfulLogIn(false);
      });
    };

    return <>
        <section className='LoginSection PositionAboluteMiddle container container--pa'>
        <Container className="container--styled container--pa" maxWidth="xs">
          <Box
            sx={{
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
            }}
          >
            <Avatar sx={{ m: 1, bgcolor: 'hsl(270, 50%, 40%)' }}>
              <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
              Log in
            </Typography>
            <Box component="form" onSubmit={handleSubmit(handleCredentialsSubmit)}>
              <TextField
                margin="normal"
                required
                fullWidth
                id="username"
                error={!!errors['username']}
                helperText={errors['username']?.message}
                {...register('username', { required: true })}
                label="Username"
                name="username"
                autoComplete='off'
                autoFocus
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                error={!!errors['password']}
                helperText={errors['password']?.message}
                {...register('password', { required: true })}
                autoComplete="current-password"
              />
              {wrongCredentials && <Alert sx={{ width: '100%' }} severity="error">Wrong credentials!</Alert>}
              {successfulLogIn && <Alert severity="success" sx={{ width: '100%' }}>
                  Successfully logged in
                </Alert>}
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ backgroundColor: 'hsl(270, 50%, 40%)', mt: 3, mb: 2 }}
              >
                Sign In
              </Button>
              <Grid container>
                <Link className="formLink" to="/signup">
                    Don't have an account? Sign Up
                </Link>
              </Grid>
            </Box>
          </Box>
        </Container>
        </section>
    </>;
}