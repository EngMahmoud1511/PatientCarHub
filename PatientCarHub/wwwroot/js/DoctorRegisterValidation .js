document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("doctorRegisterForm");
    const firstName = document.querySelector('input[name="FirstName"]');
    const lastName = document.querySelector('input[name="LastName"]');
    const userName = document.querySelector('input[name="UserName"]');
    const email = document.querySelector('input[name="Email"]');
    const password = document.querySelector('input[name="Password"]');
    const address = document.querySelector('input[name="Address"]');
    const nationalId = document.querySelector('input[name="NationalId"]');
    const specialization = document.querySelector('select[name="Spesialization"]');
    const idPath = document.querySelector('input[name="IdPath"]');
    const picturePath = document.querySelector('input[name="PicturePath"]');

    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const nationalIdPattern = /^[1-9]{1}\d*$/;

    // Validate form on submission
    form.addEventListener("submit", function (event) {
        let valid = true;

        // Clear previous error messages
        document.querySelectorAll('.text-danger').forEach(span => span.textContent = '');

        // Validate First Name
        if (firstName.value.trim() === "") {
            document.querySelector('[asp-validation-for="FirstName"]').textContent = "First Name is required.";
            valid = false;
        }

        // Validate Last Name
        if (lastName.value.trim() === "") {
            document.querySelector('[asp-validation-for="LastName"]').textContent = "Last Name is required.";
            valid = false;
        }

        // Validate User Name
        if (userName.value.trim() === "") {
            document.querySelector('[asp-validation-for="UserName"]').textContent = "User Name is required.";
            valid = false;
        }

        // Validate Email
        if (email.value.trim() === "") {
            document.querySelector('[asp-validation-for="Email"]').textContent = "Email is required.";
            valid = false;
        } else if (!emailPattern.test(email.value.trim())) {
            document.querySelector('[asp-validation-for="Email"]').textContent = "Please enter a valid email.";
            valid = false;
        }

        // Validate Password
        if (password.value.trim() === "") {
            document.querySelector('[asp-validation-for="Password"]').textContent = "Password is required.";
            valid = false;
        }

        // Validate Address
        if (address.value.trim() === "") {
            document.querySelector('[asp-validation-for="Address"]').textContent = "Address is required.";
            valid = false;
        }

        // Validate National ID
        if (nationalId.value.trim() === "") {
            document.querySelector('[asp-validation-for="NationalId"]').textContent = "National ID is required.";
            valid = false;
        } else if (!nationalIdPattern.test(nationalId.value.trim())) {
            document.querySelector('[asp-validation-for="NationalId"]').textContent = "National ID must be a positive number.";
            valid = false;
        }

        // Validate Specialization
        if (specialization.value.trim() === "") {
            alert("Please select a specialization.");
            valid = false;
        }

        // Validate IdPath
        if (idPath.files.length === 0) {
            document.querySelector('[asp-validation-for="IdPath"]').textContent = "ID file is required.";
            valid = false;
        } else if (!/\.(jpg|jpeg|png|gif)$/i.test(idPath.files[0].name)) {
            document.querySelector('[asp-validation-for="IdPath"]').textContent = "ID file must be an image (jpg, jpeg, png, gif).";
            valid = false;
        }

        // Validate PicturePath
        if (picturePath.files.length === 0) {
            document.querySelector('[asp-validation-for="PicturePath"]').textContent = "Picture file is required.";
            valid = false;
        } else if (!/\.(jpg|jpeg|png|gif)$/i.test(picturePath.files[0].name)) {
            document.querySelector('[asp-validation-for="PicturePath"]').textContent = "Picture file must be an image (jpg, jpeg, png, gif).";
            valid = false;
        }

        if (!valid) {
            event.preventDefault(); // Prevent form submission if validation fails
        }
    });
});
