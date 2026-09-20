import { useState, useEffect } from 'react'
import { BrowserRouter, Routes, Route, useNavigate } from 'react-router-dom'
import paymentPage from './Page/PaymentPage'
import homePage from './Page/homePage'
import './App.css'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<homePage />} />
        <Route path="/payment" element={<paymentPage />} />
      </Routes>
    </BrowserRouter>
  )
}


export default App