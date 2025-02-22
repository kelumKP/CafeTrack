import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button, TextField, Typography, Box, Snackbar } from '@mui/material';
import { addCafe, updateCafe, getCafeById } from '../services/api';
import { useFormik } from 'formik';
import * as Yup from 'yup';

const AddEditCafe = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [logo, setLogo] = useState(null);

  const formik = useFormik({
    initialValues: {
      name: '',
      description: '',
      location: '',
    },
    validationSchema: Yup.object({
      name: Yup.string()
        .min(6, 'Name must be at least 6 characters')
        .max(50, 'Name must be at most 50 characters')
        .required('Required'),
      description: Yup.string()
        .max(256, 'Description must be at most 256 characters')
        .required('Required'),
      location: Yup.string().required('Required'),
    }),
    onSubmit: async (values) => {
      try {
        const cafeData = {
          ...values,
          logo: logo, // Include the logo file
        };

        if (id) {
          await updateCafe(id, cafeData);
        } else {
          await addCafe(cafeData);
        }
        navigate('/cafes');
      } catch (error) {
        console.error('Error saving cafe:', error);
      }
    },
  });

  const handleFileChange = (event) => {
    setLogo(event.target.files[0]); // Set the selected file
  };

  return (
    <Box sx={{ padding: 3 }}>
      <Typography variant="h4">{id ? 'Edit Cafe' : 'Add Cafe'}</Typography>
      <form onSubmit={formik.handleSubmit}>
        <TextField
          label="Name"
          name="name"
          value={formik.values.name}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.name && Boolean(formik.errors.name)}
          helperText={formik.touched.name && formik.errors.name}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Description"
          name="description"
          value={formik.values.description}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.description && Boolean(formik.errors.description)}
          helperText={formik.touched.description && formik.errors.description}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Location"
          name="location"
          value={formik.values.location}
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          error={formik.touched.location && Boolean(formik.errors.location)}
          helperText={formik.touched.location && formik.errors.location}
          fullWidth
          margin="normal"
        />
        <input
          type="file"
          name="logo"
          onChange={handleFileChange}
        />
        <Box sx={{ marginTop: 2 }}>
          <Button type="submit" variant="contained" color="primary">
            {id ? 'Update Cafe' : 'Add Cafe'}
          </Button>
          <Button variant="contained" color="secondary" onClick={() => navigate('/cafes')} sx={{ marginLeft: 2 }}>
            Cancel
          </Button>
        </Box>
      </form>
    </Box>
  );
};

export default AddEditCafe;