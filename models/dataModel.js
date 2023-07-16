const mongoose = require('mongoose');

const dataSchema = new mongoose.Schema({
  text: {
    type: String,
    // Jaskio {0}rsadasd
  },
  correct: {
    type: Array,
    // [ż, ó, ł, ą, ę, ś, ć, ź, ń]
  },
  wrong: {
    type: Array,
    // [z, x, c, v, b, n, m, a, s, d, f, g, h, j, k, l, q, w, e, r, t, y, u, i, o, p]
  },
});

const Data = mongoose.model('Data', dataSchema);
module.exports = Data;
