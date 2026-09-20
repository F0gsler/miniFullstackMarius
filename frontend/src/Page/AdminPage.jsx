import { useState } from 'react'

function AdminPage() {
  const [besked, setBesked] = useState('')

  async function createAdminUser() {
    try {
      const res = await fetch('/api/Admin/CreateAdminUser', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            username: 'marius',
            password: 'hemmeligt123',
            adminLevel: 1,
        }),
      })

      if (!res.ok) throw new Error(`HTTP ${res.status}`)

      const data = await res.json()
      setBesked(`Oprettet: ${data.username}`)
    } catch (err) {
      setBesked(`Fejl: ${err.message}`)
    }
  }

  return (
    <div>
      <button onClick={createAdminUser}>Opret admin</button>
      <p>{besked}</p>
    </div>
  )
}

export default AdminPage