import React, { useState } from 'react';
import './HomePage.css';
import { useNavigate } from 'react-router-dom'; // Yönlendirme için

export default function HomePage() {
  const [gameId, setGameId] = useState(null);
  const navigate = useNavigate(); // Yönlendirme için

  const handleTwoPlayerGame = async () => {
    try {
      const response = await fetch('https://localhost:7200/api/Game/start', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          // API'ye gönderilecek gerekli veriler (örneğin, oyuncu ID'leri)
          player1Id: 1, // Örnek değer, dinamik olarak alınmalı
          player2Id: 2, // Örnek değer, dinamik olarak alınmalı
        }),
      });

      if (response.ok) {
        const data = await response.json();
        setGameId(data.gameId); // API'den dönen oyun ID'sini state'e kaydet
        alert(`Game started successfully! Game ID: ${data.gameId}`);
        navigate(`/game/${data.gameId}`); // Oyun sayfasına yönlendir
      } else {
        const errorData = await response.json();
        alert(`Failed to start game: ${errorData.message}`);
      }
    } catch (error) {
      console.error('Error starting game:', error);
      alert('An error occurred while starting the game.');
    }
  };

  return (
    <div className='HomePage'>
      <img className='tankimage' src="Public/Photos/tank.jpeg" alt="" />
      <a href='Login' className='LoginButton'>Login</a>
      <a href='#' className='SoloButton'>Solo Game</a>
      <button onClick={handleTwoPlayerGame} className='TwoPlayerButtonn'>
        Multiple Game
      </button>
      <a href='#' className='SettingsButton'>Settings</a>
    </div>
  );
}