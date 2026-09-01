import react from '@vitejs/plugin-react'
import { defineConfig } from 'vite'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      // Porten kommer fra backend/Properties/launchSettings.json -> "http" profilen
      '/api': {
        target: 'http://localhost:5187',
        changeOrigin: true,
      },
      '/images': {
        target: 'http://localhost:5187',
        changeOrigin: true,
      },
    },
  },
})