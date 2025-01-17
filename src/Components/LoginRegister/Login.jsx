import React from "react";
import "./LoginRegister.css";

export default function Login() {
  return (
    <div>
      <video autoPlay loop muted className="background-video">
        <source src="./Public/Photos/tank.mp4" />
      </video>

      <form>
        <h3>Login</h3>
    
        <label for="username">Username</label>
        <input type="text" placeholder="Email or Phone" id="username" />

        <label for="password">Password</label>
        <input type="password" placeholder="Password" id="password" />

        <button>Log In</button>
        
        <div class="social">
          <div class="go">
            <i class="fab fa-google"></i> Google
          </div>
          <div class="fb">
            <i class="fab fa-facebook"></i> Facebook
          </div>
        </div>
        <button>Register</button>

      </form>
    </div>
  );
}
