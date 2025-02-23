import axios from 'axios';

const API_BASE_URL = 'https://localhost:7199'; // Replace with your actual API URL

// Create a reusable axios instance
const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Helper function to handle API errors
const handleApiError = (error, context) => {
  console.error(`Error ${context}:`, error.response?.data || error.message);
  throw new Error(error.response?.data?.message || `Failed to ${context}.`);
};

// Helper function to create form data for cafe operations
const createCafeFormData = (cafeData) => {
  const formData = new FormData();
  formData.append('id', cafeData.id);
  formData.append('name', cafeData.name);
  formData.append('description', cafeData.description);
  formData.append('location', cafeData.location);
  if (cafeData.logo instanceof File) {
    formData.append('logo', cafeData.logo); // Append the logo file
  }
  return formData;
};

// Helper function to create employee payload
const createEmployeePayload = (employeeData) => ({
  Employee: {
    Id: employeeData.Id || "",
    Name: employeeData.Name,
    EmailAddress: employeeData.EmailAddress,
    PhoneNumber: employeeData.PhoneNumber,
    Gender: employeeData.Gender,
    DaysWorked: employeeData.DaysWorked || 0,
    CafeId: employeeData.CafeId || null,
  },
});

// Cafe-related functions
export const getCafes = async (locationFilter = '') => {
  try {
    const response = await apiClient.get('/api/Cafe', {
      params: { location: locationFilter },
    });
    return response.data;
  } catch (error) {
    return handleApiError(error, 'fetching cafes');
  }
};

export const getCafeById = async (id) => {
  try {
    const response = await apiClient.get(`/api/Cafe/${id}`);
    return response.data;
  } catch (error) {
    if (error.response?.status === 404) {
      console.warn('Cafe not found:', error.message);
      return null;
    }
    return handleApiError(error, 'fetching cafe');
  }
};

export const addCafe = async (cafeData) => {
  try {
    const formData = createCafeFormData(cafeData);
    const response = await apiClient.post('/api/Cafe/createCafe', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  } catch (error) {
    return handleApiError(error, 'creating cafe');
  }
};

export const updateCafe = async (cafeData) => {
  try {
    const formData = createCafeFormData(cafeData);
    const response = await apiClient.put('/api/Cafe/updateCafe', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  } catch (error) {
    return handleApiError(error, 'updating cafe');
  }
};

export const deleteCafe = async (id) => {
  try {
    await apiClient.delete(`/api/Cafe/cafe/${id}`);
  } catch (error) {
    return handleApiError(error, 'deleting cafe');
  }
};

// Employee-related functions
export const getEmployeesByCafe = async (cafe) => {
  try {
    const response = await apiClient.get('/api/Employees', {
      params: { cafe },
    });
    return response.data;
  } catch (error) {
    return handleApiError(error, 'fetching employees by cafe');
  }
};

export const getEmployeeById = async (id) => {
  try {
    const response = await apiClient.get(`/api/Employees/${id}`);
    return response.data;
  } catch (error) {
    if (error.response?.status === 404) {
      console.warn('Employee not found:', error.message);
      return null;
    }
    return handleApiError(error, 'fetching employee');
  }
};

export const addEmployee = async (employeeData) => {
  try {
    const payload = createEmployeePayload(employeeData);
    const response = await apiClient.post('/api/Employees/createEmployee', payload);
    return response.data;
  } catch (error) {
    return handleApiError(error, 'creating employee');
  }
};

export const updateEmployee = async (employeeData) => {
  try {
    const payload = createEmployeePayload(employeeData);
    const response = await apiClient.put('/api/Employees/updateEmployee', payload);
    return response.data;
  } catch (error) {
    return handleApiError(error, 'updating employee');
  }
};

export const deleteEmployee = async (id) => {
  try {
    await apiClient.delete(`/api/Employees/employee/${id}`);
  } catch (error) {
    return handleApiError(error, 'deleting employee');
  }
};