window.captureHtmlSnapshot = (elementId) => {
    return new Promise((resolve, reject) => {
        try {
            const element = document.getElementById(elementId);
            if (element) {
                const clonedElement = element.cloneNode(true);

                const spans = clonedElement.querySelectorAll('span');
                spans.forEach(span => {
                    if (span.textContent) {
                        span.setAttribute('data-text', span.textContent);
                    }
                });

                const inputs = clonedElement.querySelectorAll('input, textarea, select');
                inputs.forEach(input => {
                    if (input.tagName === 'SELECT') {
                        input.setAttribute('disabled', 'disabled');
                        const options = input.querySelectorAll('option');
                        options.forEach(option => {
                            if (option.value === input.value) {
                                option.setAttribute('selected', 'selected');
                            } else {
                                option.removeAttribute('selected');
                            }
                        });
                    } else if (input.type === 'checkbox' || input.type === 'radio') {
                        input.setAttribute('disabled', 'disabled');
                        if (input.checked) {
                            input.setAttribute('checked', 'checked');
                        } else {
                            input.removeAttribute('checked');
                        }
                    } else {
                        input.setAttribute('readonly', 'readonly');
                        input.setAttribute('value', input.value);
                        input.removeAttribute('tooltip');
                        input.setAttribute('tooltip', input.value);
                    }
                });
                const tables = clonedElement.querySelectorAll('table');
                tables.forEach(table => {
                    const tableInputs = table.querySelectorAll('input, textarea, select');
                    tableInputs.forEach(tableInput => {
                        if (tableInput.tagName === 'SELECT') {
                            tableInput.setAttribute('disabled', 'disabled');
                            const options = tableInput.querySelectorAll('option');
                            options.forEach(option => {
                                if (option.value === tableInput.value) {
                                    option.setAttribute('selected', 'selected');
                                } else {
                                    option.removeAttribute('selected');
                                }
                            });
                        } else if (tableInput.type === 'checkbox' || tableInput.type === 'radio') {
                            tableInput.setAttribute('disabled', 'disabled');
                            if (tableInput.checked) {
                                tableInput.setAttribute('checked', 'checked');
                            } else {
                                tableInput.removeAttribute('checked');
                            }
                        } else {
                            tableInput.setAttribute('readonly', 'readonly');
                            tableInput.setAttribute('value', tableInput.value);
                            tableInput.removeAttribute('tooltip');
                            tableInput.setAttribute('tooltip', tableInput.value);
                        }
                    });

                    table.setAttribute('border', '1');
                    table.setAttribute('cellpadding', '5');
                });

                const htmlContent = clonedElement.outerHTML;

                resolve(htmlContent);
            } else {
                reject("Element not found");
            }
        } catch (error) {
            reject(error);
        }
    });
};

window.highlightChangedElements = (elementId, oldHtml, newHtml) => {
    return new Promise((resolve, reject) => {
        try {
            // Get the element by its ID
            const element = document.getElementById(elementId);

            // Parse the old and new HTML content into DOM elements
            const oldElement = new DOMParser().parseFromString(oldHtml, 'text/html').body.firstChild;
            const newElement = new DOMParser().parseFromString(newHtml, 'text/html').body.firstChild;

            // Get all input, textarea, select, and div elements from both the old and new elements
            const oldInputs = oldElement.querySelectorAll('input, textarea, select, div');
            const newInputs = newElement.querySelectorAll('input, textarea, select, div');

            // Create a Map to store the values of the old inputs by their ID
            let oldFields = new Map();
            let divFields = new Map();
            let result = '';

            // Store old input values using their ID and store div contents
            oldInputs.forEach((oldInput) => {
                if (oldInput.id) {
                    // Store input values by ID
                    oldFields.set(oldInput.id, oldInput.value);
                }
                if (oldInput.tagName === 'DIV' && oldInput.id && oldInput.className.includes('snap-panel')) {
                    // Store the old div content
                    divFields.set(oldInput.id, { oldDiv: oldInput.innerHTML, newDiv: '' });

                }
            });

            // Compare new input values and update div content
            newInputs.forEach((newInput) => {
                if (newInput.id) {
                    // Handle input, textarea, select changes
                    if (oldFields.has(newInput.id)) {
                        if (newInput.value !== oldFields.get(newInput.id)) {
                            newInput.style.backgroundColor = 'yellow';  // Highlight changed inputs
                        }
                    }

                }
            });

            newInputs.forEach((newInput) => {
                if (newInput.id) {
                    // Handle div changes
                    if (newInput.tagName === 'DIV') {
                        if (divFields.has(newInput.id)) {
                            // Update div content if it exists in divFields
                            let divContent = divFields.get(newInput.id);
                            divContent.newDiv = newInput.innerHTML;  // Update newDiv content
                            divFields.set(newInput.id, divContent);  // Save back the updated entry

                            result += '<div class="row g-3">' + '\n';
                            result += '<div class=" col-6 form-group">' + '\n';
                            result += divContent.oldDiv + '\n';
                            result += '</div>';
                            //result += '<div class="col-2 form-group" style="display: flex; align-items: center; justify-content: center;">' + '\n';
                            //result += '<div style="height: 112%; width: 2px; background-color: #00827f;"></div> </div>' + '\n';
                            result += '<div class=" col-6 form-group">' + '\n';
                            result += newInput.innerHTML;
                            result += '</div>';
                            result += '</div>';
                        }
                    }
                }
            });


            // Log the divFields map for debugging
            console.log(divFields);
            console.log(result);

            // Return the updated HTML with changes highlighted
            const updatedHtml = result;
            //const updatedHtml = newElement.outerHTML;
            resolve(updatedHtml);
        } catch (error) {
            reject(error);
        }
    });
};