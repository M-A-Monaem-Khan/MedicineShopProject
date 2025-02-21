window.globalFunctions = {


    initialideDatepicker: function (_id, _dotNetHelper) {

        var element = document.getElementById(_id);
        $(element).datepicker({

            dateFormat: 'dd/mm/yy',


            onClose: function () {
                //let validatedValue = validateDate($(this));
                //$(this).val(validatedValue);

            },
            onSelect: function (dateText) {
                // Call the Blazor method to update the date               
                _dotNetHelper.invokeMethodAsync('datePickerSelectedValue', dateText);
            }
        });


    },
    handlePasteEvent: function (_id, _dotNetHelper) {

        document.getElementById(_id).addEventListener('paste', function (event) {
            event.preventDefault();
            const pasteData = event.clipboardData.getData('Text');
            _dotNetHelper.invokeMethodAsync('ReceivePastedData', pasteData);

        });
    },

    fireToastEvent: function (toastType, toastTitle, toastMessage) {



        const toastPlacementContainer = document.querySelector('.toast-placement-ex');
        let selectedType, selectedPlacement, toastPlacement;

        function toastDispose(toast) {
            if (toast && toast._element !== null) {
                if (toastPlacementContainer) {
                    toastPlacementContainer.classList.remove(selectedType);
                    DOMTokenList.prototype.remove.apply(toastPlacementContainer.classList, selectedPlacement);
                }
                toast.dispose();
            }
        }

        $("#toastBody").text(toastMessage);
        $("#toastTitle").text(toastTitle);

        if (toastPlacement) {
            toastDispose(toastPlacement);
        }

        selectedType = toastType; //'bg-primary';//document.querySelector('#selectTypeOpt').value;
        selectedPlacement = 'top-0 start-0';//document.querySelector('#selectPlacement').value.split(' ');

        toastPlacementContainer.classList.add(selectedType);
        // DOMTokenList.prototype.add.apply(toastPlacementContainer.classList, selectedPlacement);
        toastPlacement = new bootstrap.Toast(toastPlacementContainer);
        toastPlacement.show();

    },

    closeModal: function (modalId) {

        var modalElement = document.getElementById(modalId);
        if (modalElement) {
            $(modalElement).modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

    },

    focusElement: function (id) {
        const element = document.getElementById(id);
        element.focus();
    },
    validateFormById: function (formId) {
        const form = document.getElementById(formId);

        if (form.checkValidity()) {
            return true;
        } else {

            form.classList.add('was-validated');
            return false;
        }
    },


};


function custom_Control_Event() {

    const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    popoverTriggerList.map(function (popoverTriggerEl) {
        // added { html: true, sanitize: false } option to render button in content area of popover
        return new bootstrap.Popover(popoverTriggerEl, { html: true, sanitize: false });
    });


    // Init BS Tooltip
    //const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    //tooltipTriggerList.map(function (tooltipTriggerEl) {
    //    return new bootstrap.Tooltip(tooltipTriggerEl);
    //});
    $('[data-bs-toggle="tooltip"]').tooltip({
        trigger: 'hover'
    })

    // Function to validate the date
    function validateDate(e) {

        var value = e.val();

        const parts = value.split('/');
        if (parts.length === 3) {
            const day = parseInt(parts[0], 10);
            const month = parseInt(parts[1], 10);
            const year = parseInt(parts[2], 10);

            if (year < 1000 || year > 9999 || parts[2][0] === '0') {
                alert("Invalid year");
                return parts[0] + '/' + parts[1] + '/';
            }

            const daysInMonth = new Date(year, month, 0).getDate();
            if (day > daysInMonth) {
                alert("Invalid day for the given month");
                return parts[0] + '/' + parts[1] + '/';
            }

            const inputDate = new Date(year, month - 1, day);

            var maxDate = e.attr("MaxDate");
            var minDate = e.attr("MinDate");

            if (maxDate && maxDate !== null) {

                try {
                    var dateParts = maxDate.split("/");

                    // month is 0-based, that's why we need dataParts[1] - 1
                    var maxDateValue = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);

                    if (inputDate > maxDateValue) {
                        fireToastEvent('bg-warning', 'Date is greater than the maximum allowed date');
                        // alert("Date is greater than the maximum allowed date: " + maxDate);
                        return parts[0] + '/' + parts[1] + '/';
                    }

                }
                catch (err) { }

            }

            if (minDate && minDate !== null) {

                try {
                    var dateParts = minDate.split("/");

                    // month is 0-based, that's why we need dataParts[1] - 1
                    var minDateValue = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);

                    if (inputDate < minDateValue) {
                        alert("Date is less than the minimum allowed date: " + minDate);
                        e.focus();
                        return parts[0] + '/' + parts[1] + '/';
                    }

                }
                catch (err) { }

            }

        }
        return value;
    }


    function fireToastEvent(toastType, toastTitle, toastMessage) {



        const toastPlacementContainer = document.querySelector('.toast-placement-ex');
        let selectedType, selectedPlacement, toastPlacement;

        function toastDispose(toast) {
            if (toast && toast._element !== null) {
                if (toastPlacementContainer) {
                    toastPlacementContainer.classList.remove(selectedType);
                    DOMTokenList.prototype.remove.apply(toastPlacementContainer.classList, selectedPlacement);
                }
                toast.dispose();
            }
        }

        $("#toastBody").text(toastMessage);
        $("#toastTitle").text(toastTitle);

        if (toastPlacement) {
            toastDispose(toastPlacement);
        }

        selectedType = toastType; //'bg-primary';//document.querySelector('#selectTypeOpt').value;
        selectedPlacement = 'top-0 start-0';//document.querySelector('#selectPlacement').value.split(' ');

        toastPlacementContainer.classList.add(selectedType);
        // DOMTokenList.prototype.add.apply(toastPlacementContainer.classList, selectedPlacement);
        toastPlacement = new bootstrap.Toast(toastPlacementContainer);
        toastPlacement.show();

    }



    let amount = document.querySelectorAll('input[data-validation-type="amount"]');

    amount.forEach(item => {


        item.addEventListener('keypress', function (e) {



            var $this = this;

            var decimalLength = $(this).attr("data-decimal-point");

            if ((e.which != 46 || $this.val().indexOf('.') != -1) &&
                ((e.which < 48 || e.which > 57) &&
                    (e.which != 0 && e.which != 8))) {
                e.preventDefault();
            }

            var text = $(this).val();


            if ((e.which == 46) && (text.indexOf('.') == -1)) {
                setTimeout(function () {
                    if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                        $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                    }
                }, 1);
            }

            if ((text.indexOf('.') != -1) &&
                (text.substring(text.indexOf('.')).length > decimalLength) &&
                (e.which != 0 && e.which != 8) &&
                ($(this)[0].selectionStart >= text.length - decimalLength)) {
                e.preventDefault();
            }

        });

    });



    let numbersOnly = document.querySelectorAll('input[data-validation-type="numbersOnly"]');

    numbersOnly.forEach(item => {


        item.addEventListener('input', function (e) {

            var enableZero = $(this).attr("data-leading-zero");
            if (typeof enableZero != 'undefined' && enableZero.toLowerCase() === "false") {
                if (this.value.startsWith('0')) {
                    this.value = "";
                }
                else {
                    this.value = this.value.replace(/[^0-9]/g, '');
                }

            }
            else {
                this.value = this.value.replace(/[^0-9]/g, '');
            }



        });

    });


    $('.calenderPicker').click(function () {
        $(this).next('input.datepicker').focus();
    });



    let inputdatatype = document.querySelectorAll('input[data-type="datepicker"]');


    inputdatatype.forEach(item => {

        item.addEventListener('blur', function (e) {


            let validatedValue = validateDate($(this));
            $(this).val(validatedValue);

        });

    });




    let t = [].slice.call(document.querySelectorAll(".chat-contact-list-item:not(.chat-contact-list-item-title)")),
        i = document.querySelector(".menu-search-input");

    function v(e, a, c, t) {
        e.forEach(e => {
            var t = e.textContent.toLowerCase();
            if (!c || -1 < t.indexOf(c)) {
                e.classList.add("d-flex");
                e.classList.remove("d-none");
                a++;
            }
            else {
                e.classList.add("d-none");
            }

        }
        ),
            0 == a ? t.classList.remove("d-none") : t.classList.add("d-none")
    }


    t.forEach(e => {
        e.addEventListener("click", e => {
            t.forEach(e => {
                e.classList.remove("active")
            }
            ),
                e.currentTarget.classList.add("active")
        }
        )
    }
    ),
        i && i.addEventListener("keyup", e => {
            var e = e.currentTarget.value.toLowerCase()
                , g = document.querySelector(".gb-list-item-0")
                , d = document.querySelector(".dc-list-item-0")
                , c = [].slice.call(document.querySelectorAll("#generalBanking li:not(.gb-module-title)"))
                , r = [].slice.call(document.querySelectorAll("#deliveryChannel li:not(.dc-module-title)"));
            v(c, 0, e, g),
                v(r, 0, e, d)
        }
        )
        ;



    ///* Added from Shakil Chowdhury */

    //let i = document.querySelector(".menu-search-input");

    //function v(items, searchTerm, noDataElement) {
    //    let hasMatch = false;

    //    items.forEach(item => {
    //        const text = item.textContent.toLowerCase();
    //        if (!searchTerm || text.includes(searchTerm)) {
    //            item.classList.add("d-flex");
    //            item.classList.remove("d-none");
    //            hasMatch = true;
    //        } else {
    //            item.classList.add("d-none");
    //            item.classList.remove("d-flex");
    //        }
    //    });

    //    // Show or hide "No data found" for this list
    //    if (!hasMatch && searchTerm) {  // Show only if there's a search term and no match
    //        noDataElement.classList.add("d-flex");
    //        noDataElement.classList.remove("d-none");
    //    } else {
    //        noDataElement.classList.add("d-none");
    //        noDataElement.classList.remove("d-flex");
    //    }
    //}

    //function applyFilter() {
    //    const searchTerm = i.value.toLowerCase();

    //    // Get all <ul> elements in the sidebar-body
    //    const ulElements = document.querySelectorAll(".sidebar-body ul");

    //    // Loop through each <ul>
    //    ulElements.forEach(ul => {
    //        // Select the "No data found" element dynamically for this <ul>
    //        const noDataElement = ul.querySelector(".module-list-item-0");

    //        // Select all the list items in this <ul>, excluding the title and "No Data Found" element
    //        const items = [].slice.call(ul.querySelectorAll("li:not(.module-title, .module-list-item-0)"));

    //        // Apply filtering logic independently for each <ul>
    //        v(items, searchTerm, noDataElement);
    //    });
    //}

    //// Add both keyup and input event listeners
    //i.addEventListener("keyup", applyFilter);
    //i.addEventListener("input", applyFilter);

    let srcUrl = [].slice.call(document.querySelectorAll(".tab-content-menu ul li:not(.module-title)"));

    srcUrl.forEach(item => {
        item.addEventListener('click', function (e) {
            // Hide the container
            $(".searchPages").hide();

            // Clear the search input text
            i.value = '';

            // Reapply filter to reset items visibility
            applyFilter();
        });
    });


  

}