/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './**/*.{razor,html}', // Scan Razor and HTML files in Shared
        "./wwwroot/**/*.{html,js}",
        "../Pixsale.Web/**/*.{razor,cshtml}",
        "../Pixsale.App/**/*.{razor,cshtml}",
    ],
  theme: {
      extend: {
          colors: {
              tata: "#1C2D8C" // Tata Blue
          }
      },
  },
  plugins: [],
}

