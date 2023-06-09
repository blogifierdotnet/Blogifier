import resolve from '@rollup/plugin-node-resolve';
import terser from '@rollup/plugin-terser';

export default {
  input: 'js/blogifier.js',
  output: {
    format: 'iife',
    file: 'dist/js/blogifier.js',
    sourcemap: true,
    minifyInternalExports: true,
    plugins: [terser()]
  },
  plugins: [
    resolve()
  ]
};
