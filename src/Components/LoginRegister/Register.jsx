import React from 'react';
import './LoginRegister.css';

export default function Register() {
  return (
    <div className="login-register-container">
      {/* Background Video */}
      <div className="video-container">
        <video src="" autoPlay muted loop className="background-video"></video>
      </div>
      <div className="form-container register-form">
        <h2>Register</h2>
        <input type="text" className="input-field" placeholder="Name" />
        <input type="text" className="input-field" placeholder="Surname" />
        <input type="email" className="input-field" placeholder="Email" />
        <input type="password" className="input-field" placeholder="Password" />
        <button className="form-button register-button">Register</button>
      </div>
    </div>
  );
}
