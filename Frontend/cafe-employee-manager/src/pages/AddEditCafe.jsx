import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button, TextField } from '@mui/material';
import { addCafe, updateCafe, getCafeById } from '../services/api';

const AddEditCafe = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [location, setLocation] = useState('');

  useEffect(() => {
    if (id) {
      fetchCafeDetails();
    }
  }, [id]);

  const fetchCafeDetails = async () => {
    const data = await getCafeById(id);
    setName(data.name);
    setDescription(data.description);
    setLocation(data.location);
  };

  const handleSubmit = async () => {
    if (id) {
      await updateCafe(id, { name, description, location });
    } else {
      await addCafe({ name, description, location });
    }
    navigate('/cafes');
  };

  return (
    <div>
      <TextField label="Name" value={name} onChange={(e) => setName(e.target.value)} fullWidth />
      <TextField label="Description" value={description} onChange={(e) => setDescription(e.target.value)} fullWidth />
      <TextField label="Location" value={location} onChange={(e) => setLocation(e.target.value)} fullWidth />
      <Button onClick={handleSubmit}>{id ? 'Update Cafe' : 'Add Cafe'}</Button>
    </div>
  );
};

export default AddEditCafe;