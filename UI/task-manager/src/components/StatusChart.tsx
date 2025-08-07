import { PieChart, Pie, Cell, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import type { Task } from '../types/task';

interface Props {
  tasks: Task[];
}

const STATUS_COLORS: Record<string, string> = {
  Pending: '#f39c12',
  'InProgress': '#3498db',
  Completed: '#2ecc71',
  Archived: '#7f8c8d',
};

const StatusChart: React.FC<Props> = ({ tasks }) => {
  const statusTypes = ['Pending', 'InProgress', 'Completed', 'Archived'];

  const statusCounts = statusTypes.map((status) => ({
    name: status.replace('-', ' ').toUpperCase(),
    value: tasks.filter((t) => t.status === status).length,
    status,
  }));

  return (
    <div style={{ width: '100%', maxWidth: 400, margin: '0 auto' }}>
      <ResponsiveContainer width="100%" height={250}>
        <PieChart>
          <Pie
            data={statusCounts}
            dataKey="value"
            nameKey="name"
            cx="50%"
            cy="50%"
            outerRadius={80}
            label
          >
            {statusCounts.map((entry, index) => (
              <Cell key={index} fill={STATUS_COLORS[entry.status]} />
            ))}
          </Pie>
          <Tooltip />
          <Legend />
        </PieChart>
      </ResponsiveContainer>
    </div>
  );
};

export default StatusChart;
