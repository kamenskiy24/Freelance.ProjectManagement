import axios from 'axios'
import * as constants from './constants'

const client = axios.create({
    baseURL: constants.API_URL,
    timeout: 5000,
    headers: {
        'Content-Type': 'application/json'
    }
 });

 //Project API calls
export const getProjects = () => sendRequest(client.get('/projects'));

export const createProject = (json) => sendRequest(client.post('/projects', json));

export const updateProject = (id, json) => sendRequest(client.put('/projects/' + String(id), json));

export const deleteProject = (id) => sendRequest(client.delete('/projects/' + String(id)));

export const getTasksByProjectId = (projectId) => sendRequest(client.get('/projects/' + String(projectId) + '/tasks'));

//Task API calls
export const createTask = (projectId, json) => sendRequest(client.post('/projects/' + String(projectId) + '/tasks', json));

export const updateTask = (id, json) => sendRequest(client.put('/tasks/' + String(id), json));

export const deleteTask = (id) => sendRequest(client.delete('/tasks/' + String(id)));
    

const sendRequest = async (callback) => {
    try {
        return await callback;
    } catch (error) {
        console.log(error);
    }
}