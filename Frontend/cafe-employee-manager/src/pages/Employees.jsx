import React, { useEffect, useState } from 'react';
import { getEmployeesByCafe, getEmployeeById, deleteEmployee } from '../services/api'; // Add getEmployeeById
import { Button, TextField } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import EmployeeCard from '../components/EmployeeCard';

const Employees = () => {
  const [employees, setEmployees] = useState([]);
  const [cafeFilter, setCafeFilter] = useState(''); // State to store cafe filter
  const [idFilter, setIdFilter] = useState(''); // State to store ID filter
  const navigate = useNavigate();

  useEffect(() => {
    if (idFilter) {
      fetchEmployeeById();
    } else {
      fetchEmployees();
    }
  }, [cafeFilter, idFilter]); // Re-fetch employees when either cafeFilter or idFilter changes

  const fetchEmployees = async () => {
    if (cafeFilter) {
      const data = await getEmployeesByCafe(cafeFilter); // Fetch employees based on cafe filter
      setEmployees(data);
    } else {
      setEmployees([]); // Handle case where no filter is set (optional)
    }
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
    setCafeFilter(event.target.value); // Update cafe filter
  };

  const handleIdFilterChange = (event) => {
    setIdFilter(event.target.value); // Update ID filter
  };

  return (
    <div>
      <Button onClick={() => navigate('/add-employee')}>Add New Employee</Button>
      
      <TextField
        label="Filter by Cafe"
        value={cafeFilter}
        onChange={handleCafeFilterChange} // Allow user to input cafe filter
        variant="outlined"
        fullWidth
        margin="normal"
      />

      <TextField
        label="Filter by Employee ID"
        value={idFilter}
        onChange={handleIdFilterChange} // Allow user to input employee ID
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
