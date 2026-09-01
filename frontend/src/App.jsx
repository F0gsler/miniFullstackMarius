import { useState, useEffect } from 'react'
import { BrowserRouter, Routes, Route, useNavigate } from 'react-router-dom'
import SizeValg from './SizeValg'
import dog1 from './assets/1.jpg'
import dog2 from './assets/2.jpg'
import dog3 from './assets/3.jpg'
import './App.css'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Galleri />} />
        <Route path="/downloaded" element={<Downloaded />} />
      </Routes>
    </BrowserRouter>
  )
}

function Galleri() {
  const [visDog1, setVisdog1] = useState(false)
  const [visDog2, setVisdog2] = useState(false)
  const [visDog3, setVisdog3] = useState(false)

  const [visSizeValg, setVisSizeValg] = useState(false)
  const [size, setSize] = useState("mellem")
  const [ingenBilleder, setIngenBilleder] = useState(false)

  const navigate = useNavigate()

  // Koerer naar siden loades
  useEffect(() => {
    if (!dog1 && !dog2 && !dog3) {
      setIngenBilleder(true)
    }
  }, [])

  // Metode med parameter
  function download(billede) {
    const link = document.createElement("a")
    link.href = billede
    link.download = "hund3.jpg"
    link.click()
    navigate("/downloaded")
  }

  // Callback fra SizeValg
  function modtagSize(valgt) {
    setSize(valgt)
    setVisSizeValg(false)
    setVisdog2(true)
  }

  return (
    <>
      {ingenBilleder && (
        <p>Ingen billeder fundet</p>
      )}

      {!visDog1 && !visDog2 && !visSizeValg && (
        <div style={{ justifyContent: "center", display: "flex", gap: "16px" }}>
          <div>
            <button
              onClick={() => setVisdog1(true)}
              style={{ padding: 0, border: "none", background: "none", cursor: "pointer" }}
            >
              <img
                src={dog1}
                alt=""
                style={{ height: "250px", width: "250px", objectFit: "cover", display: "block" }}
              />
            </button>
          </div>
          <div>
            <button
              onClick={() => setVisSizeValg(true)}
              style={{ padding: 0, border: "none", background: "none", cursor: "pointer" }}
            >
              <img
                src={dog2}
                alt=""
                style={{ height: "250px", width: "250px", objectFit: "cover", display: "block" }}
              />
            </button>
          </div>
          <div>
            <button
              onClick={() => download(dog3)}
              style={{ padding: 0, border: "none", background: "none", cursor: "pointer" }}
            >
              <img
                src={dog3}
                alt=""
                style={{ height: "250px", width: "250px", objectFit: "cover", display: "block" }}
              />
            </button>
          </div>
        </div>
      )}

      {visSizeValg && (
        <SizeValg onVaelg={modtagSize} />
      )}

      {visDog1 && (
        <div style={{
          position: "fixed",
          top: 0,
          left: 0,
          width: "100vw",
          height: "100vh",
          background: "black",
          display: "flex",
          flexDirection: "column",
          alignItems: "center"
        }}>
          <img
            src={dog1}
            alt=""
            style={{ flex: 1, minHeight: 0, width: "100%", objectFit: "contain" }}
          />
          <button
            onClick={() => setVisdog1(false)}
            style={{ margin: "16px", padding: "12px 32px", fontSize: "18px" }}
          >
            Tilbage
          </button>
        </div>
      )}

      {visDog2 && size === "lille" && (
        <div>
          <img src={dog2} alt="" style={{ width: "200px", height: "200px", objectFit: "cover" }} />
          <br />
          <button onClick={() => setVisdog2(false)}>Tilbage</button>
        </div>
      )}

      {visDog2 && size === "mellem" && (
        <div>
          <img src={dog2} alt="" style={{ width: "400px", height: "400px", objectFit: "cover" }} />
          <br />
          <button onClick={() => setVisdog2(false)}>Tilbage</button>
        </div>
      )}

      {visDog2 && size === "stor" && (
        <div>
          <img src={dog2} alt="" style={{ width: "700px", height: "700px", objectFit: "cover" }} />
          <br />
          <button onClick={() => setVisdog2(false)}>Tilbage</button>
        </div>
      )}
    </>
  )
}

function Downloaded() {
  const navigate = useNavigate()

  return (
    <div>
      <h2>Download gennemfoert</h2>
      <button onClick={() => navigate("/")}>Tilbage</button>
    </div>
  )
}

export default App