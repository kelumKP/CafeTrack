import React from 'react';
import { TextField } from '@mui/material';

const TextBox = ({ label, value, onChange, type = 'text', ...props }) => {
  return (
    <TextField
      label={label}
      value={value}
      onChange={onChange}
      type={type}
      variant="outlined"
      fullWidth
      {...props}
    />
  );
};

export default TextBox;