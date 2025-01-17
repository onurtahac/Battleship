import { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Homepage from './Components/Game/HomePage'; 
import Login from './Components/LoginRegister/Login';
import Register from './Components/LoginRegister/Register';
import TwoPlayerGame from './Components/Game/TwoPlayerGame';
import PlayerName from './Components/Game/PlayerName';
import Player1Selection from './Components/Game/Player1Selection';
import Player2Selection from './Components/Game/Player2Selection';
import './App.css';


function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Homepage />} />
        <Route path="/Login" element={<Login />} />
        <Route path="/Register" element={<Register />} />
        <Route path="/PlayerName" element={<PlayerName/>} />     
        <Route path="/Player1Selection" element={<Player1Selection />} />
        <Route path="/Player2Selection" element={<Player2Selection />} />
        <Route path="/TwoPlayerGame" element={<TwoPlayerGame />} />
      </Routes>
    </Router>
  );
}

export default App;
