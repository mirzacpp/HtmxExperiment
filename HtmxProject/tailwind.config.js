module.exports = {
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
    ]
}
