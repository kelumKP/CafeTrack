import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button, TextField } from '@mui/material';
import { addEmployee, updateEmployee, getEmployeeById } from '../services/api';

const AddEditEmployee = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [name, setName] = useState('');
  const [role, setRole] = useState('');
  const [cafe, setCafe] = useState('');

  useEffect(() => {
    if (id) {
      fetchEmployeeDetails();
    }
  }, [id]);

  const fetchEmployeeDetails = async () => {
    const data = await getEmployeeById(id);
    setName(data.name);
    setRole(data.role);
    setCafe(data.cafe);
  };

  const handleSubmit = async () => {
    if (id) {
      await updateEmployee(id, { name, role, cafe });
    } else {
      await addEmployee({ name, role, cafe });
    }
    navigate('/employees');
  };

  return (
    <div>
      <TextField label="Name" value={name} onChange={(e) => setName(e.target.value)} fullWidth />
      <TextField label="Role" value={role} onChange={(e) => setRole(e.target.value)} fullWidth />
      <TextField label="Cafe" value={cafe} onChange={(e) => setCafe(e.target.value)} fullWidth />
      <Button onClick={handleSubmit}>{id ? 'Update Employee' : 'Add Employee'}</Button>
    </div>
  );
};

export default AddEditEmployee;
