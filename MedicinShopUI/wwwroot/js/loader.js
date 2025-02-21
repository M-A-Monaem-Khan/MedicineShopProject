window.loader = {
    showLoader: function () {
        const selector = document.querySelector('.loader');
        selector.classList.add('d-flex');
    },

    hideLoader: function () {
        const selector = document.querySelector('.loader');
        selector.classList.remove('d-flex');
    }
}
