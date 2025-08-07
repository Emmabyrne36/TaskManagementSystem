import { BrowserRouter, Routes, Route } from 'react-router-dom';
import TaskListPage from './pages/TaskListPage';
import CreateTaskPage from './pages/CreateTaskPage';
import TaskDetailPage from './pages/TaskDetailPage';
import './App.css';

const App = () => {
  return (
    <div className="app-container">
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<TaskListPage />} />
          <Route path="/create" element={<CreateTaskPage />} />
          <Route path="/task/:id" element={<TaskDetailPage />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
};

export default App;
