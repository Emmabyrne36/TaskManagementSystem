import type { Task } from '../types/task';
import { Link } from 'react-router-dom';
import '../css/TaskItem.css';


interface Props {
  task: Task;
}

const TaskItem: React.FC<Props> = ({ task }) => (
  <div className="task-item">
    <h3>{task.title}</h3>
    <p>{task.description}</p>
    <div className="task-meta">
      <span className={`priority ${task.priority}`}>{task.priority.toUpperCase()}</span>
      <span className={`status ${task.status}`}>{task.status}</span>
    </div>

    <Link to={`/task/${task.id}`} className="view-details-button">
      View Details
    </Link>
  </div>
);

export default TaskItem;

