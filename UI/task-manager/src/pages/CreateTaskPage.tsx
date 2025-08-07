import { useState } from "react";
import type { TaskPriority, TaskStatus } from "../types/task";
import ConfirmationModal from "../components/ConfirmationModal";
import Navbar from "../components/Navbar";
import { createTask } from "../services/taskService";
import "../css/CreateTaskPage.css";

const CreateTaskPage: React.FC = () => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [priority, setPriority] = useState<TaskPriority>("Low");
  const [PendingPriority, setPendingPriority] = useState<TaskPriority | null>(
    null
  );
  const [status, setStatus] = useState<TaskStatus>("Pending");
  const [dueDate, setDueDate] = useState("");
  const [showModal, setShowModal] = useState(false);
  const taskStatusOptions: TaskStatus[] = [
    "Pending",
    "InProgress",
    "Completed",
    "Archived",
  ];
  const [message, setMessage] = useState("");

  const handlePriorityChange = (value: TaskPriority) => {
    if (value === "High") {
      setPendingPriority(value);
      setShowModal(true);
    } else {
      setPriority(value);
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    saveTask();
  };

  const saveTask = async () => {
    const newTask = {
      title,
      description,
      priority,
      dueDate: new Date(dueDate).toISOString(),
      status,
    };
    try {
      await createTask(newTask);
      console.log("Task created!");
      setMessage("Task created successfully!");
      resetFormFields();
    } catch (error) {
      console.error("Error creating task:", error);
      setMessage("Failed to create task.");
    }
  };
  const resetFormFields = () => {
    setTitle("");
    setDescription("");
    setPriority("Low");
    setStatus("Pending");
    setDueDate("");
    setPendingPriority(null);
  };

  return (
    <>
      <Navbar />
      <form className="create-task-form" onSubmit={handleSubmit}>
        <h1>Create Task</h1>

        <input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Title"
          required
        />

        <select
          value={priority}
          onChange={(e) => handlePriorityChange(e.target.value as TaskPriority)}
        >
          <option value="Low">Low</option>
          <option value="Medium">Medium</option>
          <option value="High">High</option>
        </select>

        <select
          value={status}
          onChange={(e) => setStatus(e.target.value as TaskStatus)}
        >
          {taskStatusOptions.map((statusOption) => (
            <option key={statusOption} value={statusOption}>
              {statusOption
                .replace(/-/g, " ")
                .replace(/\b\w/g, (l) => l.toUpperCase())}
            </option>
          ))}
        </select>

        <input
          type="date"
          value={dueDate}
          onChange={(e) => setDueDate(e.target.value)}
          placeholder="Due Date"
          required
        />

        <textarea
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Description"
          rows={3}
        />

        <button type="submit">Create</button>

        {showModal && (
          <ConfirmationModal
            isOpen={showModal}
            onConfirm={() => {
              if (PendingPriority) setPriority(PendingPriority);
              setShowModal(false);
            }}
            onCancel={() => setShowModal(false)}
          />
        )}
      {message && <p style={{color: "#27ae60"}}>{message}</p>}
      </form>
    </>
  );
};

export default CreateTaskPage;
