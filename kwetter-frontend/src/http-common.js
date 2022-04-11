import axios from "axios";
import url from '../src/baseUrl';

const httpdefault = () => {
  const https = require("https");
  console.log(url)
  return axios.create({
    baseURL: url + "/api",
    httpsAgent: new https.Agent({
      rejectUnauthorized: false,
    }),
  });
};
const httptoken = () => {
  const token = localStorage.getItem("Token");
  return axios.create({
    baseURL: url + "/api",
    headers: {
      "Content-type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });
};

export default {
  httpdefault,
  httptoken,
};
