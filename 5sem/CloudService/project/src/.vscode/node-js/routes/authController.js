const User = require('../models/User')
const Student = require('../models/Student')
const Role = require('../models/Role')
const bcrypt = require('bcryptjs')
const jwt = require('jsonwebtoken')
const { validationResult } = require('express-validator')
const { secret } = require("./config")


const generateAccessToken = (id, roles) => {
    const payload = {
        id,
        roles
    }
    return jwt.sign(payload, secret, { expiresIn: "24h" })
}

class authController {
    async registrarion(req, res) {
        try {
            const errors = validationResult(req)
            if (!errors.isEmpty()) {
                res.status(400)
                return res.render('loginView', {
                    message: `Ошибка при регистрации`,
                    messageClass: 'alert-danger'
                });
            }
            const { username, password, name, surname } = req.body
            const candidate = await User.findOne({ username })
            if (candidate) {
                res.status(400)
                return res.render('loginView', {
                    message: "Пользователь с таким именем уже существует",
                    messageClass: 'alert-danger'
                });
            }

            const hashPassword = bcrypt.hashSync(password, 7);
            const userRole = await Role.findOne({ value: "USER" })
            const user = new User({ username, password: hashPassword, roles: [userRole.value], name, surname })
            await user.save()
            return res.redirect('/loginView')
        }
        catch (e) {
            res.status(400).send({ message: 'Registration error' })
        }
    }
    async login(req, res) {
        try {
            const { username, password } = req.body
            const user = await User.findOne({ username })
            if (!user) {
                res.status(400)
                return res.render('loginView', {
                    message: `Пользователь ${username} не найден`,
                    messageClass: 'alert-danger'
                });
            }
            const validPassword = bcrypt.compareSync(password, user.password)
            if (!validPassword) {
                res.status(400)
                return res.render('loginView', {
                    message: `Введен неверный пароль`,
                    messageClass: 'alert-danger'
                });
            }
            //получение токена для доступа
            const token = generateAccessToken(user._id, user.roles)
            user.access = true;
            res.cookie('token', token, { maxAge: 30 * 24 * 60 * 60 * 1000, httpOnly: false })
            return res.redirect("/")

        } catch (e) {
            console.log(e)
            res.status(400).send({ message: 'Login error' })
        }
    }

    async logout(req, res, next) {
        try {
            res.clearCookie('token');
            return res.redirect("/")
        } catch (e) {
            next(e);
        }
    }


    async editUser(req, res) {
        try {
            User.findOneAndUpdate({ _id: req.body._id }, req.body, { new: true },async (err, doc) => {
                if (!err) { res.redirect("/adminView") }
                const { username, name, surname } = req.body
                const candidate = await Student.findOne({username})
                if (req.body.roles === "USER") {
                    if(!candidate){
                        const student = new Student({ userId:req.body._id, username:req.body.username,name:req.body.name,surname:req.body.surname, })
                        await student.save()
                    }
                }
            });
        } catch (e) {
            res.status(404).send({ message: 'Error' })
        }
    }

  
    async getUsers(req, res) {
        try {
            const users = await User.find().lean()
            res.render('adminView', {
                users
            });
        } catch (e) {
            console.log(e)
        }
    }
}
module.exports = new authController()