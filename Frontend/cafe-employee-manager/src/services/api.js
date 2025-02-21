import axios from 'axios';

const API_BASE_URL = 'https://localhost:7199'; // Replace with your actual API URL

// Fetch all cafes with optional location filtering
export const getCafes = async (locationFilter = '') => {
  try {
    const response = await axios.get(`${API_BASE_URL}/api/Cafe`, {  // Changed here to match backend route
      params: { location: locationFilter },
    });
    return response.data;
  } catch (error) {
    console.error('Error fetching cafes:', error);
    return [];
  }
};

// Fetch a single cafe by ID
export const getCafeById = async (id) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/api/Cafe/${id}`);  // Changed here to match backend route
    return response.data;
  } catch (error) {
    console.error('Error fetching cafe:', error);
    return null;
  }
};

// Create a new cafe
export const addCafe = async (cafeData) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/api/Cafe`, cafeData);  // Changed here to match backend route
    return response.data;
  } catch (error) {
    console.error('Error creating cafe:', error);
    throw error;
  }
};

// Update an existing cafe
export const updateCafe = async (id, cafeData) => {
  try {
    const response = await axios.put(`${API_BASE_URL}/api/Cafe/${id}`, cafeData);  // Changed here to match backend route
    return response.data;
  } catch (error) {
    console.error('Error updating cafe:', error);
    throw error;
  }
};

// Delete a cafe
export const deleteCafe = async (id) => {
  try {
    await axios.delete(`${API_BASE_URL}/api/Cafe/${id}`);  // Changed here to match backend route
  } catch (error) {
    console.error('Error deleting cafe:', error);
    throw error;
  }
};

// Fetch employees by Cafe
export const getEmployeesByCafe = async (cafe) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/api/Employees`, {
      params: { cafe },  // Passing the cafe string as a query parameter
    });
    return response.data;
  } catch (error) {
    console.error('Error fetching employees by cafe:', error);
    return [];
  }
};

// Fetch a single employee by ID
export const getEmployeeById = async (id) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/api/Employees/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching employee:', error);
    return null;
  }
};

// Create a new employee
export const addEmployee = async (employeeData) => {
  try {
    const response = await axios.post(`${API_BASE_URL}/api/Employees`, employeeData);
    return response.data;
  } catch (error) {
    console.error('Error creating employee:', error);
    throw error;
  }
};

// Update an existing employee
export const updateEmployee = async (id, employeeData) => {
  try {
    const response = await axios.put(`${API_BASE_URL}/api/Employees/employee`, employeeData);
    return response.data;
  } catch (error) {
    console.error('Error updating employee:', error);
    throw error;
  }
};

// Delete an employee
export const deleteEmployee = async (id) => {
  try {
    await axios.delete(`${API_BASE_URL}/api/Employees/employee/${id}`);
  } catch (error) {
    console.error('Error deleting employee:', error);
    throw error;
  }
};
