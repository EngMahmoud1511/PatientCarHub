document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const firstName = document.querySelector("input[name='FirstName']");
    const lastName = document.querySelector("input[name='LastName']");
    const userName = document.querySelector("input[name='UserName']");
    const email = document.querySelector("input[name='Email']");
    const password = document.querySelector("input[name='Password']");
    const nationalId = document.querySelector("input[name='NationalId']");

    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    form.addEventListener("submit", function (event) {
        let valid = true;
        event.preventDefault();

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

        // Validate National ID
        if (nationalId.value.trim() === "") {
            nationalId.nextElementSibling.textContent = "National ID is required.";
            valid = false;
        } else {
            nationalId.nextElementSibling.textContent = "";
        }

        if (valid) {
            form.submit();
        }
    });
});
