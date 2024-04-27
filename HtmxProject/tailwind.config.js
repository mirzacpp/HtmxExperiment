module.exports = {
    darkMode: 'class',
    theme: {
        extend: {
            colors: {
                primary: '#60A5FA'
            }
        },
    },
    content: ["./Pages/**/*.{cshtml,js,css}"],
    plugins: [
        require('@tailwindcss/forms'),
        require('tailwind-scrollbar')({ preferredStrategy: 'pseudoelements' }),
    ]
}
