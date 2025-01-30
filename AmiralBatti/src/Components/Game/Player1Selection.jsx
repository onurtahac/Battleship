import React, { useState, useEffect } from "react";
import "./PlayerSelection.css";
import { useNavigate } from "react-router-dom";

export default function Player1Selection({ onNext, playerName }) {
  const [timer, setTimer] = useState(90);
  const [selectedCells, setSelectedCells] = useState([]);
  const [unitPlacements, setUnitPlacements] = useState({});
  const [currentUnit, setCurrentUnit] = useState(null);
  const [remainingCells, setRemainingCells] = useState(0);
  const [unitSelected, setUnitSelected] = useState({});
  const [gameId, setGameId] = useState(null); // Oyun ID'sini tutmak için state
  const [userId, setUserId] = useState(1); // Kullanıcı ID'sini tutmak için state (örnek değer)
  const navigate = useNavigate();

  useEffect(() => {
    if (timer > 0) {
      const countdown = setInterval(() => setTimer(timer - 1), 1000);
      return () => clearInterval(countdown);
    }
  }, [timer]);

  // API'ye hamle gönderme fonksiyonu
  const sendMoveToAPI = async (row, col) => {
    try {
      const response = await fetch("https://localhost:7200/api/Game/move", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          gameId: gameId, // Oyun ID'si
          userId: userId, // Kullanıcı ID'si
          coordinateId: 0, // Koordinat ID'si (API'ye göre gerekliyse)
          shipId: 0, // Gemi ID'si (API'ye göre gerekliyse)
          row: row, // Satır bilgisi
          col: col, // Sütun bilgisi
        }),
      });

      if (response.ok) {
        const data = await response.json();
        alert(`Move successful! Result: ${data.result}`); // API'den gelen sonucu göster
      } else {
        const errorData = await response.json();
        alert(`Move failed: ${errorData.message}`);
      }
    } catch (error) {
      console.error("Error sending move:", error);
      alert("An error occurred while sending the move.");
    }
  };

  // Hücre tıklama işlemi
  const handleCellClick = (cell) => {
    if (!currentUnit || remainingCells <= 0) return;

    // Hücrenin satır ve sütun bilgilerini çıkar
    const row = parseInt(cell.substring(1)); // Örneğin, "A1" -> 1
    const col = cell.charCodeAt(0) - 64; // Örneğin, "A" -> 1

    // API'ye hamle gönder
    sendMoveToAPI(row, col);

    setSelectedCells((prevSelected) => {
      const newSelected = [...prevSelected, cell];
      return newSelected;
    });

    setRemainingCells(remainingCells - 1);

    if (remainingCells === 1) {
      placeUnit(currentUnit, [...selectedCells, cell]);
      setCurrentUnit(null);
    }
  };

  // Diğer fonksiyonlar ve render işlemleri...
  const placeUnit = (unit, cells) => {
    if (cells.some((cell) => selectedCells.includes(cell))) return;
  
    // Sıralama yaparak başlangıç hücresini belirleyelim
    const sortedCells = cells.sort((a, b) => {
      const rowA = parseInt(a.substring(1));
      const rowB = parseInt(b.substring(1));
      const colA = a.charCodeAt(0);
      const colB = b.charCodeAt(0);
  
      if (rowA === rowB) {
        return colA - colB; // Aynı satırdaysa sütuna göre sırala
      }
      return rowA - rowB; // Farklı satırdaysa satıra göre sırala
    });
  
    const startCell = sortedCells[0]; // İlk hücre başlangıç noktasıdır
    const secondCell = sortedCells[1]; // İkinci hücre yönü belirlemek için kullanılacak
  
    const isVertical = secondCell ? secondCell[0] === startCell[0] : false; // Eğer ikinci hücre aynı sütundaysa dikeydir
  
    setUnitPlacements((prev) => ({
      ...prev,
      [unit]: { length: cells.length, coordinates: [startCell], isVertical },
    }));
  
    setSelectedCells([...selectedCells, ...cells]);
  };
  

  const selectUnit = (unit, size) => {
    if (unitSelected[unit]) return;

    setUnitSelected((prev) => ({
      ...prev,
      [unit]: true,
    }));

    setCurrentUnit(unit);
    setRemainingCells(size);
  };

  const handleContinue = async () => {
    const shipsData = Object.values(unitPlacements); // JSON formatında gemileri al
  
    const payload = {
      gameId: gameId, // Oyun ID'si
      userId: userId, // Kullanıcı ID'si
      ships: shipsData, // Gemileri JSON formatına uygun gönder
    };
  
    try {
      const response = await fetch("https://localhost:7200/api/Game/setup", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });
  
      if (response.ok) {
        alert("Gemi konumları başarıyla kaydedildi!");
        navigate("/Player2Selection");
      } else {
        const errorData = await response.json();
        alert(`Hata oluştu: ${errorData.message}`);
      }
    } catch (error) {
      console.error("Error sending ship placements:", error);
      alert("Sunucuya bağlanırken hata oluştu.");
    }
  };
  

  const renderGrid = () => {
    const grid = [];
    for (let row = 1; row <= 10; row++) {
      for (let col = 1; col <= 10; col++) {
        const cell = `${String.fromCharCode(64 + col)}${row}`;
        const isSelected = selectedCells.includes(cell);
        const isUnitCell = (unit) => unitPlacements[unit]?.includes(cell);

        grid.push(
          <div
            key={cell}
            className={`cell ${isSelected ? "selected" : ""} 
                        ${isUnitCell("Yellow") ? "yellow-unit" : ""} 
                        ${isUnitCell("Green") ? "green-unit" : ""} 
                        ${isUnitCell("Blue") ? "blue-unit" : ""} 
                        ${isUnitCell("Red") ? "red-unit" : ""}`}
            onClick={() => handleCellClick(cell)}
          >
            {cell}
          </div>
        );
      }
    }
    return grid;
  };

  return (
    <div className="PlayerSelectionWrapper">
      <div className="PlayerSelection">
        <img
          className="tankimageplayerselection"
          src="Public/Photos/Tanks2.jpeg"
          alt=""
        />
        <div className="left-panel">
          <h2 className="playernameh2">{playerName} İçin Seçim Ekranı</h2>
          <div className="unit-list">
            <div onClick={() => selectUnit("Yellow", 3)} className="unit">
              Leopard 2A4(3)
            </div>
            <div onClick={() => selectUnit("Green", 3)} className="unit">
              T-90MS (3)
            </div>
            <div onClick={() => selectUnit("Blue", 4)} className="unit">
              Challenger 2 (4)
            </div>
            <div onClick={() => selectUnit("Red", 5)} className="unit">
              Leopard 2A4(5)
            </div>
          </div>
        </div>

        <div className="right-panel">
          <div className="game-grid">{renderGrid()}</div>
          <button className="PlayerSelectionButton" onClick={handleContinue}>
            Devam Et
          </button>
        </div>
      </div>
    </div>
  );
}