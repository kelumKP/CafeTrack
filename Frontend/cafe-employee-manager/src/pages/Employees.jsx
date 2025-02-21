import React, { useEffect, useState } from 'react';
import { getEmployeesByCafe, deleteEmployee } from '../services/api'; // Updated to use getEmployeesByCafe
import { Button, TextField } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import EmployeeCard from '../components/EmployeeCard';

const Employees = () => {
  const [employees, setEmployees] = useState([]);
  const [cafeFilter, setCafeFilter] = useState(''); // State to store cafe filter
  const navigate = useNavigate();

  useEffect(() => {
    fetchEmployees();
  }, [cafeFilter]); // Re-fetch employees when cafeFilter changes

  const fetchEmployees = async () => {
    if (cafeFilter) {
      const data = await getEmployeesByCafe(cafeFilter); // Fetch employees based on cafe filter
      setEmployees(data);
    } else {
      setEmployees([]); // Handle case where no filter is set (optional)
    }
  };

  const handleDelete = async (id) => {
    await deleteEmployee(id);
    fetchEmployees(); // Re-fetch after deleting
  };

  const handleCafeFilterChange = (event) => {
    setCafeFilter(event.target.value); // Update cafe filter
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
