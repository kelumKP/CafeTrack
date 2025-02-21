import React from 'react';
import { Drawer, List, ListItem, ListItemText } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const Sidebar = ({ open, onClose }) => {
  const navigate = useNavigate();

  return (
    <Drawer anchor="left" open={open} onClose={onClose}>
      <List>
        <ListItem button onClick={() => navigate('/cafes')}>
          <ListItemText primary="Cafes" />
        </ListItem>
        <ListItem button onClick={() => navigate('/employees')}>
          <ListItemText primary="Employees" />
        </ListItem>
        <ListItem button onClick={() => navigate('/add-cafe')}>
          <ListItemText primary="Add Cafe" />
        </ListItem>
        <ListItem button onClick={() => navigate('/add-employee')}>
          <ListItemText primary="Add Employee" />
        </ListItem>
      </List>
    </Drawer>
  );
};

export default Sidebar;
