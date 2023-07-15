const dotenv = require('dotenv');
const mongoose = require('mongoose');

dotenv.config({ path: './config.env' });
const app = require('./app');

process.on('uncaughtException', (err) => {
  console.log('Uncaught Exception --server shoutdown');
  console.log(err.name, err.message);
  process.exit(1);
});
const DB = process.env.DATABASE.replace('<PASSWORD>', process.env.DB_PASSWORD);
mongoose
  .connect(DB, {
    useNewUrlParser: true,
    useUnifiedTopology: true,
  })
  .then(() => console.log('Connection succesful'));
const port = process.env.PORT || 3000;
const server = app.listen(port, () =>
  console.log(`To do list API listening on port ${port}!`),
);
process.on('unhandle dRejection', (err) => {
  console.log('Rejected Promise --server shoutdown');
  console.log(err.name, err.message);
  server.close(() => {
    process.exit(1);
  });
});
