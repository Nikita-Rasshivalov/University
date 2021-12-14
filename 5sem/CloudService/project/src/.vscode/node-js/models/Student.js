const {Schema, model} = require('mongoose')

const Student = new Schema({
    username: {type: String, unique: true, required: true},
    userId: {type: String, unique: false, required: true},
    name:{type: String, unique: false, required: true},
    surname:{type: String, unique: false, required: true},
    exams: {
        Biology: {type: Number,default:0},
        English: {type: Number,default:0},
        Math: {type: Number,default:0},
    },
    attestation: {
        Math: {type: Number,default:0},
    },
    credit: {
        Art: {type: String,default:"0"},
        PE: {type: String,default:"0"},
    },
})

module.exports = model('Student', Student)