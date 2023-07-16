const mongoose = require('mongoose');
const validator = require('validator');
const bcrypt = require('bcryptjs');

const userSchema = new mongoose.Schema({
  username: {
    type: String,
    required: [true, 'A user must have a username'],
    unique: true,
    trim: true,
    validate: validator.isAlphanumeric,
  },
  name: {
    type: String,
    required: [true, 'A user must have a name'],
    trim: true,
    validate: validator.isAlphanumeric,
  },
  surname: {
    type: String,
    required: [true, 'A user must have a surname'],
    trim: true,
    validate: validator.isAlphanumeric,
  },
  email: {
    type: String,
    required: [true, 'A user must have a email'],
    unique: true,
    trim: true,
    lowercase: true,
    validate: [validator.isEmail, 'Please provide a valid email'],
  },
  difficulty: {
    type: String,
    enum: ['1-3', '4-6', '7-8'],
    default: '1-3',
  },
  role: {
    type: String,
    enum: ['user', 'admin', 'parent'],
    default: 'user',
  },
  password: {
    type: String,
    required: [true, 'A user must have a password'],
    minlength: [8, 'Password must be at least 8 characters'],
    select: false,
  },
  passwordConfirm: {
    type: String,
    required: [true, 'A user must have a password'],
    validate: {
      validator: function (el) {
        return el === this.password;
      },
      message: 'Passwords are not the same',
    },
  },
  active: {
    type: Boolean,
    default: true,
    select: false,
  },
  newsletters: {
    type: Boolean,
    default: false,
  },
  weeklyUpdates: {
    type: Boolean,
    default: false,
  },
  ranking: {
    type: Boolean,
    default: false,
  },
});
userSchema.pre('save', async function (next) {
  if (!this.isModified('password')) return next();
  this.password = await bcrypt.hash(this.password, 12);
  this.passwordConfirm = undefined;
  next();
});
userSchema.methods.correctPassword = async function (
  candidatePassword,
  userPassword,
) {
  return await bcrypt.compare(candidatePassword, userPassword);
};
userSchema.pre(/^find/, function (next) {
  this.find({ active: { $ne: false } });
  next();
});
const User = mongoose.model('User', userSchema);
module.exports = User;
