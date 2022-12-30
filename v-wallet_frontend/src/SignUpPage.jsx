import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { Link, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import GeneralAxoisService from './services/GeneralAxoisService';
import { useMemo, useState } from 'react';
import { Alert } from '@mui/material';
import Tooltip from '@mui/material/Tooltip';
import InputAdornment from '@mui/material/InputAdornment';
import InfoIcon from '@mui/icons-material/Info';
import countryList from 'react-select-country-list';
import ListSelect from './ListSelect';

export default function SignUp() {
  const { register, handleSubmit, formState: { errors } } = useForm();
  const [successfulSignUp, setSuccessfulSignUp] = useState(false);
  const [uniqueCheck, setUniqueCheck] = useState(undefined);

  const [openUserNameToolTip, setOpenUserNameToolTip] = useState(false);

  const [openPasswordToolTip, setopenPasswordToolTip] = useState(false);

  const navigate = useNavigate();

  const countries = useMemo(() => countryList().getData().map((c) => {
    return {
      label: c.label,
      value: c.label,
    }
  }), []);

  const handleUniqueCheck = (event) => {
    const username = event.target.value;
    if (username !== "") {
      GeneralAxoisService.getMethod("/unique/" + username)
        .then((res) => setUniqueCheck(res.data));
    } else {
      setUniqueCheck(undefined);
    }
  }

  const handleSignUp = (data) => {
    if (uniqueCheck) {
      const creds = {
        name: data['firstName'] + ' ' + data['lastName'],
        country: data['country'],
        city: data['city'],
        street: data['street'],
        dateOfBirth: data['dateOfBirth'],
        username: data['username'],
        password: data['password'],
      }

      GeneralAxoisService.postMethod("", creds)
        .then((res) => localStorage.setItem("User", JSON.stringify(res.data)))
        .then(() => setTimeout(() => {
          navigate("/");
        }, 3000))
        .catch((err) => {
          setSuccessfulSignUp(false);
        });
    }
  };

  return (
    <section className='LoginSection PositionAboluteMiddle--dt container container--pa'>
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
            Sign up
          </Typography>
          <Box component="form" onSubmit={handleSubmit(handleSignUp)} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12} sm={6}>
                <TextField
                  name="firstName"
                  fullWidth
                  id="firstName"
                  label="First Name"
                  error={!!errors['firstName']}
                  helperText={errors['firstName']?.message}
                  {...register('firstName', { required: '*Field is required' })}
                />
              </Grid>

              <Grid item xs={12} sm={6}>
                <TextField
                  name="lastName"
                  fullWidth
                  id="lastName"
                  label="Last Name"
                  error={!!errors['lastName']}
                  helperText={errors['lastName']?.message}
                  {...register('lastName', { required: '*Field is required' })}
                />
              </Grid>

              <Grid item xs={12} sm={6}>
                <ListSelect
                  NoneSelect
                  list={countries}
                  label="Country"
                  errors={errors}
                  register={register}
                />
              </Grid>

              <Grid item xs={12} sm={6}>
                <TextField
                  id="date"
                  label="Birthday"
                  type="date"
                  sx={{ width: '100%' }}
                  InputLabelProps={{
                    shrink: true,
                  }}
                  error={!!errors['dateOfBirth']}
                  helperText={errors['dateOfBirth']?.message}
                  {...register('dateOfBirth', { required: '*Field is required' })}
                />
              </Grid>

              <Grid item xs={12} sm={6}>
                <TextField
                  name="city"
                  fullWidth
                  id="city"
                  label="City"
                  error={!!errors['city']}
                  helperText={errors['city']?.message}
                  {...register('city', { required: '*Field is required' })}
                />
              </Grid>

              <Grid item xs={12} sm={6}>
                <TextField
                  name="street"
                  fullWidth
                  id="street"
                  label="Street"
                  error={!!errors['street']}
                  helperText={errors['street']?.message}
                  {...register('street', { required: '*Field is required' })}
                />
              </Grid>

              <Grid item xs={12}>
                <TextField
                  fullWidth
                  id="username"
                  label="Username"
                  name="username"
                  onFocus={() => {
                    setOpenUserNameToolTip(true);
                  }}
                  InputProps={{
                    endAdornment: <InputAdornment position="end">
                      <Tooltip
                        open={openUserNameToolTip}
                        onClose={() => {
                          setOpenUserNameToolTip(false);
                        }}
                        disableFocusListener
                        disableHoverListener
                        disableTouchListener
                        title="Username must not contain any special characters">
                        <InfoIcon style={{ color: "hsl(270, 50%, 40%)", cursor: "default" }} />
                      </Tooltip>
                    </InputAdornment>,
                  }}

                  autoComplete='off'
                  error={!!errors['username']}
                  helperText={errors['username']?.message}
                  {...register('username', {
                    required: '*Field is required', pattern: {
                      value: /^^[a-zA-Z0-9]+$/,
                      message: "*Username must not contain any special characters"
                    }
                  })}
                  onBlur={(event) => {
                    setOpenUserNameToolTip(false);
                    handleUniqueCheck(event);
                  }}
                />
                {(uniqueCheck !== undefined && uniqueCheck) && <Alert severity="success"> Username entered is available!</Alert>}
                {(uniqueCheck !== undefined && !uniqueCheck) && <Alert severity="error"> Username entered is not available!</Alert>}
              </Grid>

              <Grid item xs={12}>
                <TextField
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  autoComplete="new-password"
                  onFocus={() => {
                    setopenPasswordToolTip(true);
                  }}
                  InputProps={{
                    endAdornment: <InputAdornment position="end">
                      <Tooltip
                        open={openPasswordToolTip}
                        onClose={() => {
                          setopenPasswordToolTip(false);
                        }}
                        disableFocusListener
                        disableHoverListener
                        disableTouchListener
                        title="Password must be at least 8 characters long and must contain at least 1 capital letter, 1 number and 1 special character.">
                        <InfoIcon style={{ color: "hsl(270, 50%, 40%)", cursor: "default" }} />
                      </Tooltip>
                    </InputAdornment>,
                  }}

                  error={!!errors['password']}
                  helperText={errors['password']?.message}
                  {...register('password', {
                    required: '*Field is required', pattern: {
                      value: /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/,
                      message: "*Password must be at least 8 characters long and must contain at least 1 capital letter, 1 number and 1 special character. "
                    }
                  })}
                  onBlur={() => {
                    setopenPasswordToolTip(false);
                  }}

                />
              </Grid>

            </Grid>
            {successfulSignUp && <Alert severity="success" sx={{ width: '100%' }}>
              Account created successfully
            </Alert>}
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ backgroundColor: 'hsl(270, 50%, 40%)', mt: 3, mb: 2 }}
            >
              Sign Up
            </Button>
            <Grid container>
              <Link className="formLink" to="/">
                Already have an account? Sign in
              </Link>
            </Grid>
          </Box>
        </Box>
      </Container>
    </section>
  );
}            