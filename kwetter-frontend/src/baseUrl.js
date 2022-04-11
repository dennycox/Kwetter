var url = ''

if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
    url = 'http://localhost:5001';
} else {
    url = 'http://localhost:6060';
}
export default url;
