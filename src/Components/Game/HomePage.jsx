import React from 'react'
import './HomePage.css';

export default function HomePage() {
  return (
    <div className='HomePage'>
        <img className='tankimage' src="Public/Photos/tank.jpeg" alt="" />
       <a href='Login' className='LoginButton'>Login</a>
       <a href='#' className='SoloButton'>Solo Game</a>
       <a href='PlayerName' className='TwoPlayerButton'>Two Players Game</a>
       <a href='#' className='SettingsButton'>Settings</a>
    </div>
  )
}
