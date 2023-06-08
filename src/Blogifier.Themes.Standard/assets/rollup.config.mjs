import resolve from '@rollup/plugin-node-resolve';
import terser from '@rollup/plugin-terser';

export default {
  input: 'js/index.js',
  output: {
    format: 'iife',
    file: 'dist/js/index.min.js',
    sourcemap: true,
    minifyInternalExports: true,
    plugins: [terser()]
  },
  plugins: [
    resolve()
  ]
};
