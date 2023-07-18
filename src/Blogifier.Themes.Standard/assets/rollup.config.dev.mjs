import { nodeResolve } from '@rollup/plugin-node-resolve';
import commonjs from '@rollup/plugin-commonjs';

export default {
  input: 'js/blogifier.js',
  output: {
    format: 'iife',
    file: 'dist/js/blogifier.js',
    sourcemap: true,
  },
  plugins: [
    commonjs(),
    nodeResolve({ browser: true }),
  ]
};
