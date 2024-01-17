import axios, { AxiosInstance } from "axios";

const apiConfig: AxiosInstance = axios.create({
  baseURL: "https://localhost:7071",
});

export default apiConfig;
