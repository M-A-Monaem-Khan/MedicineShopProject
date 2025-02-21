/**
 * Perfect Scrollbar
 */
'use strict';

function fhScrollbar() {
        const verticalExample = document.getElementById('vertical-example'),
            horizontalExample = document.getElementById('horizontal-example'),
            horizVertExample = document.getElementById('both-scrollbars-example'),
            mainContainer = document.getElementById('main-container'),
            menuScroll = document.getElementById('menu-scroll');

        // Vertical Example
        // --------------------------------------------------------------------
        if (verticalExample) {
            new PerfectScrollbar(verticalExample, {
                wheelPropagation: false
            });
        }

        if (mainContainer) {
            new PerfectScrollbar(mainContainer, {
                wheelPropagation: false
            });
        }

        //if (menuScroll) {
        //    new PerfectScrollbar(menuScroll, {
        //        wheelPropagation: false
        //    });
        //}
        

        // Horizontal Example
        // --------------------------------------------------------------------
        if (horizontalExample) {
            new PerfectScrollbar(horizontalExample, {
                wheelPropagation: false,
                suppressScrollY: true
            });
        }

        // Both vertical and Horizontal Example
        // --------------------------------------------------------------------
        if (horizVertExample) {
            new PerfectScrollbar(horizVertExample, {
                wheelPropagation: false
            });
        }
    };

