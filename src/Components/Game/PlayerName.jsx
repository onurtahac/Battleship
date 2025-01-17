import React, { useState } from 'react';
import './PlayerName.css';
import { useNavigate } from 'react-router-dom';

export default function PlayerNames({ onStartGame }) {
  const [player1Name, setPlayer1Name] = useState('');
  const [player2Name, setPlayer2Name] = useState('');
  const navigate = useNavigate(); // useNavigate kancasını kullanarak `navigate` fonksiyonunu tanımlıyoruz

  const handleStartGame = () => {
    if (player1Name && player2Name) {
      if (onStartGame) {
        onStartGame(player1Name, player2Name); // Opsiyonel `onStartGame` çağrısı
      }
      navigate('/Player1Selection'); // Yönlendirme işlemi
    } else {
      alert('Please fill in the blanks.');
    }
  };

  const handleBack = () => {
    navigate('/'); // Ana sayfaya yönlendirme
  };

  return (
    <div className="PlayerNames">
      <img className='tankimageplayerselection' src="Public/Photos/Soliders.jpeg" alt="" />
      <h1 className='PlayerNameh1'>Please Enter the Player Names</h1>
      <h2 className="playernameh2">Player 1 Name:</h2>
      <input
        className="Player1input"
        type="text"
        value={player1Name}
        onChange={(e) => setPlayer1Name(e.target.value)}
        placeholder="Player 1"
      />
      <h2 className="playernameh2">Player 2 Name:</h2>
      <input
        className="Player2input"
        type="text"
        value={player2Name}
        onChange={(e) => setPlayer2Name(e.target.value)}
        placeholder="Player 2"
      />
      <button className="PlayerNamesButtonBack" onClick={handleBack}>
        Back
      </button>
      <button className="PlayerNamesButtonCont" onClick={handleStartGame}>
        Continue
      </button>
    </div>
  );
}
