

import { deleteAsync } from 'del';

import gulp from 'gulp';
import plumber from 'gulp-plumber';
import notify from 'gulp-notify';
import size from 'gulp-size';
import uglify from 'gulp-uglify';
import sourcemaps from 'gulp-sourcemaps';

import buffer from 'vinyl-buffer';
import source from 'vinyl-source-stream';

import rollupStream from '@rollup/stream';
import { babel } from '@rollup/plugin-babel';
import { nodeResolve } from '@rollup/plugin-node-resolve';
import commonjs from '@rollup/plugin-commonjs';
import terser from '@rollup/plugin-terser';

import dartSass from 'sass';
import gulpSass from 'gulp-sass';
const sass = gulpSass(dartSass);
import postcss from 'gulp-postcss';
import autoprefixer from 'autoprefixer';
import cssnano from 'cssnano';

const { src, dest, watch, series, parallel } = gulp;

// Clear shell screen
console.clear();

const clean = () => {
  return deleteAsync(['./dist']);
};

// 设置一个全局变量，用于指定当前的模式，默认为Debug模式
let mode = 'Debug';
const debug = (done) => {
  mode = 'Debug';
  done();
};
const release = (done) => {
  mode = 'Release';
  done();
};

// Rollup.js
const rollupJs = () => {
  let outputOptions = {
    sourcemap: true,
    format: 'iife'
  }

  if (mode !== 'Debug') {
    outputOptions.sourcemap = false;
    outputOptions.minifyInternalExports = true;
    outputOptions.plugins = [terser()];
  }

  let stream = rollupStream({
    input: 'js/blogifier.js',
    output: outputOptions,
    plugins: [
      babel({
        exclude: 'node_modules/**',
        presets: ['@babel/preset-env'],
        babelHelpers: 'bundled',
      }),
      nodeResolve({
        browser: true,
        preferBuiltins: false,
      }),
      commonjs({
        include: ['node_modules/**'],
        exclude: [],
        sourceMap: mode === 'Debug',
      }),
    ],
  })

  stream = stream.pipe(source('blogifier.js'));
  if (mode !== 'Debug') {
    // JS Minify
    stream = stream.pipe(buffer())
    stream = stream.pipe(size())
    stream = stream.pipe(plumber())
    stream = stream.pipe(uglify())
    stream = stream.on('error', notify.onError())
    stream = stream.pipe(size())
  }
  return stream.pipe(dest('dist/js'));
}

// sass
const scss = () => {
  let stream = src("./scss/**/*.scss")

  if (mode === 'Debug') {
    stream = stream.pipe(sourcemaps.init())
  }

  stream = stream.pipe(
    sass({
      outputStyle: mode === 'Debug' ? 'expanded' : 'compressed',
      errLogToConsole: false,
      includePaths: ['node_modules', 'bower_components', 'scss', '.'],
    })
  )
  stream = stream.on('error', notify.onError());

  if (mode === 'Debug') {
    stream = stream.pipe(sourcemaps.write())
  } else {
    stream = stream.pipe(size())
    stream = stream.pipe(plumber())
    stream = stream.pipe(postcss([autoprefixer(), cssnano()]))
    stream = stream.on('error', notify.onError())
    stream = stream.pipe(size())
  }
  return stream.pipe(dest('dist/css'));
}

const watcher = () => {
  watch('./js/**/*.js', series(rollupJs));
};

export default series(
  clean,
  parallel(
    scss,
    rollupJs,
    watcher
  )
);

const build = series(
  clean,
  parallel(
    scss,
    rollupJs
  )
);

export { debug, release, build };

