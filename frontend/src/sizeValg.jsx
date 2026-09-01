import { useState } from 'react'

function SizeValg({ onVaelg }) {
  const [valgt, setValgt] = useState("mellem")

  return (
    <div>
      <h2>Vaelg stoerrelse</h2>

      <select value={valgt} onChange={(e) => setValgt(e.target.value)}>
        <option value="lille">Lille</option>
        <option value="mellem">Mellem</option>
        <option value="stor">Stor</option>
      </select>

      <p>Du har valgt: {valgt}</p>

      <button onClick={() => onVaelg(valgt)}>Vis billedet</button>
    </div>
  )
}

export default SizeValg