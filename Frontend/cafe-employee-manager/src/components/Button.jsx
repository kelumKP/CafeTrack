import React from 'react';
import { Button as MuiButton } from '@mui/material';

const Button = ({ onClick, children, variant = 'contained', color = 'primary', ...props }) => {
  return (
    <MuiButton onClick={onClick} variant={variant} color={color} {...props}>
      {children}
    </MuiButton>
  );
};

export default Button;
