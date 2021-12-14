const express = require('express')
const mongoose = require('mongoose')
const exphbs = require('express-handlebars')
const router = require('./routes/route')
const path = require('path')
const URI = 'mongodb://localhost:27017/'
const PORT = process.env.PORT || 5000
const cookieParser = require('cookie-parser')


const app = express()
const hbs= exphbs.create({
    defaultLayout:'main',
    extname:'hbs'
})

// Register `hbs.engine` with the Express app.
app.engine('hbs', hbs.engine)
app.set('view engine','hbs')
app.set('views', 'views')


app.use(cookieParser())
app.use(express.urlencoded({ extended: true }))
app.use(express.json())
app.use(express.static(path.join(__dirname, 'public')))
app.use(router)


async function start(){
    try{
        await mongoose.connect(URI,{
            useNewUrlParser: true, 
            useUnifiedTopology: true 
        })
        app.listen(PORT, () => {
            console.log('Server has been started..')
        })
    } catch(e){
        console.log(e)
    }
}

start()


