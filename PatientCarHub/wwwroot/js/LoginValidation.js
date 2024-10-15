document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");

    const email = document.querySelector("input[name='Email']");
    const password = document.querySelector("input[name='Password']");


    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    form.addEventListener("submit", function (event) {
        let valid = true;
        event.preventDefault();

        // Validate First Name
      
        // Validate Email
        if (email.value.trim() === "") {
            email.nextElementSibling.textContent = "Email is required.";
            valid = false;
        } else if (!emailPattern.test(email.value.trim())) {
            email.nextElementSibling.textContent = "Please enter a valid email.";
            valid = false;
        } else {
            email.nextElementSibling.textContent = "";
        }

        // Validate Password
        if (password.value.trim() === "") {
            password.nextElementSibling.textContent = "Password is required.";
            valid = false;
        } else {
            password.nextElementSibling.textContent = "";
        }

       
        if (valid) {
            form.submit();
        }
    });
});
