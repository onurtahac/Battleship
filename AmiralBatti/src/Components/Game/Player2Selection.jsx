import React, { useState, useEffect } from "react";
import "./PlayerSelection.css";
import { useNavigate } from "react-router-dom";

export default function Player2Selection({ onNext, playerName }) {
  const [timer, setTimer] = useState(90);
  const [selectedCells, setSelectedCells] = useState([]);
  const [unitPlacements, setUnitPlacements] = useState({});
  const [currentUnit, setCurrentUnit] = useState(null);
  const [remainingCells, setRemainingCells] = useState(0);
  const [unitSelected, setUnitSelected] = useState({});
  const [gameId, setGameId] = useState(null);
  const [userId, setUserId] = useState(6); // Player 2 ID'si fix olarak 6
  const navigate = useNavigate();

  useEffect(() => {
    if (timer > 0) {
      const countdown = setInterval(() => setTimer(timer - 1), 1000);
      return () => clearInterval(countdown);
    }
  }, [timer]);

  const handleCellClick = (cell) => {
    if (!currentUnit || remainingCells <= 0) return;
    const row = parseInt(cell.substring(1));
    const col = cell.charCodeAt(0) - 64;
    setSelectedCells((prevSelected) => [...prevSelected, cell]);
    setRemainingCells(remainingCells - 1);

    if (remainingCells === 1) {
      placeUnit(currentUnit, [...selectedCells, cell]);
      setCurrentUnit(null);
    }
  };

  const placeUnit = (unit, cells) => {
    if (cells.some((cell) => selectedCells.includes(cell))) return;
    const sortedCells = cells.sort((a, b) => {
      const rowA = parseInt(a.substring(1));
      const rowB = parseInt(b.substring(1));
      const colA = a.charCodeAt(0);
      const colB = b.charCodeAt(0);
      return rowA === rowB ? colA - colB : rowA - rowB;
    });
    const startCell = sortedCells[0];
    const secondCell = sortedCells[1];
    const isVertical = secondCell ? secondCell[0] === startCell[0] : false;
    setUnitPlacements((prev) => ({
      ...prev,
      [unit]: { length: cells.length, coordinates: [startCell], isVertical },
    }));
    setSelectedCells([...selectedCells, ...cells]);
  };

  const selectUnit = (unit, size) => {
    if (unitSelected[unit]) return;
    setUnitSelected((prev) => ({ ...prev, [unit]: true }));
    setCurrentUnit(unit);
    setRemainingCells(size);
  };

  const handleContinue = async () => {
    const shipsData = Object.values(unitPlacements);
    const payload = { gameId, userId, ships: shipsData };
    try {
      const response = await fetch("https://localhost:7200/api/Game/setup", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });
      if (response.ok) {
        alert("Gemi konumları başarıyla kaydedildi!");
        navigate("/GameStart");
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
        grid.push(
          <div
            key={cell}
            className={`cell ${selectedCells.includes(cell) ? "selected" : ""}`}
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
        <img className="tankimageplayerselection" src="Public/Photos/Tanks2.jpeg" alt="" />
        <div className="left-panel">
          <h2 className="playernameh2">{playerName} İçin Seçim Ekranı</h2>
          <div className="unit-list">
            <div onClick={() => selectUnit("Yellow", 3)} className="unit">Leopard 2A4(3)</div>
            <div onClick={() => selectUnit("Green", 3)} className="unit">T-90MS (3)</div>
            <div onClick={() => selectUnit("Blue", 4)} className="unit">Challenger 2 (4)</div>
            <div onClick={() => selectUnit("Red", 5)} className="unit">Leopard 2A4(5)</div>
          </div>
        </div>
        <div className="right-panel">
          <div className="game-grid">{renderGrid()}</div>
          <button className="PlayerSelectionButton" onClick={handleContinue}>Devam Et</button>
        </div>
      </div>
    </div>
  );
}
