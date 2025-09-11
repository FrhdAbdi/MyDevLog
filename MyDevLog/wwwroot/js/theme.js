export function applyTheme(theme) {
    document.documentElement.setAttribute('data-theme', theme);
}

export function getInitialTheme() {
    return localStorage.getItem('theme') || 'dark';
}

export function setThemeInStorage(theme) {
    localStorage.setItem('theme', theme);
}