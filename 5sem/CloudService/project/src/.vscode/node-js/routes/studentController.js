const Student = require('../models/Student')


class studentController {
    async getStudents(req, res) {
        try {
            const students = await Student.find().lean()
            res.render('studentView', {
                students
            });
        } catch (e) {
            console.log(e)
        }
    }

    async editStudent(req, res) {
        try {
            Student.findOneAndUpdate({ _id: req.body._id }, req.body, { new: true },async (err, doc) => {
                if (!err) { res.redirect("/studentView") }
            });
        } catch (e) {
            res.status(404).send({ message: 'Error' })
        }
    }

    // async addExam(req, res) {
    //     try {
    //         const students = await Student.find().lean()            
    //         }
    //     catch (e) {
    //         res.status(404).send({ message: 'Error' })
    //     }
    // }
}
module.exports = new studentController()