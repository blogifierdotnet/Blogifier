import resolve from '@rollup/plugin-node-resolve';
import terser from '@rollup/plugin-terser';

export default {
  input: 'js/index.js',
  output: {
    format: 'iife',
    file: '../wwwroot/js/index.js',
    plugins: [terser()]
  },
  plugins: [
    resolve()
  ]
};
