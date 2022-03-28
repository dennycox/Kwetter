import http from '../http-common';

const login = async data => {
  return http.httpdefault().post("Authentication/login", data);
};

const register = async data => {
  return http.httpdefault().post("Authentication/register", data);
};

export default {
    login,
    register,
};