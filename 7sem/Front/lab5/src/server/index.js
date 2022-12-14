const express = require('express')
const cors = require('cors')
const bodyParser = require('body-parser')
const path = require('path')
const fs = require("fs");
const currentPath = __dirname + '/data/students.json';

const PORT =  5000

const app = express()
app.use(cors())

app.use(bodyParser.urlencoded({ extended: false }))

// parse application/json
app.use(bodyParser.json())
app.use(express.static(__dirname + '/public'));


//start server 
app.listen(PORT, () => {
  console.log("server started");
})


//Get students
app.get("/students", (req, res) => {
  fs.readFile(currentPath, (err, data) => {
    if (err) throw err;
    let students = JSON.parse(data);
    res.send(students);
  });
});


//Insert student
app.post("/students", (req, res) => {
  // res.set({
  //   'Accept': 'application/json',
  //   'Content-Type': 'application/json',
  //   'Access-Control-Allow-Origin': '*',
  //   'Access-Control-Allow-Methods': 'GET,PUT,POST,DELETE'
  // });
  fs.readFile(currentPath, (err, data) => {
    if (err) throw err;
    let students = JSON.parse(data);
    let newStudent = req.body;
    students.students.push(newStudent);
    fs.writeFileSync(currentPath, JSON.stringify(students), function (err) {
      if (err) console.log(err);
    });
    res.send({ message: `Object with id:${newStudent.id} has been added` })
  });
});


//Delete student
app.delete("/students/:id", (req, res) => {
  let currentId = req.url.split('/')[2]
  fs.readFile(currentPath, (err, data) => {
    if (err) throw err;
    let students = JSON.parse(data);
    deleteStudent(students.students, currentId);
    fs.writeFile(currentPath, JSON.stringify(students), function (err) {
      if (err) console.log(err);
    });
    res.send({ message: `Object with id:${currentId} has been deleted` })
  });
});


//Update student
app.put("/students/:id", (req, res) => {
  let currentId = req.url.split('/')[2]
  let updatedStudent = req.body;
  console.log(updatedStudent);
  fs.readFile(currentPath, (err, data) => {
    if (err) throw err;
    let students = JSON.parse(data);
    updateStudent(students.students, currentId,updatedStudent);
    fs.writeFile(currentPath, JSON.stringify(students), function (err) {
      if (err) console.log(err);
    });
    res.send({ message: `Object with id:${currentId} has been updated` })
  });
});

/**
 * Find a student and delete him.
 * @param {arr} array
 * @param {id} id
 */
function deleteStudent(arr, id) {
  let currentIndex;
  arr.forEach(function (entry, index) {
    if (entry.id === id) currentIndex = index;
  });
  arr.splice(currentIndex, 1);
}


/**
 * Find a student and update him.
 * @param {arr} array
 * @param {id} id
 */
 function updateStudent(arr, id,updatedStudent) {
  let currentIndex;
  arr.forEach(function (entry, index) {
    if (entry.id === id) currentIndex = index;
  });
  arr[currentIndex] = updatedStudent;
  updatedStudent.id = id;
}