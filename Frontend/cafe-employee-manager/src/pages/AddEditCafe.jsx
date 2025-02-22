import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button, TextField} from '@mui/material';
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
      name,
      description,
      location,
      logo,
      employeeIds: [] // Add employee IDs if needed
    };

    if (id) {
      await updateCafe(id, cafeData);
    } else {
      await addCafe(cafeData);
    }
    navigate('/cafes');
  };

  return (
    <div>
      <TextField
        label="Name"
        value={name}
        onChange={(e) => setName(e.target.value)}
        fullWidth
      />
      <TextField
        label="Description"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        fullWidth
      />
      <TextField
        label="Location"
        value={location}
        onChange={(e) => setLocation(e.target.value)}
        fullWidth
      />
      <input
        type="file"
        name="logo"
        onChange={(event) => setLogo(event.target.files[0])}
      />
      <Button onClick={handleSubmit}>{id ? 'Update Cafe' : 'Add Cafe'}</Button>
    </div>
  );
};

export default AddEditCafe;