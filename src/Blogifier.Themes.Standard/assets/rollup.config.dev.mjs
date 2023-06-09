import resolve from '@rollup/plugin-node-resolve';

export default {
  input: 'js/blogifier.js',
  output: {
    format: 'iife',
    file: 'dist/js/blogifier.js',
    sourcemap: true,
  },
  plugins: [
    resolve()
  ]
};
