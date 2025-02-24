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
    <div className="main-content">
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
        <TextField
          label="Filter by Location"
          value={locationFilter}
          onChange={(e) => setLocationFilter(e.target.value)}
          style={{ marginRight: '20px' }}
        />
        <Button onClick={() => navigate('/add-cafe')} variant="contained" color="primary">
          Add New Cafe
        </Button>
      </div>
      <div>
        {cafes.map((cafe) => (
          <CafeCard key={cafe.id} cafe={cafe} onEdit={() => navigate(`/edit-cafe/${cafe.id}`)} onDelete={handleDelete} />
        ))}
      </div>
    </div>
  );
};

export default Cafes;