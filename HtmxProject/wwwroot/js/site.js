var LayoutUtils = function () {
    var toggleMode = function () {     
        if (!('theme' in localStorage) || localStorage.theme === 'dark') {
            localStorage.theme = 'light';
            document.querySelector('html').classList.remove('dark');
        }
        else {
            localStorage.theme = 'dark';
            document.querySelector('html').classList.add('dark');
        }
    };

    return {
        toggleMode: toggleMode
    }
}();
