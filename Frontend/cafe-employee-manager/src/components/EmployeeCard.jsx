import React from 'react';
import { Card, CardContent, Typography, Button } from '@mui/material';

const EmployeeCard = ({ employee, onEdit, onDelete }) => {
  return (
    <Card sx={{ mb: 2 }}>
      <CardContent>
        <Typography variant="h6">{employee.name}</Typography>
        <Typography variant="body2">{employee.role}</Typography>
        <Typography variant="body2" color="textSecondary">
          Café: {employee.cafe}
        </Typography>
        <Button onClick={() => onEdit(employee.id)}>Edit</Button>
        <Button onClick={() => onDelete(employee.id)} color="error">
          Delete
        </Button>
      </CardContent>
    </Card>
  );
};

export default EmployeeCard;
