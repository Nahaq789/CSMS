import axios, {AxiosInstance} from 'axios'

const apiConfig: AxiosInstance = axios.create({
    baseURL: 'http://localhost:8081'
})

export default apiConfig;