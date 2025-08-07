import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import ConfirmationModal from "../components/ConfirmationModal";
import { getTaskById, updateTask, deleteTask } from "../services/taskService";
import type { Task, TaskPriority, TaskStatus } from "../types/task";
import Navbar from "../components/Navbar";
import "../css/TaskDetailPage.css";

const TaskDetail: React.FC = () => {
  const [task, setTask] = useState<Task | null>(null);
  const [isSaving, setIsSaving] = useState(false);
  const [message, setMessage] = useState("");
  const { id } = useParams<{ id: string }>();
  const [showModal, setShowModal] = useState(false);
  const [pendingPriority, setPendingPriority] = useState<TaskPriority | null>(
    null
  );
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      getTaskById(id).then(setTask);
    }
  }, [id]);

  const handlePriorityChange = (value: TaskPriority) => {
    if (value === "High") {
      setPendingPriority(value);
      setShowModal(true);
    } else {
      setTask((prev) => (prev ? { ...prev, priority: value } : prev));
    }
  };

  const handleConfirmPriority = () => {
    if (pendingPriority && task) {
      setTask({ ...task, priority: pendingPriority });
    }
    setShowModal(false);
    setPendingPriority(null);
  };

  const handleUpdate = async () => {
    if (!task) return;
    setIsSaving(true);
    try {
      await updateTask(id!, task);
      setMessage("Task updated successfully!");
    } catch (error) {
      console.error("Error updating task:", error);
      setMessage("Failed to update task.");
    } finally {
      setIsSaving(false);
    }
  };

  const handleDelete = async () => {
    if (!id) return;
    try {
      await deleteTask(id);
      navigate("/");
    } catch (error) {
      console.error("Error deleting task:", error);
      setMessage("Failed to delete task.");
    }
  };

  if (!task) return <div>Loading...</div>;

  return (
    <>
      <Navbar />
      <div className="task-detail-form">
        <h1>Edit Task</h1>

        <label>
          Title:
          <input
            type="text"
            value={task.title}
            onChange={(e) => setTask({ ...task, title: e.target.value })}
          />
        </label>

        <label>
          Description:
          <textarea
            value={task.description}
            onChange={(e) => setTask({ ...task, description: e.target.value })}
          />
        </label>

        <label>
          Priority:
          <select
            value={task.priority}
            onChange={(e) =>
              handlePriorityChange(e.target.value as TaskPriority)
            }
          >
            <option value="Low">Low</option>
            <option value="Medium">Medium</option>
            <option value="High">High</option>
          </select>
        </label>

        <label>
          Status:
          <select
            value={task.status}
            onChange={(e) =>
              setTask({ ...task, status: e.target.value as TaskStatus })
            }
          >
            <option value="Pending">Pending</option>
            <option value="InProgress">In Progress</option>
            <option value="Completed">Completed</option>
            <option value="Archived">Archived</option>
          </select>
        </label>

        <label>
          Due Date:
          <input
            type="date"
            value={task.dueDate?.split("T")[0] || ""}
            onChange={(e) =>
              setTask({
                ...task,
                dueDate: new Date(e.target.value).toISOString(),
              })
            }
          />
        </label>

        <button onClick={handleUpdate} disabled={isSaving}>
          {isSaving ? "Saving..." : "Update Task"}
        </button>

        <button onClick={handleDelete} className="delete-button">
          Delete Task
        </button>

        {showModal && (
          <ConfirmationModal
            isOpen={showModal}
            onConfirm={handleConfirmPriority}
            onCancel={() => setShowModal(false)}
          />
        )}

        {message && <p>{message}</p>}
      </div>
    </>
  );
};

export default TaskDetail;
