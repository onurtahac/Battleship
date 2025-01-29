import React, { useState, useEffect } from 'react';
import './PlayerSelection.css';
import { useNavigate } from 'react-router-dom';

export default function Player1Selection({ onNext, playerName }) {
  const [timer, setTimer] = useState(90);
  const [selectedCells, setSelectedCells] = useState([]); // Seçilen karelerin tutulduğu dizi
  const [unitPlacements, setUnitPlacements] = useState({}); // Birliklerin yerleştirildiği hücreler
  const [currentUnit, setCurrentUnit] = useState(null); // Şu anki seçili birlik
  const [remainingCells, setRemainingCells] = useState(0); // Kalan hücre sayısı
  const [unitSelected, setUnitSelected] = useState({}); // Birliklerin seçilip seçilmediğini takip et
  const navigate = useNavigate();
  useEffect(() => {
    if (timer > 0) {
      const countdown = setInterval(() => setTimer(timer - 1), 1000);
      return () => clearInterval(countdown);
    }
  }, [timer]);

  // Birlik yerleştirme fonksiyonu
  const placeUnit = (unit, cells) => {
    // Birlik zaten yerleştirildiyse, aynı hücrelere yerleştirilemez
    if (cells.some(cell => selectedCells.includes(cell))) return;

    setUnitPlacements((prev) => {
      return { ...prev, [unit]: cells };
    });
    setSelectedCells([...selectedCells, ...cells]); // Seçilen hücrelere yeni hücreler ekle
  };

  // Birlik seçme
  const selectUnit = (unit, size) => {
    if (unitSelected[unit]) return; // Eğer bu birlik daha önce seçildiyse, işlem yapılmasın

    setUnitSelected((prev) => ({
      ...prev,
      [unit]: true, // Bu birliği seçildi olarak işaretle
    }));

    setCurrentUnit(unit); // Seçili birliği güncelle
    setRemainingCells(size); // Seçilecek hücre sayısını güncelle
  };

  const handleCellClick = (cell) => {
    if (!currentUnit || remainingCells <= 0) return; // Eğer bir birlik seçilmemişse veya yerleştirilecek hücre kalmamışsa işlem yapma

    // Seçilen hücreyi ekle
    setSelectedCells((prevSelected) => {
      const newSelected = [...prevSelected, cell];
      return newSelected;
    });

    // Birliği yerleştir
    setRemainingCells(remainingCells - 1); // Kalan hücreyi azalt

    // Birlik tüm hücreler yerleştirildiyse, onu kaydet
    if (remainingCells === 1) {
      placeUnit(currentUnit, [...selectedCells, cell]);
      setCurrentUnit(null); // Seçili birliği sıfırla
    }
  };

  const handleContinue = () => {

      navigate('/TwoPlayerGame'); // Yönlendirme işlemi

  };

  const renderGrid = () => {
    const grid = [];
    for (let row = 1; row <= 10; row++) {
      for (let col = 1; col <= 10; col++) {
        const cell = `${String.fromCharCode(64 + col)}${row}`; // Hücre adı örn: A1, B2
        const isSelected = selectedCells.includes(cell);
        const isUnitCell = (unit) => unitPlacements[unit]?.includes(cell);

        grid.push(
          <div
            key={cell}
            className={`cell ${isSelected ? 'selected' : ''} 
                        ${isUnitCell('Yellow') ? 'yellow-unit' : ''} 
                        ${isUnitCell('Green') ? 'green-unit' : ''} 
                        ${isUnitCell('Blue') ? 'blue-unit' : ''} 
                        ${isUnitCell('Red') ? 'red-unit' : ''}`}
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
    <div className="PlayerSelection">
            <img className='tankimageplayerselection' src="Public/Photos/Tanks2.jpeg" alt="" />

      <div className="left-panel">
        <h2 className='playernameh2'>{playerName} İçin Seçim Ekranı</h2>
        <div className="unit-list">
          <div onClick={() => selectUnit('Yellow', 3)} className="unit">Sarı Birlik (3)</div>
          <div onClick={() => selectUnit('Green', 3)} className="unit">Yeşil Birlik (3)</div>
          <div onClick={() => selectUnit('Blue', 4)} className="unit">Mavi Birlik (4)</div>
          <div onClick={() => selectUnit('Red', 5)} className="unit">Kırmızı Birlik (5)</div>
        </div>
      </div>

      <div className="right-panel">
        <div className="game-grid">{renderGrid()}</div>
        <button className='PlayerSelectionButton' onClick={handleContinue} >
          Devam Et
        </button>
      </div>
    </div>
  );
}
