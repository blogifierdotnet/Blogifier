

import { deleteAsync } from 'del';

import gulp from 'gulp';

import sprite from 'gulp-svg-sprite';

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

const svgSprite = () => {
  let stream = src("./svg/**/*.svg");
  stream = stream.pipe(
    sprite({
      mode: {
        symbol: {
          sprite: '../icon-sprites.svg',
        },
      }
    })
  );
  return stream.pipe(dest('dist/img'));
}

const watcher = () => {
  watch('./svg/**/*.svg', series(svgSprite));
};

export default series(
  clean,
  parallel(
    svgSprite,
    watcher
  )
);

const build = series(
  clean,
  svgSprite,
);

export { debug, release, build };

