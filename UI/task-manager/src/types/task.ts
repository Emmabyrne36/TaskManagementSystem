export type TaskStatus = 'Pending' | 'InProgress' | 'Completed' | 'Archived';
export type TaskPriority = 'Low' | 'Medium' | 'High';

export interface Task {
  id: string;
  title: string;
  priority: TaskPriority;
  dueDate: string;
  status: TaskStatus;
  description: string;
}

export type PagedResponse<T> = {
  items: T[];
  pageSize: number;
  page: number;
  total: number;
  hasNextPage: boolean;
};
