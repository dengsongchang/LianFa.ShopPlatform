const autoprefixer = require('autoprefixer');
const pxtorem = require('postcss-pxtorem');
const merge = require('webpack-merge')

module.exports = {
    // 输出文件目录
    outputDir: 'dist',
    publicPath: process.env.NODE_ENV === 'production' ? '/' : '/',
    css: {
        loaderOptions: {
            postcss: {
                plugins: [
                    autoprefixer(),
                    pxtorem({
                        rootValue: 37.5,
                        propList: ['*']
                    })
                ]
            }
        }
    },
    chainWebpack: config => {
        config.module
            .rule('images')
            .test(/\.(png|jpe?g|gif|webp)(\?.*)?$/)
            .use('url-loader')
            .loader('url-loader')
            .tap(options =>
                merge(options, {
                    limit: 10000,
                })
            )
    },

};
