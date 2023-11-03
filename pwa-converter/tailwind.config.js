
/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ['./Views/**/*.cshtml', './wwwroot/js/*.js', './Models/LighthouseAuditResultContainer.cs'],
    theme: {
        colors: {
            'white': '#FFFFFF',
            'black': '#000000',
            'primary': {
                DEFAULT: '#185C90',
                hover: '#12466d'
            },
            'grey': '#575757',
            'red': '#E51F1F',
            'green': '#068938',
            'yellow': '#EDD94C',
            'transparent': 'transparent'
        },
        fontFamily: {
            "roboto": ['Roboto', "ui-sans-serif", "system-ui", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "Roboto", "Helvetica Neue", "Arial", "Noto Sans", "sans-serif", "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"],
            "archivo": ['Archivo', "ui-sans-serif", "system-ui", "-apple-system", "BlinkMacSystemFont", "Segoe UI", "Roboto", "Helvetica Neue", "Arial", "Noto Sans", "sans-serif", "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"]
        },
        screens: {
            'xs': '375px',
            'mobilel': '425px',
            'sm': '640px',
            'md': '768px',
            'lg': '1024px',
            'xl': '1280px',
            '2xl': '1580px',
            '3xl': '1910px'
        },
    extend: {},
  },
  plugins: [],
}

