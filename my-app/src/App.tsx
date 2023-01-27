import React, { useEffect } from 'react';
import logo from './logo.svg';
import './App.css';
// import { jwt_decode } from "jwt-decode";

const App = () => {

  const handleGoogleLogin=(resp: any) =>{
    console.log("Google login resp", resp);
    console.log("Token Id", resp.credential);  
    
  }

  useEffect(() => {
    //global google
    window.google.accounts!.id.initialize({
      client_id: "977621133056-f3vvvb7evmme0348afesskmcf37h2srv.apps.googleusercontent.com",
      callback: handleGoogleLogin
    });

    window.google.accounts!.id.renderButton(document.getElementById("signInDiv"),{
      theme: "outline", size: "Large"
    });

  }, []);
  

  return (
    <>
      <div id="signInDiv"></div>
    </>
  );
}

export default App;
function jwt_decode(credential: any) {
  throw new Error('Function not implemented.');
}

