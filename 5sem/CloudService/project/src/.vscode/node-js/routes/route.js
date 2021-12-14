const { Router } = require("express");
const router = Router();
const controller = require("./authController");
const studController = require("./studentController");
const authMiddleware = require("../middleWare/authMiddleware");
const roleMiddleware = require("../middleWare/roleMiddleware");
const { secret } = require("./config")
const jwt = require('jsonwebtoken')
const { check } = require("express-validator");
const User = require("../models/User");
const Student = require("../models/Student");

const isAdministror = false;


// Registration users
router.post(
  "/registration",
  [
    check("username", "имя пользователя не может быть пустым").notEmpty(),
    check("password", "Пароль должен быть больше 4 символов").isLength({
      min: 4,
    }),
  ],
  controller.registrarion
);

router.post("/login", controller.login);
router.post("/editStudent", studController.editStudent);
router.post("/editUser", controller.editUser);
router.get("/logout", controller.logout);

router.get("/registrationView", async (req, res) => {
  res.render("registrationView", {
    title: "registration",
    isReg: true,
  });
});


router.get("/", async (req, res) => {
  let token = req.cookies.token;
  if (token) {
    let user = jwt.verify(token, secret);
    let isStudent = (user.roles == "USER") ? true: false;
    Student.findOne({ userId: user.id }, (err, doc) => {
      if (!err) {
        res.render("index", {
          title: "index",
          user: doc,
          isStudent:isStudent,
          isIndex:true
        });
      }
    }).lean();
  }else{
    res.render("index", {
      title: "index",
      isIndex:true
    });
  }
});

router.get("/loginView", async (req, res) => {
  res.render("loginView", {
    title: "login",
    isAutorize: true,
  });
});


router.get("/adminView", roleMiddleware(["ADMIN"]), controller.getUsers);
router.get("/adminView", async (req, res) => {
  res.render("adminView", {
    title: "admin menu",
    isAdmin: true,
  });
});


router.get("/studentView", roleMiddleware(["ADMIN", "TEACHER"]), studController.getStudents);
router.get("/studentView", async (req, res) => {
  res.render("studentView", {
    title: "studentView menu",
  });
});


router.get("/studentDetail/:id", async (req, res) => {
  Student.findById(req.params.id, (err, doc) => {
    if (!err) {
      res.render("Detail", {
        title: "Detail student",
        student: doc
      });
    }
  }).lean();
});


router.get("/editUser/:id", async (req, res) => {
  User.findById(req.params.id, (err, doc) => {
    if (!err) {
      res.render("editUser", {
        title: "Edit user",
        user: doc
      });
    }
  }).lean();
});



router.get("/delete/:id", async (req, res) => {
  User.findByIdAndRemove(req.params.id, (err, doc) => {
    if (!err) {
      res.redirect("/adminView");
    }
    else {
      console.log("Error from delete " + err)
    }
  }).lean();
});

module.exports = router;
