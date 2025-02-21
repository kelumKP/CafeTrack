import React, { useEffect, useState } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-material.css';
import { getCafes, deleteCafe } from '../services/api';
import { Button, TextField } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import CafeCard from '../components/CafeCard';

const Cafes = () => {
  const [cafes, setCafes] = useState([]);
  const [locationFilter, setLocationFilter] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    fetchCafes();
  }, [locationFilter]);

  const fetchCafes = async () => {
    const data = await getCafes(locationFilter);
    setCafes(data);
  };

  const handleDelete = async (id) => {
    await deleteCafe(id);
    fetchCafes();
  };

  return (
    <div>
      <TextField
        label="Filter by Location"
        value={locationFilter}
        onChange={(e) => setLocationFilter(e.target.value)}
      />
      <Button onClick={() => navigate('/add-cafe')}>Add New Cafe</Button>
      <div>
        {cafes.map((cafe) => (
          <CafeCard key={cafe.id} cafe={cafe} onEdit={() => navigate(`/edit-cafe/${cafe.id}`)} onDelete={handleDelete} />
        ))}
      </div>
    </div>
  );
};

export default Cafes;