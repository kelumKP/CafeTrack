// Utility functions for general helpers

// Format a date to a readable format (e.g., MM/DD/YYYY)
export const formatDate = (date) => {
    const d = new Date(date);
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    const year = d.getFullYear();
    return `${month}/${day}/${year}`;
  };
  
  // Capitalize the first letter of each word in a string
  export const capitalizeWords = (str) => {
    return str.replace(/\b\w/g, (char) => char.toUpperCase());
  };
  
  // Convert an object to a query string
  export const toQueryString = (params) => {
    return Object.keys(params)
      .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(params[key])}`)
      .join('&');
  };
  
  // Debounce function to limit the rate of execution
  export const debounce = (func, delay) => {
    let timeout;
    return function (...args) {
      clearTimeout(timeout);
      timeout = setTimeout(() => func(...args), delay);
    };
  };
  
  // Convert a string to lowercase
  export const toLowerCase = (str) => {
    return str.toLowerCase();
  };
  