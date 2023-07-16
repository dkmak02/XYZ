const express = require('express');

const authController = require('../controllers/authController');
const userController = require('../controllers/userController');

const router = express.Router();

router.post('/signup', authController.signUp);
router.post('/login', authController.login);

router.use(authController.protect);
router.get('/', userController.getAllUsers);
router
  .route('/me')
  .get(userController.getMe, userController.getUser)
  .delete(userController.getMe, userController.deleteUser)
  .patch(userController.getMe, userController.updateUser);
router.patch(
  '/updatePassword',
  userController.getMe,
  userController.updatePassword,
);
router
  .route('/:id')
  .get(userController.getUser)
  .delete(userController.deleteUser)
  .patch(userController.updateUser);

module.exports = router;
