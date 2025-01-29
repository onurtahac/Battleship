import React, { useState } from 'react';
import './LoginRegister.css';

export default function Register() {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    emailAddress: '',
    passwordHash: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Varsayılan role değerini ekle
    const payload = {
      ...formData,
      role: 'User', // Varsayılan role
      totalPoints: 0, // Varsayılan totalPoints
      createdAt: new Date().toISOString(), // Şu anki tarih ve saat
    };

    try {
      const response = await fetch('https://localhost:7200/api/Auth/register', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
      });

      if (response.ok) {
        alert('Registration successful!');
        // İsteğe bağlı: Kullanıcıyı başka bir sayfaya yönlendir
      } else {
        const errorData = await response.json();
        alert(`Registration failed: ${errorData.message}`);
      }
    } catch (error) {
      console.error('Error during registration:', error);
      alert('An error occurred during registration.');
    }
  };

  return (
    <div className="login-register-container">
      {/* Background Video */}
      <div className="video-container">
        <video src="" autoPlay muted loop className="background-video"></video>
      </div>
      <div className="form-container register-form">
        <h2>Register</h2>
        <form onSubmit={handleSubmit}>
          <input
            type="text"
            className="input-field"
            placeholder="Name"
            name="firstName"
            value={formData.firstName}
            onChange={handleChange}
            required
          />
          <input
            type="text"
            className="input-field"
            placeholder="Surname"
            name="lastName"
            value={formData.lastName}
            onChange={handleChange}
            required
          />
          <input
            type="email"
            className="input-field"
            placeholder="Email"
            name="emailAddress"
            value={formData.emailAddress}
            onChange={handleChange}
            required
          />
          <input
            type="password"
            className="input-field"
            placeholder="Password"
            name="passwordHash"
            value={formData.passwordHash}
            onChange={handleChange}
            required
          />
          <button type="submit" className="form-button register-button">
            Register
          </button>
        </form>
      </div>
    </div>
  );
}