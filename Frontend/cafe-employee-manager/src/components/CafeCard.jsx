import React from 'react';
import { Card, CardContent, Typography, Button } from '@mui/material';

const CafeCard = ({ cafe, onEdit, onDelete }) => {
  return (
    <Card sx={{ mb: 2 }}>
      <CardContent>
        <Typography variant="h6">{cafe.name}</Typography>
        <Typography variant="body2">{cafe.description}</Typography>
        <Typography variant="body2" color="textSecondary">
          Location: {cafe.location}
        </Typography>
        <Button onClick={() => onEdit(cafe.id)}>Edit</Button>
        <Button onClick={() => onDelete(cafe.id)} color="error">
          Delete
        </Button>
      </CardContent>
    </Card>
  );
};

export default CafeCard;
