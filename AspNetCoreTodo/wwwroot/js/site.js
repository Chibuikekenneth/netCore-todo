// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//using jquery
  // $(document).ready(function() {

  //   //wire up all of the checkboxes to run markCompleted()
  //   $(".done-checkbox").on("click", function(e){
  //     markCompleted(e.target)
  //   })
  // });

//using vanilla js 
let doneCheckbox = document.getElementsByClassName("done-checkbox")

for (let i = 0; i < doneCheckbox.length; i++) {
  doneCheckbox[i].addEventListener("click", function(e){
     markCompleted(e.target)
   });
}


function markCompleted(checkbox) {
  checkbox.disable = true;

  var row = checkbox.closest("tr");
  $(row).addClass("done");

  var form = checkbox.closest("form");
  form.submit();
}


// This code first uses jQuery (a JavaScript helper library) to attach some code to the click even of all the checkboxes on the page with the CSS class done-checkbox. When a checkbox is clicked, the markCompleted() function is run.
// The markCompleted() function does a few things:
// Adds the disabled attribute to the checkbox so it can't be clicked again
// Adds the done CSS class to the parent row that contains the checkbox, which changes the way the row looks based on the CSS rules in style.css
// Submits the form