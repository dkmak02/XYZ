const express = require('express');
const cors = require('cors');
const morgan = require('morgan');
const rateLimit = require('express-rate-limit');

const userRouter = require('./routes/userRoutes');

const app = express();

const singUp = require('./controllers/authController');
app.use(cors());
app.use(morgan('dev'));
app.use(express.json({ limit: '10kb' }));
const limiter = rateLimit({
  max: 100,
  windowMs: 60 * 60 * 1000,
  message: 'Too many requests from this ip',
});
app.use('/api', limiter);
app.use('/api/users', userRouter);
module.exports = app;
