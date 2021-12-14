const {Schema, model} = require('mongoose')

const User = new Schema({
    username: {type: String, unique: true, required: true},
    password: {type: String, required: true},
    roles: [{type: String, ref: 'Role'}],
    name:{type: String, unique: false, required: true},
    surname:{type: String, unique: false, required: true},
    access:{type: Boolean, unique: false, required: false}
})

module.exports = model('User', User)