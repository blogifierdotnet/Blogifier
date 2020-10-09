/// <binding />
module.exports = function (grunt) {
    grunt.initConfig({
        sass: {
            dist: {
                options: {
                    style: 'compressed'
                },
                files: {
                    'wwwroot/admin/css/admin.min.css': 'wwwroot/admin/scss/admin.scss',
                }
            }
        },
        watch: {
            src: {
                files: ['wwwroot/admin/scss/**/*.scss'],
                tasks: ['sass']
            }
        }
    });
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.registerTask('default', ['watch']);
};
