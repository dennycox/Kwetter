import http from '../http-common';

const getProfile = async data => {
  return http.httptoken().get("Profile/", data.id);
};

const editProfile = async data => {
    return http.httptoken().put("Profile/", data.id);
  };

export default {
    getProfile,
    editProfile,
};