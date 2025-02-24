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
  const [logo, setLogo] = useState(null);

  useEffect(() => {
    if (id) {
      fetchCafeDetails();
    }
  }, [id]);

  const fetchCafeDetails = async () => {
    const data = await getCafeById(id);
    if (data) {
      setName(data.name);
      setDescription(data.description);
      setLocation(data.location);
      setLogo(data.logo);
    } else {
      console.error('Cafe not found');
      navigate('/cafes'); // Redirect to the cafes page if the cafe doesn't exist
    }
  };

  const handleSubmit = async () => {
    const cafeData = {
      id: id || undefined,
      name,
      description,
      location,
      logo,
    };

    if (id) {
      await updateCafe(cafeData);
    } else {
      await addCafe(cafeData);
    }
    navigate('/cafes');
  };

  return (
    <div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto' }}>
      <TextField
        label="Name"
        value={name}
        onChange={(e) => setName(e.target.value)}
        fullWidth
        margin="normal"
      />
      <TextField
        label="Description"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        fullWidth
        margin="normal"
      />
      <TextField
        label="Location"
        value={location}
        onChange={(e) => setLocation(e.target.value)}
        fullWidth
        margin="normal"
      />
      <input
        type="file"
        name="logo"
        onChange={(event) => setLogo(event.target.files[0])}
        style={{ margin: '16px 0' }}
      />
      <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: '20px' }}>
        <Button onClick={handleSubmit} variant="contained" color="primary">
          {id ? 'Update Cafe' : 'Add Cafe'}
        </Button>
      </div>
    </div>
  );
};

export default AddEditCafe;