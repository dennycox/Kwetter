import http from '../http-common';

const getProfile = async data => {
  return http.httptoken().get("Profile/user/", data);
};

const editProfile = async data => {
    return http.httptoken().put("Profile/", data);
  };

export default {
    getProfile,
    editProfile,
};