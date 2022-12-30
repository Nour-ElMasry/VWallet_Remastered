import React from 'react';
import Button from '@mui/material/Button';
import { useNavigate } from 'react-router';

const NotFound = () => {
  const navigate = useNavigate();

  const handleOnClick = () => {
    navigate("/")
  }
  return (
    <div className="flex flex-jc-c flex-ai-c NotFoundContainer">
      <div>
        <h1>404 - Page Not Found</h1>
        <p>We're sorry, but the page you requested was not found.</p>
      </div>
      <Button variant="contained" onClick={handleOnClick}>Go Back</Button>
    </div>
  );
};

export default NotFound;