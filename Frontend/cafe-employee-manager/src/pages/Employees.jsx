import React, { useEffect, useState } from 'react';
import { getEmployeesByCafe, getEmployeeById, deleteEmployee } from '../services/api';
import { Button, TextField } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import EmployeeCard from '../components/EmployeeCard';

const Employees = () => {
  const [employees, setEmployees] = useState([]);
  const [filter, setFilter] = useState({ cafe: '', id: '' });
  const navigate = useNavigate();

  useEffect(() => {
    fetchEmployees();
  }, [filter]);

  const fetchEmployees = async () => {
    let data = [];
    if (filter.id) {
      const employee = await getEmployeeById(filter.id);
      data = employee ? [employee] : [];
    } else if (filter.cafe) {
      data = await getEmployeesByCafe(filter.cafe);
    }
    setEmployees(data);
  };

  const handleDelete = async (id) => {
    await deleteEmployee(id);
    fetchEmployees(); // Refresh the list after deletion
  };

  const handleCafeFilterChange = (event) => {
    setFilter({ ...filter, cafe: event.target.value, id: '' });
  };

  const handleIdFilterChange = (event) => {
    setFilter({ ...filter, id: event.target.value, cafe: '' });
  };

  return (
    <div>
      <Button onClick={() => navigate('/add-employee')} variant="contained" color="primary">
        Add New Employee
      </Button>

      <TextField
        label="Filter by Employee ID"
        value={filter.id}
        onChange={handleIdFilterChange}
        variant="outlined"
        fullWidth
        margin="normal"
      />
      <TextField
        label="Filter by Cafe"
        value={filter.cafe}
        onChange={handleCafeFilterChange}
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
            onDelete={() => handleDelete(employee.id)}
          />
        ))}
      </div>
    </div>
  );
};

export default Employees;