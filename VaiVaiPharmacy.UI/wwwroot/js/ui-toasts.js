/**
 * UI Toasts
 */

'use strict';

(function () {
    // Bootstrap toasts example
    // --------------------------------------------------------------------
    const toastPlacementContainer = document.querySelector('.toast-placement-ex'),
        toastPlacementBtn = document.querySelector('#showToastPlacement');
    let selectedType, selectedPlacement, toastPlacement;

    // Dispose toast when open another
    function toastDispose(toast) {
        if (toast && toast._element !== null) {
            if (toastPlacementContainer) {
                toastPlacementContainer.classList.remove(selectedType);
                DOMTokenList.prototype.remove.apply(toastPlacementContainer.classList, selectedPlacement);
            }
            toast.dispose();
        }
    }
    // Placement Button click
    if (toastPlacementBtn) {
        toastPlacementBtn.onclick = function () {
            if (toastPlacement) {
                toastDispose(toastPlacement);
            }
            selectedType = document.querySelector('#selectTypeOpt').value;
            selectedPlacement = document.querySelector('#selectPlacement').value.split(' ');

            toastPlacementContainer.classList.add(selectedType);
            DOMTokenList.prototype.add.apply(toastPlacementContainer.classList, selectedPlacement);
            toastPlacement = new bootstrap.Toast(toastPlacementContainer);
            toastPlacement.show();
        };
    }


})();
