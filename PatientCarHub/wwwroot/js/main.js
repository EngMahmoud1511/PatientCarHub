(function ($) {
  "use strict";

  // Spinner
  var spinner = function () {
    setTimeout(function () {
      if ($("#spinner").length > 0) {
        $("#spinner").removeClass("show");
      }
    }, 1);
  };
  spinner();

  // Initiate the wowjs
  new WOW().init();

  // Sticky Navbar
  $(window).scroll(function () {
    if ($(this).scrollTop() > 300) {
      $(".sticky-top").addClass("shadow-sm").css("top", "0px");
    } else {
      $(".sticky-top").removeClass("shadow-sm").css("top", "-100px");
    }
  });

  // Back to top button
  $(window).scroll(function () {
    if ($(this).scrollTop() > 300) {
      $(".back-to-top").fadeIn("slow");
    } else {
      $(".back-to-top").fadeOut("slow");
    }
  });
  $(".back-to-top").click(function () {
    $("html, body").animate({ scrollTop: 0 }, 1500, "easeInOutExpo");
    return false;
  });

  // Facts counter
  $('[data-toggle="counter-up"]').counterUp({
    delay: 10,
    time: 2000,
  });

  // Date and time picker
  $(".date").datetimepicker({
    format: "L",
  });
  $(".time").datetimepicker({
    format: "LT",
  });

  // Header carousel
  $(".header-carousel").owlCarousel({
    autoplay: false,
    animateOut: "fadeOutLeft",
    items: 1,
    dots: true,
    loop: true,
    nav: true,
    navText: [
      '<i class="bi bi-chevron-left"></i>',
      '<i class="bi bi-chevron-right"></i>',
    ],
  });

  // Testimonials carousel
  $(".testimonial-carousel").owlCarousel({
    autoplay: false,
    smartSpeed: 1000,
    center: true,
    dots: false,
    loop: true,
    nav: true,
    navText: [
      '<i class="bi bi-arrow-left"></i>',
      '<i class="bi bi-arrow-right"></i>',
    ],
    responsive: {
      0: {
        items: 1,
      },
      768: {
        items: 2,
      },
    },
  });
})(jQuery);

function searchPatient() {
  const input = document.getElementById("searchInput").value.toLowerCase();
  const results = ["John Doe", "Jane Smith", "Robert Brown"];
  const filtered = results.filter((patient) =>
    patient.toLowerCase().includes(input)
  );

  const resultsList = document.getElementById("patientResults");
  resultsList.innerHTML = "";

  filtered.forEach((patient) => {
    const li = document.createElement("li");
    li.textContent = patient;
    resultsList.appendChild(li);
  });
}


function searchDoctorOrHospital() {
    const input = document.getElementById('searchDocInput').value.toLowerCase();
    const results = ['City Hospital', 'Health Clinic', 'Dr. Smith', 'Dr. Brown'];
    const filtered = results.filter(item => item.toLowerCase().includes(input));

    const resultsList = document.getElementById('docResults');
    resultsList.innerHTML = '';
    
    filtered.forEach(item => {
        const li = document.createElement('li');
        li.textContent = item;
        resultsList.appendChild(li);
    });
}

function submitRating() {
    const name = document.getElementById('ratingName').value;
    const score = document.getElementById('ratingScore').value;

    if (name && score >= 1 && score <= 5) {
        const resultsList = document.getElementById('ratingResults');
        const li = document.createElement('li');
        li.textContent = `${name}: ${score} Stars`;
        resultsList.appendChild(li);
        
        document.getElementById('ratingName').value = '';
        document.getElementById('ratingScore').value = '';
    } else {
        alert("Please enter a valid name and rating (1-5).");
    }
}


