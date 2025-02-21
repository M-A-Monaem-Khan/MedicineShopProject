window.setFocus = (elementId) => {
    var element = document.getElementById(elementId);
    if (element) {
        element.focus();
    }
};