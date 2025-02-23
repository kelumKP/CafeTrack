import axios from 'axios';

const API_BASE_URL = 'https://localhost:7199'; // Replace with your actual API URL

// Fetch all cafes with optional location filtering
export const getCafes = async (locationFilter = '') => {
  try {
    const response = await axios.get(`${API_BASE_URL}/api/Cafe`, {
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
    const response = await axios.get(`${API_BASE_URL}/api/Cafe/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching cafe:', error);
    return null;
  }
};

// Create a new cafe
export const addCafe = async (cafeData) => {
  try {
    const formData = new FormData();
    formData.append('id', cafeData.id);
    formData.append('name', cafeData.name);
    formData.append('description', cafeData.description);
    formData.append('location', cafeData.location);
    if (cafeData.logo instanceof File) {
      formData.append('logo', cafeData.logo); // Append the logo file
    }
    const response = await axios.post(`${API_BASE_URL}/api/Cafe/createCafe`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data', // Set the correct Content-Type
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error creating cafe:', error);
    throw new Error(error.response?.data?.message || 'Failed to create cafe.');
  }
};

// Update an existing cafe
export const updateCafe = async (cafeData) => {
  try {
    const formData = new FormData();
    formData.append('id', cafeData.id);
    formData.append('name', cafeData.name);
    formData.append('description', cafeData.description);
    formData.append('location', cafeData.location);
    if (cafeData.logo instanceof File) {
      formData.append('logo', cafeData.logo); // Append the logo file
    }
    const response = await axios.put(`${API_BASE_URL}/api/Cafe/updateCafe`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data', // Set the correct Content-Type
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error updating cafe:', error);
    throw error;
  }
};

// Delete a cafe
export const deleteCafe = async (id) => {
  try {
    await axios.delete(`${API_BASE_URL}/api/Cafe/cafe/${id}`);
  } catch (error) {
    console.error('Error deleting cafe:', error);
    throw error;
  }
};

// Fetch employees by Cafe
export const getEmployeesByCafe = async (cafe) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/api/Employees`, {
      params: { cafe }, // Passing the cafe string as a query parameter
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
    if (error.response?.status === 404) {
      console.warn('Employee not found:', error.message);
      return null; // Return null if employee is not found
    } else {
      console.error('Error fetching employee:', error);
      throw error; // Re-throw the error for other cases
    }
  }
};

// Create a new employee
export const addEmployee = async (employeeData) => {
  try {
    const payload = {
      Employee: {
        Id: employeeData.Id || "", 
        Name: employeeData.Name,
        EmailAddress: employeeData.EmailAddress,
        PhoneNumber: employeeData.PhoneNumber,
        Gender: employeeData.Gender,
        DaysWorked: employeeData.DaysWorked || 0,
        CafeId: employeeData.CafeId || null,
      },
    };
    const response = await axios.post(`${API_BASE_URL}/api/Employees/createEmployee`, payload, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error creating employee:', error.response?.data || error.message);
    throw new Error(error.response?.data?.message || 'Failed to create employee.');
  }
};

// Update an existing employee
export const updateEmployee = async (employeeData) => {
  try {
    const payload = {
      Employee: {
        Id: employeeData.Id || "", 
        Name: employeeData.Name,
        EmailAddress: employeeData.EmailAddress,
        PhoneNumber: employeeData.PhoneNumber,
        Gender: employeeData.Gender,
        DaysWorked: employeeData.DaysWorked || 0,
        CafeId: employeeData.CafeId || null,
      },
    };
    const response = await axios.put(`${API_BASE_URL}/api/Employees/updateEmployee`, payload, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error updating employee:', error.response?.data || error.message);
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