window.validateForm = {
    checkValidation: function (formId, elementId) {
        const form = document.getElementById(formId);
        const element = document.getElementById(elementId);
        if (form.checkValidity()) {
            //element.classList.remove('invalid');
            return true;
        } else {
           // form.reportValidity();
            form.classList.add('was-validated');
            element.classList.add('invalid');
            return false;
        }
    },

    //addClass: function (elementId) {
    //    const element = document.getElementById(elementId);
    //    element.classList.add('invalid');
    //}

};

