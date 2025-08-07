import { useEffect, useState } from "react";
import type { Task, PagedResponse } from "../types/task";
import TaskItem from "../components/TaskItem";
import Navbar from "../components/Navbar";
import StatusChart from "../components/StatusChart";
import { getTasks } from "../services/taskService";
import "../css/TaskListPage.css";

const PAGE_SIZE = 10;

const TaskListPage: React.FC = () => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [pagedData, setPagedData] = useState<PagedResponse<Task> | null>(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [isLoading, setIsLoading] = useState(false);

  const [priorityFilter, setPriorityFilter] = useState<string>("");
  const [statusFilter, setStatusFilter] = useState<string>("");

  const fetchTasks = async (page: number) => {
    setIsLoading(true);
    try {
      const data = await getTasks(page, PAGE_SIZE);

      if (page === 1) {
        setTasks(data.items);
      } else {
        setTasks((prev) => [...prev, ...data.items]);
      }

      setPagedData(data);
      setCurrentPage(data.page);
    } catch (error) {
      console.error("Error fetching tasks:", error);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchTasks(1);
  }, []);

  const handleScroll = () => {
    if (
      window.innerHeight + window.scrollY >= document.body.offsetHeight - 100 &&
      !isLoading &&
      pagedData?.hasNextPage
    ) {
      fetchTasks(currentPage + 1);
    }
  };

  useEffect(() => {
    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, [pagedData, isLoading, currentPage]);

  const filteredTasks = tasks.filter((task) => {
    const matchesPriority = priorityFilter
      ? task.priority.toLowerCase() === priorityFilter.toLowerCase()
      : true;
    const matchesStatus = statusFilter
      ? task.status.toLowerCase() === statusFilter.toLowerCase()
      : true;
    return matchesPriority && matchesStatus;
  });

  const resetFilters = () => {
    setPriorityFilter("");
    setStatusFilter("");
  };

  return (
    <div className="page-container">
      <Navbar />

      <div className="content">
        <h2>Task List</h2>
        <br />
        <StatusChart tasks={filteredTasks} />
        <br />

        <div className="filters">
          <label>
            Priority:
            <select
              value={priorityFilter}
              onChange={(e) => setPriorityFilter(e.target.value)}
            >
              <option value="">All</option>
              <option value="Low">Low</option>
              <option value="Medium">Medium</option>
              <option value="High">High</option>
            </select>
          </label>

          <label>
            Status:
            <select
              value={statusFilter}
              onChange={(e) => setStatusFilter(e.target.value)}
            >
              <option value="">All</option>
              <option value="Pending">Pending</option>
              <option value="InProgress">In Progress</option>
              <option value="Completed">Completed</option>
              <option value="Archived">Archived</option>
            </select>
          </label>

          <button onClick={resetFilters}>Reset Filters</button>
        </div>

        <div className="task-list">
          {filteredTasks.map((task) => (
            <TaskItem key={task.id} task={task} />
          ))}
        </div>

        {isLoading && <p>Loading more tasks...</p>}

        {!pagedData?.hasNextPage && !isLoading && (
          <p className="end-of-list">You've reached the end of the list.</p>
        )}
      </div>
    </div>
  );
};

export default TaskListPage;
