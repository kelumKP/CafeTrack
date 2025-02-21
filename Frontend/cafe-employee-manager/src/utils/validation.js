// Utility functions for form validation

// Validate if a string is not empty
export const validateRequired = (value) => {
    return value && value.trim() !== '';
  };
  
  // Validate if a string is a valid email address
  export const validateEmail = (value) => {
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailPattern.test(value);
  };
  
  // Validate if a string is a valid phone number (example: (123) 456-7890)
  export const validatePhoneNumber = (value) => {
    const phonePattern = /^\(\d{3}\) \d{3}-\d{4}$/;
    return phonePattern.test(value);
  };
  
  // Validate if a string is a valid URL
  export const validateUrl = (value) => {
    const urlPattern = /^(https?|chrome):\/\/[^\s$.?#].[^\s]*$/;
    return urlPattern.test(value);
  };
  
  // Validate if a string has a minimum length
  export const validateMinLength = (value, minLength) => {
    return value.length >= minLength;
  };
  