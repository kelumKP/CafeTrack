import React, { useEffect, useState } from 'react';
import { getEmployees, deleteEmployee } from '../services/api';
import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import EmployeeCard from '../components/EmployeeCard';

const Employees = () => {
  const [employees, setEmployees] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetchEmployees();
  }, []);

  const fetchEmployees = async () => {
    const data = await getEmployees();
    setEmployees(data);
  };

  const handleDelete = async (id) => {
    await deleteEmployee(id);
    fetchEmployees();
  };

  return (
    <div>
      <Button onClick={() => navigate('/add-employee')}>Add New Employee</Button>
      <div>
        {employees.map((employee) => (
          <EmployeeCard key={employee.id} employee={employee} onEdit={() => navigate(`/edit-employee/${employee.id}`)} onDelete={handleDelete} />
        ))}
      </div>
    </div>
  );
};

export default Employees;
