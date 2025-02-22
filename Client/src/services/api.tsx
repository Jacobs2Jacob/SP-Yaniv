import axios from 'axios';

// Create an Axios instance
const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    withCredentials: true
});

api.interceptors.request.use(
    (config) => {
        return config;
    },
    (error) => {
        error.response.statusText = error.response.data.message;
        return Promise.resolve(error.response);  
    }
);

api.interceptors.response.use(
    (response) => response,
    async (error) => {
        error.response.statusText = error.response.data.message;
        return Promise.resolve(error.response);  
    }
);

export default api;