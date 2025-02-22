import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button, TextField, MenuItem, Select, InputLabel, FormControl } from '@mui/material';
import { addEmployee, updateEmployee, getEmployeeById, getCafes } from '../services/api';

const AddEditEmployee = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [name, setName] = useState('');
  const [emailAddress, setEmailAddress] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [gender, setGender] = useState('');
  const [cafeId, setCafeId] = useState('');
  const [cafes, setCafes] = useState([]); // State to store cafes

  useEffect(() => {
    fetchCafes(); // Fetch cafes when the component mounts
    if (id) {
      fetchEmployeeDetails();
    }
  }, [id]);

  const fetchCafes = async () => {
    const data = await getCafes(); // Fetch cafes from the API
    setCafes(data);
  };

  const fetchEmployeeDetails = async () => {
    const data = await getEmployeeById(id);
    setName(data.name);
    setEmailAddress(data.emailAddress);
    setPhoneNumber(data.phoneNumber);
    setGender(data.gender);
    setCafeId(data.cafeId); // Set the cafeId for the employee
  };

  const handleSubmit = async () => {
    const employeeData = {
      Employee: {
        Id: id || "", // Include Id (can be empty for new employees)
        Name: name,
        EmailAddress: emailAddress,
        PhoneNumber: phoneNumber,
        Gender: gender,
        CafeId: cafeId || null, // Include CafeId (can be null)
      },
    };
  
    try {
      if (id) {
        await updateEmployee(id, employeeData);
      } else {
        await addEmployee(employeeData);
      }
      navigate('/employees');
    } catch (error) {
      console.error('Error submitting employee:', error);
    }
  };
  
  return (
    <div>
      <TextField label="Name" value={name} onChange={(e) => setName(e.target.value)} fullWidth margin="normal" />
      <TextField label="Email Address" value={emailAddress} onChange={(e) => setEmailAddress(e.target.value)} fullWidth margin="normal" />
      <TextField label="Phone Number" value={phoneNumber} onChange={(e) => setPhoneNumber(e.target.value)} fullWidth margin="normal" />
      <TextField
        label="Gender"
        value={gender}
        onChange={(e) => setGender(e.target.value)}
        fullWidth
        margin="normal"
        select
      >
        <MenuItem value="Male">Male</MenuItem>
        <MenuItem value="Female">Female</MenuItem>
        <MenuItem value="Other">Other</MenuItem>
      </TextField>

      {/* Cafe Name Dropdown */}
      <FormControl fullWidth margin="normal">
        <InputLabel>Cafe</InputLabel>
        <Select
          value={cafeId}
          onChange={(e) => setCafeId(e.target.value)}
          label="Cafe"
        >
          <MenuItem value="">None</MenuItem>
          {cafes.map((cafe) => (
            <MenuItem key={cafe.id} value={cafe.id}>
              {cafe.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>

      <Button onClick={handleSubmit} variant="contained" color="primary">
        {id ? 'Update Employee' : 'Add Employee'}
      </Button>
    </div>
  );
};

export default AddEditEmployee;