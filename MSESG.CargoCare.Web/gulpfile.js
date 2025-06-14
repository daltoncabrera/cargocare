/// <binding BeforeBuild='scripts, template' />
'use strict';

// sass compile
var gulp = require('gulp');
var prettify = require('gulp-prettify');
var minifyCss = require("gulp-minify-css");
var rename = require("gulp-rename");
var uglify = require("gulp-uglify");
var rtlcss = require("gulp-rtlcss");
var connect = require('gulp-connect');
var concat = require('gulp-concat');
var templateCache = require('gulp-angular-templatecache');
//*** Localhost server tast
gulp.task('localhost', function () {
    connect.server();
});

gulp.task('localhost-live', function () {
    connect.server({
        livereload: true
    });
});


//*** CSS & JS minify task
gulp.task('minify', function () {
    // css minify 
    gulp.src(['./assets/apps/css/*.css', '!./assets/apps/css/*.min.css']).pipe(minifyCss()).pipe(rename({ suffix: '.min' })).pipe(gulp.dest('./assets/apps/css/'));

    gulp.src(['./assets/global/css/*.css', '!./assets/global/css/*.min.css']).pipe(minifyCss()).pipe(rename({ suffix: '.min' })).pipe(gulp.dest('./assets/global/css/'));
    gulp.src(['./assets/pages/css/*.css', '!./assets/pages/css/*.min.css']).pipe(minifyCss()).pipe(rename({ suffix: '.min' })).pipe(gulp.dest('./assets/pages/css/'));

    gulp.src(['./assets/layouts/**/css/*.css', '!./assets/layouts/**/css/*.min.css']).pipe(rename({ suffix: '.min' })).pipe(minifyCss()).pipe(gulp.dest('./assets/layouts/'));
    gulp.src(['./assets/layouts/**/css/**/*.css', '!./assets/layouts/**/css/**/*.min.css']).pipe(rename({ suffix: '.min' })).pipe(minifyCss()).pipe(gulp.dest('./assets/layouts/'));

    gulp.src(['./assets/global/plugins/bootstrap/css/*.css', '!./assets/global/plugins/bootstrap/css/*.min.css']).pipe(minifyCss()).pipe(rename({ suffix: '.min' })).pipe(gulp.dest('./assets/global/plugins/bootstrap/css/'));

    //js minify
    gulp.src(['./assets/apps/scripts/*.js', '!./assets/apps/scripts/*.min.js']).pipe(uglify()).pipe(rename({ suffix: '.min' })).pipe(gulp.dest('./assets/apps/scripts/'));
    gulp.src(['./assets/global/scripts/*.js', '!./assets/global/scripts/*.min.js']).pipe(uglify()).pipe(rename({ suffix: '.min' })).pipe(gulp.dest('./assets/global/scripts'));
    gulp.src(['./assets/pages/scripts/*.js', '!./assets/pages/scripts/*.min.js']).pipe(uglify()).pipe(rename({ suffix: '.min' })).pipe(gulp.dest('./assets/pages/scripts'));
    gulp.src(['./assets/layouts/**/scripts/*.js', '!./assets/layouts/**/scripts/*.min.js']).pipe(uglify()).pipe(rename({ suffix: '.min' })).pipe(gulp.dest('./assets/layouts/'));
});

//*** RTL convertor task
gulp.task('rtlcss', function () {

    gulp
        .src(['./assets/apps/css/*.css', '!./assets/apps/css/*-rtl.min.css', '!./assets/apps/css/*-rtl.css', '!./assets/apps/css/*.min.css'])
        .pipe(rtlcss())
        .pipe(rename({ suffix: '-rtl' }))
        .pipe(gulp.dest('./assets/apps/css'));

    gulp
        .src(['./assets/pages/css/*.css', '!./assets/pages/css/*-rtl.min.css', '!./assets/pages/css/*-rtl.css', '!./assets/pages/css/*.min.css'])
        .pipe(rtlcss())
        .pipe(rename({ suffix: '-rtl' }))
        .pipe(gulp.dest('./assets/pages/css'));

    gulp
        .src(['./assets/global/css/*.css', '!./assets/global/css/*-rtl.min.css', '!./assets/global/css/*-rtl.css', '!./assets/global/css/*.min.css'])
        .pipe(rtlcss())
        .pipe(rename({ suffix: '-rtl' }))
        .pipe(gulp.dest('./assets/global/css'));

    gulp
        .src(['./assets/layouts/**/css/*.css', '!./assets/layouts/**/css/*-rtl.css', '!./assets/layouts/**/css/*-rtl.min.css', '!./assets/layouts/**/css/*.min.css'])
        .pipe(rtlcss())
        .pipe(rename({ suffix: '-rtl' }))
        .pipe(gulp.dest('./assets/layouts'));

    gulp
        .src(['./assets/layouts/**/css/**/*.css', '!./assets/layouts/**/css/**/*-rtl.css', '!./assets/layouts/**/css/**/*-rtl.min.css', '!./assets/layouts/**/css/**/*.min.css'])
        .pipe(rtlcss())
        .pipe(rename({ suffix: '-rtl' }))
        .pipe(gulp.dest('./assets/layouts'));

    gulp
        .src(['./assets/global/plugins/bootstrap/css/*.css', '!./assets/global/plugins/bootstrap/css/*-rtl.css', '!./assets/global/plugins/bootstrap/css/*.min.css'])
        .pipe(rtlcss())
        .pipe(rename({ suffix: '-rtl' }))
        .pipe(gulp.dest('./assets/global/plugins/bootstrap/css'));
});

//*** HTML formatter task
gulp.task('prettify', function () {

    gulp.src('./**/*.html').
        pipe(prettify({
            indent_size: 4,
            indent_inner_html: true,
            unformatted: ['pre', 'code']
        })).
        pipe(gulp.dest('./'));
});

gulp.task('scripts', function () {
    return gulp.src(['./wwwroot/app/**/*.js', '!./wwwroot/app/app.mod.js'])
        .pipe(concat('all.js'))
        .pipe(gulp.dest('./wwwroot/dist/'))
        .pipe(uglify()).pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('./wwwroot/dist/'))
});

gulp.task('template', function () {
    return gulp.src('./wwwroot/app/**/*.html')
        .pipe(templateCache())
        .pipe(gulp.dest('./wwwroot/dist/'));
});