import React, { useEffect, useState } from 'react';
import { getEmployeesByCafe, getEmployeeById, deleteEmployee } from '../services/api'; // Add getEmployeeById
import { Button, TextField } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import EmployeeCard from '../components/EmployeeCard';

const Employees = () => {
  const [employees, setEmployees] = useState([]);
  const [filter, setFilter] = useState({ cafe: '', id: '' }); // State to store cafe filter
  const [idFilter, setIdFilter] = useState(''); // State to store ID filter
  const navigate = useNavigate();

  useEffect(() => {
    fetchEmployees();
  }, [filter]);
  
  
  const fetchEmployees = async () => {
    let data = [];
    if (filter.id) {
      const employee = await getEmployeeById(filter.id);
      data = employee ? [employee] : []; // Convert to array
    } else if (filter.cafe) {
      data = await getEmployeesByCafe(filter.cafe);
    }
    setEmployees(data);
  };
  

  const fetchEmployeeById = async () => {
    if (idFilter) {
      const data = await getEmployeeById(idFilter); // Fetch employee by ID
      setEmployees([data]); // Assuming the response is a single employee object
    }
  };

  const handleDelete = async (id) => {
    await deleteEmployee(id);
    fetchEmployees(); // Re-fetch after deleting
  };

  const handleCafeFilterChange = (event) => {
    setFilter({ ...filter, cafe: event.target.value, id: '' }); // Corrected state update
  };

  const handleIdFilterChange = (event) => {
    setFilter({ ...filter, id: event.target.value, cafe: '' }); // Update filter.id
  };

  return (
    <div>
      <Button onClick={() => navigate('/add-employee')}>Add New Employee</Button>
      
      <TextField
  label="Filter by Employee ID"
  value={filter.id}
  onChange={(e) => setFilter({ ...filter, id: e.target.value, cafe: '' })}
  variant="outlined"
  fullWidth
  margin="normal"
/>
<TextField
  label="Filter by Cafe"
  value={filter.cafe}
  onChange={(e) => setFilter({ ...filter, cafe: e.target.value, id: '' })}
  variant="outlined"
  fullWidth
  margin="normal"
/>

      <div>
        {employees.map((employee) => (
          <EmployeeCard
            key={employee.id}
            employee={employee}
            onEdit={() => navigate(`/edit-employee/${employee.id}`)}
            onDelete={handleDelete}
          />
        ))}
      </div>
    </div>
  );
};

export default Employees;
