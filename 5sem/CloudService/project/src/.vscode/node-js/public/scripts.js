function parseJwt (token) {
    if (!token)return
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
};

function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
      "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
  }

let token = getCookie("token")
let parsedToken = parseJwt(token)

const forAdmin = document.querySelectorAll(".forAdmin").forEach( function(elem){
    elem.style.display = "none";
    if(!parsedToken)return
    let roles = parsedToken.roles
    if (roles.includes('ADMIN')){
        elem.style.display = "block";
    }
}); 

const forLogin = document.querySelectorAll(".forLogin").forEach(function(elem){
    if(token) elem.style.display = "none";
});

const forLoguot = document.querySelectorAll(".forLoguot").forEach(function(elem){
    if(!token) elem.style.display = "none";
});