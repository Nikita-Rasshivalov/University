//class of event
class Student {
  constructor(id, name, surname, age, speciality) {
    (this.id = id),
      (this.name = name),
      (this.surname = surname),
      (this.age = age),
      (this.speciality = speciality);
  }
}

//variables
const requestUrl = "http://localhost:3000/students";
const requestUrlChange = "http://localhost:3000/students/";
const id = document.querySelector("input[name=id]");
const firstName = document.querySelector("input[name=firstName]");
const secondName = document.querySelector("input[name=secondName]");
const age = document.querySelector("input[name=age]");
const speciality = document.querySelector("input[name=speciality]");
const nextBtn = document.querySelector(".nextBtn");
const prevBtn = document.querySelector(".prevBtn");
const allContainer = document.querySelector(".all-container");

window.addEventListener("DOMContentLoaded", () => {
  getStudents();
});

/**
 * Send request.
 * @param {method} method
 * @param {url} url
 * @param {body} body
 */
async function sendRequest(method, url, body = null) {
  const headers = {
    Accept: "application/json",
    "Content-Type": "application/json",
  };
  let response;
  switch (method) {
    case "GET":
      response = await fetch(url);
      break;
    case "DELETE":
      response = await fetch(url, { method: method });
      break;
    case "PUT":
      response = await fetch(url, {
        method: method,
        body: JSON.stringify(body),
        headers: headers,
      });
      break;
    case "POST":
      response = await fetch(url, {
        method: method,
        body: JSON.stringify(body),
        headers: headers,
      });
      break;
    default:
      break;
  }
  const data = await response.json();
  if (response.ok)
    return {
      status: response.status,
      statusText: response.statusText,
      url: response.url,
      body: data,
    };
}
//Get Students
function getStudents() {
  sendRequest("GET", requestUrl)
    .then((data) => {
      let currentCounter = 0;
   
      OutAllStudents(data.body.students);
    })
    .catch((err) => console.log(err));
}

//Out all Students
function OutAllStudents(students) {
  students.forEach((elem) => {
    let studentUl = document.createElement("ul");
    let studentLi = document.createElement("li");
    let studentP = document.createElement("p");

    let elemLi = `
        Id: ${elem.id}, 
        FirstName: ${elem.firstName}, 
        SecondName: ${elem.secondName}, 
        Age: ${elem.age}, 
        Speciality: ${elem.speciality},
        Labs:
        `;
    elem.labs.forEach((e) => {
      let Ul = document.createElement("ul");
      let Li = document.createElement("li");
      let pElem = `
            Name: ${e.name},
            Mark: ${e.mark},
            PassDate: ${e.PassDate}
            `;
      Li.appendChild(document.createTextNode(pElem));
      Ul.appendChild(Li);
      studentP.appendChild(Ul);
    });

    studentLi.appendChild(document.createTextNode(elemLi));
    studentLi.appendChild(studentP);
    studentUl.appendChild(studentLi);

    allContainer.appendChild(studentUl);
  });
}
