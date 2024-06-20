module.exports = {
    darkMode: 'class',
    theme: {
        extend: {
            colors: {
                primary: '#60A5FA'
            }
        },
    },
    content: ["./Pages/**/*.{cshtml,js,css}", "./Infrastructure/TagHelpers/**/*.cs"],
    plugins: [
        require('@tailwindcss/forms'),
        require('tailwind-scrollbar')({ preferredStrategy: 'pseudoelements' }),
    ]
}
