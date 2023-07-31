import { nodeResolve } from '@rollup/plugin-node-resolve';
import commonjs from '@rollup/plugin-commonjs';
import terser from '@rollup/plugin-terser';

export default [
  {
    input: 'js/blogifier.js',
    output: {
      format: 'iife',
      file: 'dist/admin/js/blogifier.js',
      sourcemap: false,
      minifyInternalExports: true,
      plugins: [terser()]
    },
    plugins: [
      commonjs(),
      nodeResolve({ browser: true }),
    ]
  },
  {
    input: 'js/editor.js',
    output: {
      format: 'es',
      file: 'dist/admin/js/editor.js',
      sourcemap: false,
      minifyInternalExports: true,
      plugins: [terser()]
    },
    plugins: [
      commonjs(),
      nodeResolve({ browser: true }),
    ]
  }
];
