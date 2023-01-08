// When the user scrolls down 80px from the top of the document, resize the navbar's padding and the logo's font size
window.onscroll = function() {scrollFunction()};

function scrollFunction() {
  if (document.body.scrollTop > 80 || document.documentElement.scrollTop > 80) {
    document.getElementById("header").style.padding = "0px 0px";
    document.getElementById("header").style.background = "#DF4441";
    document.getElementsByClassName("list-item").style.color = "#312528";
  } else {
    document.getElementById("header").style.padding = "50px 0px";
    document.getElementById("header").style.background = "transparent";
  }
}

