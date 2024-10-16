document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("registerForm");
    const firstName = document.getElementById("firstName");
    const lastName = document.getElementById("lastName");
    const userName = document.getElementById("userName");
    const email = document.getElementById("email");
    const password = document.getElementById("password");
    const iAgree = document.getElementById("iAgree");

    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    // Validate form on submission
    form.addEventListener("submit", function (event) {
        let valid = true;

        // Validate First Name
        if (firstName.value.trim() === "") {
            firstName.nextElementSibling.textContent = "First Name is required.";
            valid = false;
        } else {
            firstName.nextElementSibling.textContent = "";
        }

        // Validate Last Name
        if (lastName.value.trim() === "") {
            lastName.nextElementSibling.textContent = "Last Name is required.";
            valid = false;
        } else {
            lastName.nextElementSibling.textContent = "";
        }

        // Validate User Name
        if (userName.value.trim() === "") {
            userName.nextElementSibling.textContent = "User Name is required.";
            valid = false;
        } else {
            userName.nextElementSibling.textContent = "";
        }

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

        // Validate Terms & Conditions
        if (!iAgree.checked) {
            alert("You must agree to the terms and conditions.");
            valid = false;
        }

        if (!valid) {
            event.preventDefault(); // Prevent form submission if validation fails
        }
    });
});
