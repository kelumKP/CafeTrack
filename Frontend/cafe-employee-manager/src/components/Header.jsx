import React from 'react';
import { AppBar, Toolbar, Typography, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const Header = () => {
  const navigate = useNavigate();

  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          Café Management
        </Typography>
        <Button color="inherit" onClick={() => navigate('/cafes')}>
          Cafés
        </Button>
        <Button color="inherit" onClick={() => navigate('/employees')}>
          Employees
        </Button>
      </Toolbar>
    </AppBar>
  );
};

export default Header;