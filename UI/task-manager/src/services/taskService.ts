import axios from "axios";
import type { Task, PagedResponse } from "../types/task";

const API_BASE = import.meta.env.TASKMANAGEMENT_API_URL || "http://localhost:5000/api";

export const getTasks = async (page: number = 1, pageSize: number = 10): Promise<PagedResponse<Task>> => {
  const response = await axios.get(`${API_BASE}/tasks?page=${page}&pageSize=${pageSize}`);
  return response.data;
};

export const getTaskById = async (id: string): Promise<Task> => {
  const response = await axios.get<Task>(`${API_BASE}/tasks/${id}`);
  return response.data;
};

export const createTask = async (task: Partial<Task>): Promise<Task> => {
  const response = await axios.post<Task>(`${API_BASE}/tasks`, task);
  return response.data;
};

export const updateTask = async (id: string, task: Partial<Task>): Promise<Task> => {
  const response = await axios.put<Task>(`${API_BASE}/tasks?id=${id}`, task);
  return response.data;
};

export const deleteTask = async (id: string): Promise<void> => {
  await axios.delete(`${API_BASE}/tasks?id=${id}`);
};