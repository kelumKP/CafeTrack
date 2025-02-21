import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Cafes from './pages/Cafes';
import Employees from './pages/Employees';
import AddEditCafe from './pages/AddEditCafe';
import AddEditEmployee from './pages/AddEditEmployee';
import Header from './components/Header';
import Sidebar from './components/Sidebar';
import Footer from './components/Footer';

const App = () => (
  <Router>
    <div className="app-container">
      <Header />
      <Sidebar />
      <main>
        <Routes>
          <Route path="/cafes" element={<Cafes />} />
          <Route path="/employees" element={<Employees />} />
          <Route path="/add-cafe" element={<AddEditCafe />} />
          <Route path="/edit-cafe/:id" element={<AddEditCafe />} />
          <Route path="/add-employee" element={<AddEditEmployee />} />
          <Route path="/edit-employee/:id" element={<AddEditEmployee />} />
        </Routes>
      </main>
      <Footer />
    </div>
  </Router>
);

export default App;
