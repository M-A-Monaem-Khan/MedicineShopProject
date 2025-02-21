window.radialMenu = {
    toggleMenu: function (menuVisible) {
        const selector = document.querySelector('.selector');
        if (menuVisible) {
            selector.classList.add('open');
            radialMenu.rotateMenu(menuVisible);
        } else {
            selector.classList.remove('open');
            radialMenu.rotateMenu(menuVisible);
        }
    },

    rotateMenu: function (menuVisible) {
        const liElementsOne = document.querySelectorAll('.selector .orbit-one li');
        const liElementsTwo = document.querySelectorAll('.selector .orbit-two li');

        const totalOne = liElementsOne.length;
        const totalTwo = liElementsTwo.length;
        const angleStepOne = 360 / totalOne;
        const angleStepTwo = 360 / totalTwo;

        liElementsOne.forEach((li, index) => {
            const angle = menuVisible ? index * angleStepOne : -360;
            li.style.transform = `rotate(${angle}deg)`;
            const label = li.querySelector('label');
            label.style.transform = `rotate(${-angle}deg)`;
        });

        liElementsTwo.forEach((li, index) => {
            const angle = menuVisible ? index * angleStepTwo : -180;
            li.style.transform = `rotate(${angle}deg)`;
            const label = li.querySelector('label');
            label.style.transform = `rotate(${-angle}deg)`;
        });
    },
       
};


